using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageUser
{
    public class CreateImagesUserConsumer : IConsumer<ICreateImagesUserRequest>
    {
        private readonly IImageUserRepository _repository;
        private readonly IDbImageUserMapper _mapper;

        public CreateImagesUserConsumer(IImageUserRepository repository, IDbImageUserMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<ICreateImagesUserRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateImages, context.Message);

            await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
        }

        private object CreateImages(ICreateImagesUserRequest request)
        {
            List<CreateImageData> createImage = request.CreateImagesData;
            List<DbImagesUser> images = new();

            foreach (CreateImageData x in createImage)
            {
                images.AddRange(CreateHQ(x));
            }

            return ICreateImagesResponse.CreateObj(_repository.Create(images));
        }

        public List<DbImagesUser> CreateHQ(CreateImageData createImageData)
        {
            List<DbImagesUser> db = new();
            var highQ = _mapper.Map(createImageData);
            db.Add(highQ);
            var createImageData1 = new CreateImageData(createImageData.Name, "преобразованная картинка", createImageData.Extension, createImageData.CreatedBy);
            var lowQ = _mapper.Map(createImageData1, highQ.Id);
            db.Add(lowQ);

            return db;
        }
    }
}
