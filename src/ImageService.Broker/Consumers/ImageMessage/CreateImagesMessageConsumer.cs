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

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class CreateImagesMessageConsumer : IConsumer<ICreateImagesMessageRequest>
    {
        private readonly IImageMessageRepository _imageMessageRepository;
        private readonly IDbImageMessageMapper _mapper;
        private readonly IResizeImageHelper _helper;

        public CreateImagesMessageConsumer(
            IImageMessageRepository imageMessageRepository,
            IResizeImageHelper helper,
            IDbImageMessageMapper mapper)
        {
            _imageMessageRepository = imageMessageRepository;
            _helper = helper;
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<ICreateImagesMessageRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(CreateImages, context.Message);

            await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
        }

        private object CreateImages(ICreateImagesMessageRequest request)
        {
            if (request.CreateImagesData == null)
            {
                return null;
            }

            List<DbImageMessage> dbImages = new();
            List<Guid> previewIds = new();
            DbImageMessage dbImageMessage;
            DbImageMessage dbPreviewImageMessage;
            string resizedContent;

            foreach (CreateImageData createImage in request.CreateImagesData)
            {
                dbImageMessage = _mapper.Map(createImage);
                resizedContent = _helper.Resize(createImage.Content, createImage.Extension);

                if (string.IsNullOrEmpty(resizedContent))
                {
                    dbImageMessage.ParentId = dbImageMessage.Id;
                    previewIds.Add(dbImageMessage.Id);
                }
                else
                {
                    dbPreviewImageMessage = _mapper.Map(createImage, dbImageMessage.Id, resizedContent);
                    dbImages.Add(dbPreviewImageMessage);
                    previewIds.Add(dbPreviewImageMessage.Id);
                }

                dbImages.Add(dbImageMessage);
            }

            if (_imageMessageRepository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
