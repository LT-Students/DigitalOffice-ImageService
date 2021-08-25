using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class DeleteImageProjectServiceConsumer : IConsumer<IDeleteImagesProjectRequest>
    {
        private readonly IImageProjectRepository _imageProjectRepository;

        public DeleteImageProjectServiceConsumer(IImageProjectRepository imageProjectRepository)
        {
            _imageProjectRepository = imageProjectRepository;
        }

        public async Task Consume(ConsumeContext<IDeleteImagesProjectRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DeleteImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object DeleteImages(IDeleteImagesProjectRequest request)
        {
            return _imageProjectRepository.Delete(request.ImageIds);
        }
    }
}
