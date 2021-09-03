using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Helpers.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class CreateImagesConsumer : IConsumer<ICreateImageRequest>
  {
    private readonly IImageUserRepository _imageUserRepository;
    private readonly IImageProjectRepository _imageProjectRepository;
    private readonly IImageNewsRepository _imageNewsRepository;
    private readonly IImageMessageRepository _imageMessageRepository;
    private readonly IDbImageUserMapper _dbImageUserMapper;
    private readonly IDbImageNewsMapper _dbImageNewsMapper;
    private readonly IDbImageProjectMapper _dbImageProjectMapper;
    private readonly IDbImageMessageMapper _dbImageMessageMapper;
    private readonly IResizeImageHelper _resizeHelper;

    public CreateImagesConsumer(
        IImageUserRepository imageUserRepository,
        IImageProjectRepository imageProjectRepository,
        IImageNewsRepository imageNewsRepository,
        IImageMessageRepository imageMessageRepository,
        IDbImageUserMapper dbImageUserMapper,
        IDbImageNewsMapper dbImageNewsMapper,
        IDbImageProjectMapper dbImageProjectMapper,
        IDbImageMessageMapper dbImageMessageMapper,
        IResizeImageHelper resizeHelper)
    {
      _imageUserRepository = imageUserRepository;
      _imageProjectRepository = imageProjectRepository;
      _imageNewsRepository = imageNewsRepository;
      _imageMessageRepository = imageMessageRepository;
      _dbImageUserMapper = dbImageUserMapper;
      _dbImageNewsMapper = dbImageNewsMapper;
      _dbImageProjectMapper = dbImageProjectMapper;
      _dbImageMessageMapper = dbImageMessageMapper;
      _resizeHelper = resizeHelper;
    }

    public async Task Consume(ConsumeContext<ICreateImageRequest> context)
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

    private object CreateUserImages(ICreateImageRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageUser> dbImages = new();
      List<Guid> previewIds = new();
      DbImageUser dbImageUser;
      DbImageUser dbPrewiewImageUser;
      string resizedContent;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageUser = _dbImageUserMapper.Map(createImage);
        resizedContent = _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (string.IsNullOrEmpty(resizedContent))
        {
          dbImageUser.ParentId = dbImageUser.Id;
          previewIds.Add(dbImageUser.Id);
        }
        else
        {
          dbPrewiewImageUser = _dbImageUserMapper.Map(createImage, dbImageUser.Id, resizedContent);
          dbImages.Add(dbPrewiewImageUser);
          previewIds.Add(dbPrewiewImageUser.Id);
        }

        dbImages.Add(dbImageUser);
      }

      if (_imageUserRepository.Create(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    private object CreateProjectImages(ICreateImageRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageProject> dbImages = new();
      List<Guid> previewIds = new();
      DbImageProject dbImageProject;
      DbImageProject dbPrewiewImageProject;
      string resizedContent;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageProject = _dbImageProjectMapper.Map(createImage);
        resizedContent = _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (string.IsNullOrEmpty(resizedContent))
        {
          dbImageProject.ParentId = dbImageProject.Id;
          previewIds.Add(dbImageProject.Id);
        }
        else
        {
          dbPrewiewImageProject = _dbImageProjectMapper.Map(createImage, dbImageProject.Id, resizedContent);
          dbImages.Add(dbPrewiewImageProject);
          previewIds.Add(dbPrewiewImageProject.Id);
        }

        dbImages.Add(dbImageProject);
      }

      if (_imageProjectRepository.Create(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }

    private object CreateMessageImages(ICreateImageRequest request)
    {
      if (request.Images == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      List<DbImageMessage> dbImages = new();
      List<Guid> previewIds = new();
      DbImageMessage dbImageMessage;
      DbImageMessage dbPrewiewImageMessage;
      string resizedContent;

      foreach (CreateImageData createImage in request.Images)
      {
        dbImageMessage = _dbImageMessageMapper.Map(createImage);
        resizedContent = _resizeHelper.Resize(createImage.Content, createImage.Extension);

        if (string.IsNullOrEmpty(resizedContent))
        {
          dbImageMessage.ParentId = dbImageMessage.Id;
          previewIds.Add(dbImageMessage.Id);
        }
        else
        {
          dbPrewiewImageMessage = _dbImageMessageMapper.Map(createImage, dbImageMessage.Id, resizedContent);
          dbImages.Add(dbPrewiewImageMessage);
          previewIds.Add(dbPrewiewImageMessage.Id);
        }

        dbImages.Add(dbImageMessage);
      }

      if (_imageMessageRepository.Create(dbImages) == null)
      {
        return ICreateImagesResponse.CreateObj(null);
      }

      return ICreateImagesResponse.CreateObj(previewIds);
    }
  }
}
