using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageNews
{
    public class CreateImagesNewsConsumer : IConsumer<ICreateImagesNewsRequest>
    {
        public async Task Consume(ConsumeContext<ICreateImagesNewsRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
