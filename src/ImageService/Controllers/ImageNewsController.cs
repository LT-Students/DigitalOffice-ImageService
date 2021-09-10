using System;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ImageNewsController : ControllerBase
  {
    [HttpGet("get")]
    public OperationResultResponse<ImageResponse> Get(
      [FromServices] IGetImageNewsCommand command,
      [FromQuery] Guid imageId)
    {
      return command.Execute(imageId);
    }

    [HttpPost("create")]
    public OperationResultResponse<CreateImageNewsResponse> Create(
      [FromServices] ICreateImageNewsCommand command,
      [FromBody] CreateImageRequest request)
    {
      return command.Execute(request);
    }
  }
}
