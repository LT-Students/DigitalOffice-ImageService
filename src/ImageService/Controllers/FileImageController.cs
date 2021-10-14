using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Controllers
{
  [ApiController]
  [Route("[Controller]")]
  public class FileImageController : ControllerBase
  {
    [HttpGet("get")]
    public async Task<FileResult> Get(
      [FromServices] IGetFileImageCommand command,
      [FromQuery] Guid imageId,
      [FromQuery] ImageSource source)
    {
      (byte[] content, string extension) = await command.Execute(imageId, source);

      return File(content, extension);
    }
  }
}
