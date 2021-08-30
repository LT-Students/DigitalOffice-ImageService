﻿using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.Models.Broker.Enums;
using LT.DigitalOffice.Models.Broker.Requests.Image;
using MassTransit;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Broker.Consumers
{
    public class RemoveImagesConsumer : IConsumer<IRemoveImagesRequest>
    {
        private readonly IImageMessageRepository _imageMessageRepository;
        private readonly IImageNewsRepository _imageNewsRepository;
        private readonly IImageProjectRepository _imageProjectRepository;
        private readonly IImageUserRepository _imageUserRepository;

        public RemoveImagesConsumer(
            IImageMessageRepository imageMessageRepository,
            IImageNewsRepository imageNewsRepository,
            IImageProjectRepository imageProjectRepository,
            IImageUserRepository imageUserRepository)
        {
            _imageMessageRepository = imageMessageRepository;
            _imageNewsRepository = imageNewsRepository;
            _imageProjectRepository = imageProjectRepository;
            _imageUserRepository = imageUserRepository;
        }

        public async Task Consume(ConsumeContext<IRemoveImagesRequest> context)
        {
            object response = OperationResultWrapper.CreateResponse(RemoveImages, context.Message);

            await context.RespondAsync<IOperationResult<bool>>(response);
        }

        private object RemoveImages(IRemoveImagesRequest request)
        {
            switch (request.ImageSource)
            {
                case ImageSource.Message:
                    return _imageMessageRepository.Remove(request.ImagesIds);
                case ImageSource.News:
                    return _imageNewsRepository.Remove(request.ImagesIds);
                case ImageSource.Project:
                    return _imageProjectRepository.Remove(request.ImagesIds);
                case ImageSource.User:
                    return _imageUserRepository.Remove(request.ImagesIds);
                default:
                    return null;
            }
        }
    }
}