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

    private async Task<object> CreateAvatarImagesAsync(ICreateImagesRequest request)
    {
      List<DbImage> dbImages = new();
      List<Guid> previewIds = new(); 

      foreach (CreateImageData createImage in request.Images)
      {
        (bool isSuccess, string resizedContent, string extension) bigAvatarResizeResult = await _resizeHelper.ResizePreciselyAsync(createImage.Content, createImage.Extension, (int)AvatarSizes.BigAvatar, (int)AvatarSizes.BigAvatar);
        (bool isSuccess, string resizedContent, string extension) smallAvatarResizeResult = await _resizeHelper.ResizePreciselyAsync(createImage.Content, createImage.Extension, (int)AvatarSizes.SmallAvatar, (int)AvatarSizes.SmallAvatar);

        if (!bigAvatarResizeResult.isSuccess || !smallAvatarResizeResult.isSuccess)
        {
          _logger.LogError("Error while resize image.");
          return null;
        }

        DbImage dbImage;

        if (string.IsNullOrEmpty(bigAvatarResizeResult.resizedContent))
        {
          dbImage = _dbImageMapper.Map(createImage, request.CreatedBy);
        } 
        else
        {
          dbImage = _dbImageMapper.Map(createImage, request.CreatedBy, null, bigAvatarResizeResult.resizedContent, bigAvatarResizeResult.extension);
        }

        if (string.IsNullOrEmpty(smallAvatarResizeResult.resizedContent))
        {
          dbImage.ParentId = dbImage.Id;
          previewIds.Add(dbImage.Id);
        }
        else
        {
          DbImage dbPrewiewImage = _dbImageMapper.Map(createImage, request.CreatedBy, dbImage.Id, smallAvatarResizeResult.resizedContent, smallAvatarResizeResult.extension);
          dbImages.Add(dbPrewiewImage);
          previewIds.Add(dbPrewiewImage.Id);
        }

        dbImages.Add(dbImage);
      }

      await _imageRepository.CreateAsync(request.ImageSource, dbImages);

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    private async Task<object> CreateImagesAsync(ICreateImagesRequest request)
    {
      List<DbImage> dbImages = new();
      List<Guid> previewIds = new();

      foreach (CreateImageData createImage in request.Images)
      {
        DbImage dbImage = _dbImageMapper.Map(createImage, request.CreatedBy);
        (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(createImage.Content, createImage.Extension);

        if (!resizeResult.isSuccess)
        {
          _logger.LogError("Error while resize image.");
          return null;
        }

        if (string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbImage.ParentId = dbImage.Id;
          previewIds.Add(dbImage.Id);
        }
        else
        {
          DbImage dbPrewiewImage = _dbImageMapper.Map(createImage, request.CreatedBy, dbImage.Id, resizeResult.resizedContent, resizeResult.extension);
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
      object response = context.Message.ImageSource.Equals(ImageSource.User)
        ? OperationResultWrapper.CreateResponse(CreateAvatarImagesAsync, context.Message)
        : OperationResultWrapper.CreateResponse(CreateImagesAsync, context.Message);

      await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
    }
  }
}
