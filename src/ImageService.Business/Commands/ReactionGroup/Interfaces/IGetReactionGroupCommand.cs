using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;

[AutoInject]
public interface IGetReactionGroupCommand
{
  Task<OperationResultResponse<GetReactionGroupResponse>> ExecuteAsync(GetReactionGroupFilter filter);
}
