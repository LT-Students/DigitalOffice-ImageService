using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers;

[ApiController]
[Route("[Controller]")]
public class ImageController : ControllerBase
{
  [HttpGet("get")]
  public async Task<OperationResultResponse<ImageResponse>> GetAsync(
    [FromServices] IGetImageCommand command,
    [FromQuery] Guid imageId,
    [FromQuery] ImageSource source)
  {
    return await command.ExecuteAsync(imageId, source);
  }
}
