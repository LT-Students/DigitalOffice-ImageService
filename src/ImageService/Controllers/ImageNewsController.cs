using System;
using System.Collections.Generic;
using System.Net;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ImageNewsController : ControllerBase
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ImageNewsController(
      IHttpContextAccessor httpContextAccessor)
    {
      _httpContextAccessor = httpContextAccessor;
    }

    [HttpGet("get")]
    public OperationResultResponse<ImageDataResponse> Get(
        [FromServices] IGetImageNewsCommand command,
        [FromQuery] Guid imageId)
    {
      return command.Execute(imageId);
    }

    [HttpPost("create")]
    public OperationResultResponse<List<Guid>> Create(
    [FromServices] ICreateImageNewsCommand command,
    [FromBody] CreateImageRequest request)
    {
      return command.Execute(request);
    }
  }
}
