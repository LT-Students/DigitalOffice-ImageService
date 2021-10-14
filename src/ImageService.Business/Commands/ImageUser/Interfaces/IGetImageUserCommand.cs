using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces
{
  [AutoInject]
  public interface IGetImageUserCommand
  {
    Task<OperationResultResponse<ImageResponse>> Execute(Guid parentId);
  }
}
