using LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Exceptions.Models;
using LT.DigitalOffice.Kernel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser
{
    public class GetImageUserCommand : IGetImageUserCommand
    {
        private readonly IImageUserRepository _imageUserRepository;
        private readonly IImageUserResponseMapper _imageUserResponseMapper;
        public GetImageUserCommand(
            IImageUserRepository imageUserRepository,
            IImageUserResponseMapper imageUserResponseMapper)
        {
            _imageUserRepository = imageUserRepository;
            _imageUserResponseMapper = imageUserResponseMapper;
        }

        public OperationResultResponse<ImageUserResponse> Execute(Guid parentId)
        {
            OperationResultResponse<ImageUserResponse> response = new();

            DbImagesUser dbImageUser = _imageUserRepository.Get(parentId);

            if (dbImageUser == null)
            {
                response.Body = null;
                response.Errors.Add("Image was not found.");
                return response;
            }

            response.Body = _imageUserResponseMapper.Map(dbImageUser);
            response.Status = OperationResultStatusType.FullSuccess;

            return response;
        }
    }
}
