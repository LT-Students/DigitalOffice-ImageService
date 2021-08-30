using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class DeleteImagesConsumer : IConsumer<IRemoveImagesRequest>
    {
        private readonly IImageMessageRepository _imageMessageRepository;
        private readonly IImageNewsRepository _imageNewsRepository;
        private readonly IImageProjectRepository _imageProjectRepository;
        private readonly IImageUserRepository _imageUserRepository;

        public DeleteImagesConsumer(
            IImageMessageRepository imageMessageRepository,
            IImageNewsRepository imageNewsRepository,
            IImageProjectRepository imageProjectRepository,
            IImageUserRepository imageUserRepository)
        {
            _imageMessageRepository = imageMessageRepository;
            _imageNewsRepository = imageNewsRepository;
            _imageProjectRepository = imageProjectRepository;
            _imageUserRepository = imageUserRepository;
        }

        public async Task Consume(ConsumeContext<IRemoveImagesRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(DeleteImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object DeleteImages(IRemoveImagesRequest request)
        {
            switch (request.ImageSource)
            {
                case ImageSource.Message:
                    return _imageMessageRepository.Delete(request.ImagesIds);
                case ImageSource.News:
                    return _imageNewsRepository.Delete(request.ImagesIds);
                case ImageSource.Project:
                    return _imageProjectRepository.Delete(request.ImagesIds);
                case ImageSource.User:
                    return _imageUserRepository.Delete(request.ImagesIds);
                default:
                    return null;
            }
        }
    }
}
