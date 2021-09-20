using System;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces
{
  [AutoInject]
  public interface IGetImageUserCommand
  {
    OperationResultResponse<ImageResponse> Execute(Guid parentId);
  }
}
