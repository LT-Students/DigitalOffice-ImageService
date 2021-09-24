using System;
using LT.DigitalOffice.ImageService.Business.Commands.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class FileImageController : ControllerBase
  {
    [HttpGet("get")]
    public FileResult Get(
      [FromServices] IGetFileImageCommand command,
      [FromQuery] Guid imageId,
      [FromQuery] ImageType type)
    {
      (byte[] content, string extension) = command.Execute(imageId, type);

      return File(content, extension);
    }
  }
}
