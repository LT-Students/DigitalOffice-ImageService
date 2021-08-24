using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces
{
    [AutoInject]
    public interface IGetImageMessageCommand
    {
        OperationResultResponse<ImageDataResponse> Execute(Guid parentId);
    }
}
