using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models.Image;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class CreateImagesConsumer : IConsumer<ICreateImagesRequest>
  {
    private readonly IImageRepository _imageRepository;
    private readonly IDbImageMapper _dbImageMapper;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly ILogger<CreateImagesConsumer> _logger;

    private async Task<object> CreateImagesAsync(ICreateImagesRequest request)
    {
      List<DbImage> dbImages = new();
      List<Guid> previewIds = new(); 

      foreach (CreateImageData createImage in request.Images)
      {
        (bool isSuccess, string resizedContent, string extension) imageResizeResult =
          request.ImageSource.Equals(ImageSource.User) 
            ? await _resizeHelper.ResizeAsync(createImage.Content, createImage.Extension, (int)ImageSizes.Middle)
            : await _resizeHelper.ResizeAsync(createImage.Content, createImage.Extension, (int)ImageSizes.Big);

        if (!imageResizeResult.isSuccess)
        {
          _logger.LogError("Error while resize image.");
          return null;
        }

        (bool isSuccess, string resizedContent, string extension) previewResizeResult =
          request.ImageSource.Equals(ImageSource.User)
            ? await _resizeHelper.ResizeForPreviewAsync(createImage.Content, createImage.Extension)
            : await _resizeHelper.ResizeForPreviewAsync(createImage.Content, createImage.Extension, 4, 3);

        if (!previewResizeResult.isSuccess)
        {
          _logger.LogError("Error while resize image.");
          return null;
        }

        DbImage dbImage = string.IsNullOrEmpty(imageResizeResult.resizedContent)
          ? _dbImageMapper.Map(createImage, request.CreatedBy)
          : _dbImageMapper.Map(createImage, request.CreatedBy, null, imageResizeResult.resizedContent, imageResizeResult.extension);

        if (string.IsNullOrEmpty(previewResizeResult.resizedContent))
        {
          dbImage.ParentId = dbImage.Id;
          previewIds.Add(dbImage.Id);
        }
        else
        {
          DbImage dbPrewiewImage = _dbImageMapper.Map(createImage, request.CreatedBy, dbImage.Id, previewResizeResult.resizedContent, previewResizeResult.extension);
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
      IImageResizeHelper resizeHelper,
      ILogger<CreateImagesConsumer> logger)
    {
      _imageRepository = imageRepository;
      _dbImageMapper = dbImageUserMapper;
      _resizeHelper = resizeHelper;
      _logger = logger;
    }

    public async Task Consume(ConsumeContext<ICreateImagesRequest> context)
    {
      object response = OperationResultWrapper.CreateResponse(CreateImagesAsync, context.Message);

      await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
    }
  }
}
