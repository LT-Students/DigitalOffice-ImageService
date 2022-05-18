using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Broker.Helpers;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class GetImagesConsumer : IConsumer<IGetImagesRequest>
  {
    private readonly IImageRepository _repository;

    private async Task<object> GetUserImagesAsync(IGetImagesRequest request)
    {
      List<DbImage> dbUserImages = await _repository
        .GetAsync(request.ImageSource, request.ImagesIds);

      return IGetImagesResponse.CreateObj(
        dbUserImages
          .Select(dbImagesUser => new ImageData(
            dbImagesUser.Id,
            dbImagesUser.ParentId,
            null,
            dbImagesUser.Content,
            dbImagesUser.Extension,
            dbImagesUser.Name))
          .ToList());
    }

    public GetImagesConsumer(IImageRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IGetImagesRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(GetUserImagesAsync, context.Message);

      await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
    }
  }
}
