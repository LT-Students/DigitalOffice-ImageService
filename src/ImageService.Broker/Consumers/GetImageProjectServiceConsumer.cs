using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
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

        public GetImageProjectServiceConsumer(IImageProjectRepository imageProjectRepository)
        {
            _imageProjectRepository = imageProjectRepository;
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
                imageData.Add(new ImageData(imagesProject.Id, imagesProject.ParentId, null, imagesProject.Content,
                    imagesProject.Extension, imagesProject.Name));
            }

            return IGetImagesResponse.CreateObj(imageData);
        }
    }
}
