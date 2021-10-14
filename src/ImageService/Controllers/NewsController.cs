﻿using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class NewsController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<ImageResponse>> Get(
      [FromServices] IGetImageNewsCommand command,
      [FromQuery] Guid imageId)
    {
      return await command.Execute(imageId);
    }

    [HttpPost("create")]
    public async Task<OperationResultResponse<CreateImageNewsResponse>> Create(
      [FromServices] ICreateImageNewsCommand command,
      [FromBody] CreateImageRequest request)
    {
      return await command.Execute(request);
    }
  }
}
