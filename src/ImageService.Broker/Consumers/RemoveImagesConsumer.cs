using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Publishing.Subscriber.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class RemoveImagesConsumer : IConsumer<IRemoveImagesPublish>
  {
    private readonly IImageMessageRepository _imageMessageRepository;
    private readonly IImageProjectRepository _imageProjectRepository;
    private readonly IImageUserRepository _imageUserRepository;

    public RemoveImagesConsumer(
      IImageMessageRepository imageMessageRepository,
      IImageProjectRepository imageProjectRepository,
      IImageUserRepository imageUserRepository)
    {
      _imageMessageRepository = imageMessageRepository;
      _imageProjectRepository = imageProjectRepository;
      _imageUserRepository = imageUserRepository;
    }

    public async Task Consume(ConsumeContext<IRemoveImagesPublish> context)
    {
      object response = OperationResultWrapper.CreateResponse(RemoveImagesAsync, context.Message);

      await context.RespondAsync<IOperationResult<bool>>(response);
    }

    private async Task<object> RemoveImagesAsync(IRemoveImagesPublish request)
    {
      switch (request.ImageSource)
      {
        case ImageSource.Message:
          return await _imageMessageRepository.RemoveAsync(request.ImagesIds);
        case ImageSource.Project:
          return await _imageProjectRepository.RemoveAsync(request.ImagesIds);
        case ImageSource.User:
          return await _imageUserRepository.RemoveAsync(request.ImagesIds);
        default:
          return null;
      }
    }
  }
}
