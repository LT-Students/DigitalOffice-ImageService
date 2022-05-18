using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Broker.Helpers;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Publishing.Subscriber.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class CreateImagesConsumer : IConsumer<ICreateImagesPublish>
  {
    private readonly IImageRepository _imageRepository;
    private readonly IDbImageMapper _dbImageMapper;
    private readonly IImageResizeHelper _resizeHelper;

    private async Task<object> CreateImagesAsync(ICreateImagesPublish request)
    {
      List<DbImage> dbImages = new();
      List<Guid> previewIds = new();
      DbImage dbImage;
      DbImage dbPrewiewImage;
      (bool isSuccess, string resizedContent, string extension) resizeResult;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImage = _dbImageMapper.Map(createImage);
        resizeResult = await _resizeHelper.ResizeAsync(createImage.Content, createImage.Extension);

        if (!resizeResult.isSuccess)
        {
          //TO DO add log
          return new();
        }

        if (string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbImage.ParentId = dbImage.Id;
          previewIds.Add(dbImage.Id);
        }
        else
        {
          dbPrewiewImage = _dbImageMapper.Map(createImage, dbImage.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImages.Add(dbPrewiewImage);
          previewIds.Add(dbPrewiewImage.Id);
        }

        dbImages.Add(dbImage);
      }

      await _imageRepository.CreateAsync(request.ImageSource, dbImages);

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    public CreateImagesConsumer(
      IImageRepository imageRepository,
      IDbImageMapper dbImageUserMapper,
      IImageResizeHelper resizeHelper)
    {
      _imageRepository = imageRepository;
      _dbImageMapper = dbImageUserMapper;
      _resizeHelper = resizeHelper;
    }

    public async Task Consume(ConsumeContext<ICreateImagesPublish> context)
    {
      object response = OperationResultWrapper.CreateResponse(CreateImagesAsync, context.Message);

      await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
    }
  }
}
