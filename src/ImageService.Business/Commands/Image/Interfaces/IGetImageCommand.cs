using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Business.Commands.Image.Interfaces
{
  [AutoInject]
  public interface IGetImageCommand
  {
    Task<OperationResultResponse<ImageResponse>> ExecuteAsync(Guid imageId, ImageSource source);
  }
}
