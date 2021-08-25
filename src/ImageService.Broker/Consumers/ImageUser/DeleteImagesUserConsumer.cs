using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageUser
{
    public class DeleteImagesUserConsumer : IConsumer<IDeleteImagesUserRequest>
    {
        private readonly IImageUserRepository _repository;

        public DeleteImagesUserConsumer (IImageUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<IDeleteImagesUserRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(DeleteImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object DeleteImages(IDeleteImagesUserRequest request)
        {
            return _repository.Delete(request.ImageIds);
        }
    }
}
