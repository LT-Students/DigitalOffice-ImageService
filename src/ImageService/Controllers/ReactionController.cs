using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReactionController : ControllerBase
{
  [HttpPost("create")]
  public async Task<OperationResultResponse<Guid?>> CreateAsync(
    [FromServices] ICreateReactionCommand command,
    [FromBody] CreateReactionRequest request)
  {
    return await command.ExecuteAsync(request);
  }

  [HttpGet("get")]
  public async Task<OperationResultResponse<GetReactionResponse>> GetAsync(
    [FromServices] IGetReactionCommand command,
    [FromQuery] Guid reactionId)
  {
    return await command.ExecuteAsync(reactionId);
  }

  [HttpGet("find")]
  public async Task<FindResultResponse<ReactionInfo>> FindAsync(
    [FromServices] IFindReactionCommand command,
    [FromQuery] FindReactionFilter findReactionFilter)
  {
    return await command.ExecuteAsync(findReactionFilter);
  }
}
