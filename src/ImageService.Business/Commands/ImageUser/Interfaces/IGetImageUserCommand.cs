using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces
{
    [AutoInject]
    public interface IGetImageUserCommand
    {
        OperationResultResponse<ImageUserResponse> Execute(Guid parentId);
    }
}
