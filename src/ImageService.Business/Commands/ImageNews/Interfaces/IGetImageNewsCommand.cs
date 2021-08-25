using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using System;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces
{
    [AutoInject]
    public interface IGetImageNewsCommand
    {
        OperationResultResponse<ImageDataResponse> Execute(Guid imageId);
    }
}
