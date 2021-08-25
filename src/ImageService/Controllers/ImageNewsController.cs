using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.ImageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageNewsController
    {
        [HttpGet("get")]
        public OperationResultResponse<ImageDataResponse> Get(
            [FromServices] IGetImageNewsCommand command,
            [FromQuery] Guid imageId)
        {
            return command.Execute(imageId);
        }
    }
}
