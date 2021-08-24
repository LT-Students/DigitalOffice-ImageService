using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class GetImagesMessageConsumer : IConsumer<IGetImagesMessageRequest>
    {
        private readonly IImageMessageRepository _imageMessageRepository;

        public GetImagesMessageConsumer(
            IImageMessageRepository imageMessageRepository)
        {
            _imageMessageRepository = imageMessageRepository;
        }

        public async Task Consume(ConsumeContext<IGetImagesMessageRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(GetImages, context.Message);

            await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
        }

        private object GetImages(IGetImagesMessageRequest request)
        {
            List<DbImagesMessage> dbImages = _imageMessageRepository.Get(request.ImageIds);

            return IGetImagesResponse.CreateObj(
                dbImages
                    .Select(dbImage => new ImageData(
                        dbImage.Id,
                        dbImage.ParentId,
                        null,
                        dbImage.Content,
                        dbImage.Extension,
                        dbImage.Name))
                    .ToList());
        }
    }
}
