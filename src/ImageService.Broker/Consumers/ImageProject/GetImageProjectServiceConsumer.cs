using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.File;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class GetImageProjectServiceConsumer : IConsumer<IGetImagesProjectRequest>
    {
        private readonly IImageProjectRepository _imageProjectRepository;
        private readonly IImageDataMapper _imageDataMapper;

        public GetImageProjectServiceConsumer(
            IImageProjectRepository imageProjectRepository,
            IImageDataMapper imageDataMapper
            )
        {
            _imageProjectRepository = imageProjectRepository;
            _imageDataMapper = imageDataMapper;
        }

        public async Task Consume(ConsumeContext<IGetImagesProjectRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetImages, context.Message);

            await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
        }

        private object GetImages(IGetImagesProjectRequest request)
        {
            List<DbImagesProject> imagesProjects = _imageProjectRepository.Get(request.ImageIds);
            List<ImageData> imageData = new();

            foreach (DbImagesProject imagesProject in imagesProjects)
            {
                imageData.Add(_imageDataMapper.Map(imagesProject));
            }

            return IGetImagesResponse.CreateObj(imageData);
        }
    }
}
