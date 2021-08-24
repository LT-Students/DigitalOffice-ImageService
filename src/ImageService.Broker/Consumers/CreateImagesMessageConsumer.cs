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

            List<DbImagesMessage> dbImages = new();
            Guid previewId;
            List<Guid> previewIds = new();
            string resizedContent;

            foreach (CreateImageData createImage in request.CreateImagesData)
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

            if (_imageMessageRepository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
