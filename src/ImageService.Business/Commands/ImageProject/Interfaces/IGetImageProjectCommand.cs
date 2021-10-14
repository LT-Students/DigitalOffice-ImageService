using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces
{
  [AutoInject]
  public interface IGetImageProjectCommand
  {
    Task<OperationResultResponse<ImageResponse>> Execute(Guid parentId);
  }
}
