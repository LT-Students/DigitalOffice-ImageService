using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageReaction.Interfaces
{
  [AutoInject]
  public interface ICreateImageReactionCommand
  {
    Task<OperationResultResponse<CreateImageReactionResponse>> ExecuteAsync(CreateImageRequest request);
  }
}
