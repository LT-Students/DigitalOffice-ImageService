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
            List<Guid> previewIds = new();
            DbImagesNews dbImageNews;
            DbImagesNews dbPrewiewImageNews;
            string resizedContent;

            foreach (CreateImageData createImage in request.CreateImagesData)
            {
                dbImageNews = _mapper.Map(createImage);
                resizedContent = _helper.Resize(createImage.Content, createImage.Extension);

                if (string.IsNullOrEmpty(resizedContent))
                {
                    dbImageNews.ParentId = dbImageNews.Id;
                    previewIds.Add(dbImageNews.Id);
                }
                else
                {
                    dbPrewiewImageNews = _mapper.Map(createImage, dbImageNews.Id, resizedContent);
                    dbImages.Add(dbPrewiewImageNews);
                    previewIds.Add(dbPrewiewImageNews.Id);
                }

                dbImages.Add(dbImageNews);
            }

            if (_repository.Create(dbImages) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}
