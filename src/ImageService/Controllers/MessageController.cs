﻿using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MessageController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<OperationResultResponse<ImageResponse>> Get(
      [FromServices] IGetImageMessageCommand command,
      [FromQuery] Guid imageId)
    {
      return await command.ExecuteAsync(imageId);
    }
  }
}
