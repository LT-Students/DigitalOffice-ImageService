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

namespace LT.DigitalOffice.ImageService.Broker.Consumers.ImageNews
{
    public class CreateImagesNewsConsumer : IConsumer<ICreateImagesNewsRequest>
    {
        private readonly IImageNewsRepository _repository;
        private readonly IResizeImageHelper _helper;
        private readonly IDbImageNewsMapper _mapper;

        public CreateImagesNewsConsumer(
            IImageNewsRepository repository,
            IDbImageNewsMapper mapper,
            IResizeImageHelper helper)
        {
            _repository = repository;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task Consume(ConsumeContext<ICreateImagesNewsRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(CreateImages, context.Message);

            await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
        }

        private object CreateImages(ICreateImagesNewsRequest request)
        {
            if (request.CreateImagesData == null)
            {
                return null;
            }

            List<DbImagesNews> dbImages = new();
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

            if (_repository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
