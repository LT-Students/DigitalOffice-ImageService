using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class DeleteImagesMessageConsumer : IConsumer<IDeleteImagesMessageRequest>
    {
        private readonly IImageMessageRepository _imageMessageRepository;

        public DeleteImagesMessageConsumer(
            IImageMessageRepository imageMessageRepository)
        {
            _imageMessageRepository = imageMessageRepository;
        }

        public async Task Consume(ConsumeContext<IDeleteImagesMessageRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(DeleteImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object DeleteImages(IDeleteImagesMessageRequest request)
        {
            return _imageMessageRepository.Delete(request.ImageIds);
        }
    }
}
