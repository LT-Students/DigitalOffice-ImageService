using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageReaction.Interfaces
{
  [AutoInject]
  public interface IFindImageReactionCommand
  {
    Task<FindResultResponse<ReactionInfo>> ExecuteAsync(FindReactionFilter findReactionFilter);
  }
}
