using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class GetImagesConsumer : IConsumer<IGetImagesRequest>
  {
    private readonly IImageMessageRepository _imageMessageRepository;
    private readonly IImageProjectRepository _imageProjectRepository;
    private readonly IImageUserRepository _imageUserRepository;

    public GetImagesConsumer(
      IImageMessageRepository imageMessageRepository,
      IImageProjectRepository imageProjectRepository,
      IImageUserRepository imageUserRepository)
    {
      _imageMessageRepository = imageMessageRepository;
      _imageProjectRepository = imageProjectRepository;
      _imageUserRepository = imageUserRepository;
    }

    public async Task Consume(ConsumeContext<IGetImagesRequest> context)
    {
      object response;

      switch (context.Message.ImageSource)
      {
        case ImageSource.User:
          response = OperationResultWrapper.CreateResponse(GetUserImages, context.Message);
          break;

        case ImageSource.Project:
          response = OperationResultWrapper.CreateResponse(GetProjectImages, context.Message);
          break;

        case ImageSource.Message:
          response = OperationResultWrapper.CreateResponse(GetMessageImages, context.Message);
          break;

        default:
          response = null;
          break;
      }

      await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
    }

    private object GetUserImages(IGetImagesRequest request)
    {
      List<DbImageUser> dbUserImages = _imageUserRepository.Get(request.ImagesIds);

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

    private object GetMessageImages(IGetImagesRequest request)
    {
      List<DbImageMessage> dbMessageImages = _imageMessageRepository.Get(request.ImagesIds);

      return IGetImagesResponse.CreateObj(
        dbMessageImages
          .Select(dbImagesMessage => new ImageData(
            dbImagesMessage.Id,
            dbImagesMessage.ParentId,
            null,
            dbImagesMessage.Content,
            dbImagesMessage.Extension,
            dbImagesMessage.Name))
          .ToList());
    }

    private object GetProjectImages(IGetImagesRequest request)
    {
      List<DbImageProject> dbProjectImages = _imageProjectRepository.Get(request.ImagesIds);

      return IGetImagesResponse.CreateObj(
        dbProjectImages
          .Select(dbImagesProject => new ImageData(
            dbImagesProject.Id,
            dbImagesProject.ParentId,
            null,
            dbImagesProject.Content,
            dbImagesProject.Extension,
            dbImagesProject.Name))
          .ToList());
    }
  }
}
