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

        public CreateImagesUserConsumer(
            IImageUserRepository repository,
            IDbImageUserMapper mapper,
            IResizeImageHelper helper)
        {
            _repository = repository;
            _helper = helper;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ICreateImagesUserRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(CreateImages, context.Message);

            await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
        }

        private object CreateImages(ICreateImagesUserRequest request)
        {
            if (request.CreateImagesData == null)
            {
                return null;
            }

            List<DbImagesUser> dbImages = new();
            List<Guid> previewIds = new();
            DbImagesUser dbImageUser;
            DbImagesUser dbPrewiewImageUser;
            string resizedContent;

            foreach (CreateImageData createImage in request.CreateImagesData)
            {
                dbImageUser = _mapper.Map(createImage);
                resizedContent = _helper.Resize(createImage.Content, createImage.Extension);

                if (string.IsNullOrEmpty(resizedContent))
                {
                    dbImageUser.ParentId = dbImageUser.Id;
                    previewIds.Add(dbImageUser.Id);
                }
                else
                {
                    dbPrewiewImageUser = _mapper.Map(createImage, dbImageUser.Id, resizedContent);
                    dbImages.Add(dbPrewiewImageUser);
                    previewIds.Add(dbPrewiewImageUser.Id);
                }

                dbImages.Add(dbImageUser);
            }

            if (_repository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
