﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using HealthChecks.UI.Client;
using LT.DigitalOffice.ImageService.Broker.Consumers;
using LT.DigitalOffice.ImageService.Data.Provider.MsSql.Ef;
using LT.DigitalOffice.ImageService.Models.Dto.Configuration;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Kernel.BrokerSupport.Extensions;
using LT.DigitalOffice.Kernel.BrokerSupport.Helpers;
using LT.DigitalOffice.Kernel.BrokerSupport.Middlewares.Token;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Kernel.EFSupport.Extensions;
using LT.DigitalOffice.Kernel.EFSupport.Helpers;
using LT.DigitalOffice.Kernel.Extensions;
using LT.DigitalOffice.Kernel.Middlewares.ApiInformation;
using MassTransit;
using MassTransit.RabbitMqTransport;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.ImageService
{
  public class Startup : BaseApiInfo
  {
    public const string CorsPolicyName = "LtDoCorsPolicy";

    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly BaseServiceInfoConfig _serviceInfoConfig;

    public IConfiguration Configuration { get; }

    #region public methods

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;

      _serviceInfoConfig = Configuration
        .GetSection(BaseServiceInfoConfig.SectionName)
        .Get<BaseServiceInfoConfig>();

      _rabbitMqConfig = Configuration
        .GetSection(BaseRabbitMqConfig.SectionName)
        .Get<RabbitMqConfig>();

      Version = "1.0.1.1";
      Description = "ImageService is an API that intended to work with images.";
      StartTime = DateTime.UtcNow;
      ApiName = $"LT Digital Office - {_serviceInfoConfig.Name}";
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddCors(options =>
      {
        options.AddPolicy(
          CorsPolicyName,
          builder =>
          {
            builder
              .AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
          });
      });

      services.Configure<TokenConfiguration>(Configuration.GetSection("CheckTokenMiddleware"));
      services.Configure<BaseRabbitMqConfig>(Configuration.GetSection(BaseRabbitMqConfig.SectionName));
      services.Configure<BaseServiceInfoConfig>(Configuration.GetSection(BaseServiceInfoConfig.SectionName));

      services.AddHttpContextAccessor();

      services
        .AddControllers()
        .AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        })
        .AddNewtonsoftJson();

      string dbConnStr = ConnectionStringHandler.Get(Configuration);

      if (string.IsNullOrEmpty(dbConnStr))
      {
        dbConnStr = Configuration.GetConnectionString("SQLConnectionString");
      }

      services.AddDbContext<ImageServiceDbContext>(options =>
      {
        options.UseSqlServer(dbConnStr);
      });

      services.AddHealthChecks()
        .AddRabbitMqCheck()
        .AddSqlServer(dbConnStr);

      services.AddBusinessObjects();

      ConfigureMassTransit(services);
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
      app.UpdateDatabase<ImageServiceDbContext>();

      app.UseForwardedHeaders();

      app.UseExceptionsHandler(loggerFactory);

      app.UseApiInformation();

      app.UseRouting();

      app.UseMiddleware<TokenMiddleware>();

      app.UseCors(CorsPolicyName);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers().RequireCors(CorsPolicyName);

        endpoints.MapHealthChecks($"/{_serviceInfoConfig.Id}/hc", new HealthCheckOptions
        {
          ResultStatusCodes = new Dictionary<HealthStatus, int>
              {
                { HealthStatus.Unhealthy, 200 },
                { HealthStatus.Healthy, 200 },
                { HealthStatus.Degraded, 200 },
              },
          Predicate = check => check.Name != "masstransit-bus",
          ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
        });
      });
    }

    #endregion

    #region private methods

    private void ConfigureMassTransit(IServiceCollection services)
    {
      (string username, string password) = RabbitMqCredentialsHelper
        .Get(_rabbitMqConfig, _serviceInfoConfig);

      services.AddMassTransit(x =>
      {
        x.AddConsumer<RemoveImagesConsumer>();
        x.AddConsumer<GetImagesConsumer>();
        x.AddConsumer<CreateImagesConsumer>();

        x.UsingRabbitMq((context, cfg) =>
        {
          cfg.Host(_rabbitMqConfig.Host, "/", host =>
            {
              host.Username(username);
              host.Password(password);
            });

          ConfigureEndpoints(context, cfg);
        });

        x.AddRequestClients(_rabbitMqConfig);
      });

      services.AddMassTransitHostedService();
    }

    private void ConfigureEndpoints(
      IBusRegistrationContext context,
      IRabbitMqBusFactoryConfigurator cfg)
    {
      cfg.ReceiveEndpoint(_rabbitMqConfig.GetImagesEndpoint, ep =>
      {
        ep.ConfigureConsumer<GetImagesConsumer>(context);
      });
      cfg.ReceiveEndpoint(_rabbitMqConfig.CreateImagesEndpoint, ep =>
      {
        ep.ConfigureConsumer<CreateImagesConsumer>(context);
      });
      cfg.ReceiveEndpoint(_rabbitMqConfig.RemoveImagesEndpoint, ep =>
      {
        ep.ConfigureConsumer<RemoveImagesConsumer>(context);
      });
    }

    #endregion
  }
}
