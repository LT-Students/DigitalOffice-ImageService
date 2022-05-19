using System.Linq;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.Broker;
using LT.DigitalOffice.Models.Broker.Publishing.Subscriber.Image;
using MassTransit;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
  public class RemoveImagesConsumer : IConsumer<IRemoveImagesPublish>
  {
    private readonly IImageRepository _repository;

    public RemoveImagesConsumer(IImageRepository repository)
    {
      _repository = repository;
    }

    public async Task Consume(ConsumeContext<IRemoveImagesPublish> context)
    {
      if (context.Message.ImagesIds is not null && context.Message.ImagesIds.Any())
      {
        await _repository.RemoveAsync(
          context.Message.ImageSource,
          context.Message.ImagesIds);
      }

      //move to publish
      await context.RespondAsync<IOperationResult<bool>>(true);
    }
  }
}
