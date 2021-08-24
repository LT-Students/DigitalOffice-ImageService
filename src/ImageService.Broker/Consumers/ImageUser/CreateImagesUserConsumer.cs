using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Helpers.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Models;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using LT.DigitalOffice.Models.Broker.Responses.Image;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageUser
{
    public class CreateImagesUserConsumer : IConsumer<ICreateImagesUserRequest>
    {
        private readonly IImageUserRepository _repository;
        private readonly IResizeImageHelper _helper;
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
            if (request.CreateImagesData == null)
            {
                return null;
            }

            List<CreateImageData> createImages = request.CreateImagesData;
            List<DbImagesUser> dbImages = new();
            Guid previewId;
            List<Guid> previewIds = new();
            string resizedContent;

            foreach (CreateImageData createImage in createImages)
            {
                resizedContent = _helper.Resize(createImage.Content, createImage.Extension);

                if (string.IsNullOrEmpty(resizedContent))
                {
                    dbImages.Add(_mapper.Map(createImage, out previewId));
                    previewIds.Add(previewId);
                }
                else
                {
                    dbImages.AddRange(_mapper.Map(createImage, resizedContent, out previewId));
                    previewIds.Add(previewId);
                }
            }

            if (_repository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
