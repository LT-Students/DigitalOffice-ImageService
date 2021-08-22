using LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses.User;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.ImageService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageUserController : ControllerBase
    {
        [HttpGet("get")]
        public OperationResultResponse<ImageUserResponse> Get(
            [FromServices] IGetImageUserCommand command,
            [FromQuery] Guid imageId)
        {
            return command.Execute(imageId);
        }
    }
}