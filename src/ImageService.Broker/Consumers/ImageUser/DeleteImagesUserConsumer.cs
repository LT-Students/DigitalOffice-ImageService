using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageUser
{
    public class DeleteImagesUserConsumer : IConsumer<IDeleteImagesUserRequest>
    {
        public async Task Consume(ConsumeContext<IDeleteImagesUserRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
