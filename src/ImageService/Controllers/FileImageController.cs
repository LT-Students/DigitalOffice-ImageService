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
      [FromQuery] ImageType directory)
    {
      (byte[] conrent, string extension) response = command.Execute(imageId, directory);

      return File(response.conrent, response.extension);
    }
  }
}
