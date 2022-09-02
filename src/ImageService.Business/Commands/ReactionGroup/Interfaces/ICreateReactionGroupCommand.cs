using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ReactionGroup.Interfaces
{
  [AutoInject]
  public interface ICreateReactionGroupCommand
  {
    Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionGroupRequest request);
  }
}
