using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces
{
  [AutoInject]
  public interface IFindReactionCommand
  {
    Task<FindResultResponse<ReactionInfo>> ExecuteAsync(FindReactionFilter findReactionFilter);
  }
}
