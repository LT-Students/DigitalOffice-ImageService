using System;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces
{
  [AutoInject]
  public interface IGetImageProjectCommand
  {
    OperationResultResponse<ImageResponse> Execute(Guid parentId);
  }
}
