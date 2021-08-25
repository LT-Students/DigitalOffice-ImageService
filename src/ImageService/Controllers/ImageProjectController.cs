﻿using LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.ImageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageProjectController : ControllerBase
    {
        [HttpGet("get")]
        public OperationResultResponse<ImageDataResponse> Get(
            [FromServices] IGetImageProjectCommand command,
            [FromQuery] Guid imageId)
        {
            return command.Execute(imageId);
        }
    }
}