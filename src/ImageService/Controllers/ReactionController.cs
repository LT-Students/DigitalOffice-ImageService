using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageReaction.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ReactionController : ControllerBase
  {
    [HttpPost("create")]
    public async Task<OperationResultResponse<CreateImageReactionResponse>> CreateAsync(
      [FromServices] ICreateImageReactionCommand command,
      [FromBody] CreateImageRequest request)
    {
      return await command.ExecuteAsync(request);
    }

    [HttpGet("find")]
    public async Task<FindResultResponse<ReactionInfo>> FindAsync(
      [FromServices] IFindImageReactionCommand command,
      [FromQuery] FindReactionFilter findReactionFilter)
    {
      return await command.ExecuteAsync(findReactionFilter);
    }
  }
}
