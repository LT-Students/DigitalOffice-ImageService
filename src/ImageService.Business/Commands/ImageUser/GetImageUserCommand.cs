using LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser
{
    public class GetImageUserCommand : IGetImageUserCommand
    {
        private readonly IImageUserRepository _imageUserRepository;
        private readonly IImageDataResponseMapper _imageUserResponseMapper;

        public GetImageUserCommand(
            IImageUserRepository imageUserRepository,
            IImageDataResponseMapper imageUserResponseMapper)
        {
            _imageUserRepository = imageUserRepository;
            _imageUserResponseMapper = imageUserResponseMapper;
        }

        public OperationResultResponse<ImageDataResponse> Execute(Guid imageId)
        {
            OperationResultResponse<ImageDataResponse> response = new();

            DbImageUser dbImageUser = _imageUserRepository.Get(imageId);

            if (dbImageUser == null)
            {
                response.Body = null;
                response.Status = OperationResultStatusType.Failed;
                response.Errors.Add("Image was not found.");
                return response;
            }

            response.Body = _imageUserResponseMapper.Map(dbImageUser);
            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
