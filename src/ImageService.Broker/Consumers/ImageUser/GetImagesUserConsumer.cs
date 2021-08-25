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

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageUser
{
    public class GetImagesUserConsumer : IConsumer<IGetImagesUserRequest>
    {
        private readonly IImageUserRepository _repository;

        public GetImagesUserConsumer (IImageUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<IGetImagesUserRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(GetImages, context.Message);

            await context.RespondAsync<IOperationResult<IGetImagesResponse>>(response);
        }

        private object GetImages(IGetImagesUserRequest request)
        {
            List<DbImagesUser> dbImages = _repository.Get(request.ImageIds);

            return IGetImagesResponse.CreateObj(
                dbImages
                    .Select(dbImagesUser => new ImageData(
                        dbImagesUser.Id,
                        dbImagesUser.ParentId,
                        null,
                        dbImagesUser.Content,
                        dbImagesUser.Extension,
                        dbImagesUser.Name))
                    .ToList());
        }
    }
}
