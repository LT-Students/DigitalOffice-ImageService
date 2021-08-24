using LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.ImageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageMessageController : ControllerBase
    {
        [HttpGet("get")]
        public OperationResultResponse<ImageDataResponse> Get(
            [FromServices] IGetImageMessageCommand command,
            [FromQuery] Guid imageId)
        {
            return command.Execute(imageId);
        }
    }
}
