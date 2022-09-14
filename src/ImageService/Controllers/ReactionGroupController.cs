using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ImageService.Business.Commands.ReactionGroup.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers;

[ApiController]
[Route("[controller]")]
public class ReactionGroupController : ControllerBase
{
  [HttpPost("create")]
  public async Task<OperationResultResponse<Guid?>> CreateAsync(
    [FromServices] ICreateReactionGroupCommand command,
    [FromBody] CreateReactionGroupRequest request)
  {
    return await command.ExecuteAsync(request);
  }

  [HttpGet("get")]
  [ResponseCache(VaryByQueryKeys = new[] { "*" }, Duration = 300)]
  public async Task<OperationResultResponse<GetReactionGroupResponse>> GetAsync(
    [FromServices] IGetReactionGroupCommand command,
    [FromQuery] GetReactionGroupFilter filter)
  {
    return await command.ExecuteAsync(filter);
  }
}
