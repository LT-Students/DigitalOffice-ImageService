using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Helpers;
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
    public class CreateImageProjectServiceConsumer : IConsumer<ICreateImagesProjectRequest>
    {
        private readonly IImageProjectRepository _imageProjectRepository;
        private readonly IDbImageProjectMapper _mapper;
        private readonly IResizeImageHelper _helper;

        public CreateImageProjectServiceConsumer(
            IImageProjectRepository imageProjectRepository,
            IDbImageProjectMapper mapper,
            IResizeImageHelper helper)
        {
            _imageProjectRepository = imageProjectRepository;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task Consume(ConsumeContext<ICreateImagesProjectRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(CreateImages, context.Message);

            await context.RespondAsync<IOperationResult<ICreateImagesResponse>>(response);
        }

        private object CreateImages(ICreateImagesProjectRequest request)
        {
            if (request.CreateImagesData == null)
            {
                return null;
            }

            List<Guid> previewIds = new();
            List<DbImagesProject> images = new();
            DbImagesProject dbImageProject;
            DbImagesProject dbPreviewImageProject;
            string resizedContent;

            foreach (CreateImageData createImage in request.CreateImagesData)
            {
                dbImageProject = _mapper.Map(createImage);
                resizedContent = _helper.Resize(createImage.Content, createImage.Extension);

                if (string.IsNullOrEmpty(resizedContent))
                {
                    dbImageProject.ParentId = dbImageProject.Id;
                    previewIds.Add(dbImageProject.Id);
                }
                else
                {
                    dbPreviewImageProject = _mapper.Map(createImage, dbImageProject.ParentId, resizedContent);
                    images.Add(dbPreviewImageProject);
                    previewIds.Add(dbPreviewImageProject.Id);
                }

                images.Add(dbImageProject);
            }

            if (_imageProjectRepository.Create(images) == null)
            {
                return ICreateImagesResponse.CreateObj(null);
            }

            return ICreateImagesResponse.CreateObj(previewIds);
        }
    }
}