using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces
{
    [AutoInject]
    public interface IGetImageProjectCommand
    {
        OperationResultResponse<ImageDataResponse> Execute(Guid parentId);
    }
}
