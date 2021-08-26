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

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageNews
{
    public class GetImagesNewsConsumer : IConsumer<IGetImagesNewsRequest>
    {
        private readonly IImageNewsRepository _repository;

        public async Task Consume(ConsumeContext<IGetImagesNewsRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetImages, context.Message);

            await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
        }

        private object GetImages(IGetImagesNewsRequest request)
        {
            List<DbImageNews> dbImages = _repository.Get(request.ImageIds);

            return IGetImagesResponse.CreateObj(
                dbImages
                    .Select(dbImagesNews => new ImageData(
                        dbImagesNews.Id,
                        dbImagesNews.ParentId,
                        null,
                        dbImagesNews.Content,
                        dbImagesNews.Extension,
                        dbImagesNews.Name))
                    .ToList());
        }
    }
}
