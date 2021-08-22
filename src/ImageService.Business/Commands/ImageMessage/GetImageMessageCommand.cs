using LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.Message;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageMessage
{
    public class GetImageMessageCommand : IGetImageMessageCommand
    {
        private readonly IImageMessageRepository _imageMessageRepository;
        private readonly IImageMessageResponseMapper _imageMessageResponseMapper;
        public GetImageMessageCommand(
            IImageMessageRepository imageMessageRepository,
            IImageMessageResponseMapper imageMessageResponseMapper)
        {
            _imageMessageRepository = imageMessageRepository;
            _imageMessageResponseMapper = imageMessageResponseMapper;
        }

        public OperationResultResponse<ImageMessageResponse> Execute(Guid parentId)
        {
            OperationResultResponse<ImageMessageResponse> response = new();

            DbImageMessage dbImageMessage = _imageMessageRepository.Get(parentId);
            if (dbImageMessage == null)
            {
                throw new NotFoundException($"Image was not found.");
            }

            response.Body = _imageMessageResponseMapper.Map(dbImageMessage);
            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
