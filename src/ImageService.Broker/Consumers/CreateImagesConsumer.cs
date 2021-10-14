using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class CreateImagesConsumer : IConsumer<ICreateImagesRequest>
  {
    private readonly IImageUserRepository _imageUserRepository;
    private readonly IImageProjectRepository _imageProjectRepository;
    private readonly IImageMessageRepository _imageMessageRepository;
    private readonly IDbImageUserMapper _dbImageUserMapper;
    private readonly IDbImageProjectMapper _dbImageProjectMapper;
    private readonly IDbImageMessageMapper _dbImageMessageMapper;
    private readonly IImageResizeHelper _resizeHelper;

    public CreateImagesConsumer(
      IImageUserRepository imageUserRepository,
      IImageProjectRepository imageProjectRepository,
      IImageMessageRepository imageMessageRepository,
      IDbImageUserMapper dbImageUserMapper,
      IDbImageProjectMapper dbImageProjectMapper,
      IDbImageMessageMapper dbImageMessageMapper,
      IImageResizeHelper resizeHelper)
    {
      _imageUserRepository = imageUserRepository;
      _imageProjectRepository = imageProjectRepository;
      _imageMessageRepository = imageMessageRepository;
      _dbImageUserMapper = dbImageUserMapper;
      _dbImageProjectMapper = dbImageProjectMapper;
      _dbImageMessageMapper = dbImageMessageMapper;
      _resizeHelper = resizeHelper;
    }

    public async Task Consume(ConsumeContext<ICreateImagesRequest> context)
    {
      object response;

      switch (context.Message.ImageSource)
      {
        case ImageSource.User:
          response = OperationResultWrapper.CreateResponse(CreateUserImages, context.Message);
          break;

        case ImageSource.Project:
          response = OperationResultWrapper.CreateResponse(CreateProjectImages, context.Message);
          break;

        case ImageSource.Message:
          response = OperationResultWrapper.CreateResponse(CreateMessageImages, context.Message);
          break;

        default:
          response = null;
          break;
      }

      await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
    }

    private async Task<object> CreateUserImages(ICreateImagesRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageUser> dbImages = new();
      List<Guid> previewIds = new();
      DbImageUser dbImageUser;
      DbImageUser dbPrewiewImageUser;
      (bool isSuccess, string resizedContent, string extension) resizeResult;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageUser = _dbImageUserMapper.Map(createImage);
        resizeResult = await _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (!resizeResult.isSuccess)
        {
          return ICreateImagesResponse.CreateObj(null);
        }

        if (string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbImageUser.ParentId = dbImageUser.Id;
          previewIds.Add(dbImageUser.Id);
        }
        else
        {
          dbPrewiewImageUser = _dbImageUserMapper.Map(createImage, dbImageUser.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImages.Add(dbPrewiewImageUser);
          previewIds.Add(dbPrewiewImageUser.Id);
        }

        dbImages.Add(dbImageUser);
      }

      if (await _imageUserRepository.CreateAsync(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    private async Task<object> CreateProjectImages(ICreateImagesRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageProject> dbImages = new();
      List<Guid> previewIds = new();
      DbImageProject dbImageProject;
      DbImageProject dbPrewiewImageProject;
      (bool isSuccess, string resizedContent, string extension) resizeResult;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageProject = _dbImageProjectMapper.Map(createImage);
        resizeResult = await _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (!resizeResult.isSuccess)
        {
          return ICreateImagesResponse.CreateObj(null);
        }

        if (string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbImageProject.ParentId = dbImageProject.Id;
          previewIds.Add(dbImageProject.Id);
        }
        else
        {
          dbPrewiewImageProject = _dbImageProjectMapper.Map(createImage, dbImageProject.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImages.Add(dbPrewiewImageProject);
          previewIds.Add(dbPrewiewImageProject.Id);
        }

        dbImages.Add(dbImageProject);
      }

      if (await _imageProjectRepository.CreateAsync(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    private async Task<object> CreateMessageImages(ICreateImagesRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageMessage> dbImages = new();
      List<Guid> previewIds = new();
      DbImageMessage dbImageMessage;
      DbImageMessage dbPrewiewImageMessage;
      (bool isSuccess, string resizedContent, string extension) resizeResult;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageMessage = _dbImageMessageMapper.Map(createImage);
        resizeResult = await _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (!resizeResult.isSuccess)
        {
          return ICreateImagesResponse.CreateObj(null);
        }

        if (string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbImageMessage.ParentId = dbImageMessage.Id;
          previewIds.Add(dbImageMessage.Id);
        }
        else
        {
          dbPrewiewImageMessage = _dbImageMessageMapper.Map(createImage, dbImageMessage.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImages.Add(dbPrewiewImageMessage);
          previewIds.Add(dbPrewiewImageMessage.Id);
        }

        dbImages.Add(dbImageMessage);
      }

      if (await _imageMessageRepository.CreateAsync(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }
  }
}
