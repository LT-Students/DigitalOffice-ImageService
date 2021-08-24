using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageNews
{
    public class DeleteImagesNewsConsumer : IConsumer<IDeleteImagesNewsRequest>
    {
        private readonly IImageNewsRepository _repository;

        public DeleteImagesNewsConsumer(IImageNewsRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<IDeleteImagesNewsRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(DeleteImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object DeleteImages(IDeleteImagesNewsRequest request)
        {
            _repository.Delete(request.ImageIds);

            return true;
        }
    }
}
