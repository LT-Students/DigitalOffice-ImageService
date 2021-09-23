using System;
using LT.DigitalOffice.ImageService.Models.Dto.Enums;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Business.Commands.Interfaces
{
  [AutoInject]
  public interface IGetFileImageCommand
  {
    (byte[] content, string extension) Execute(Guid imageId, ImageType directory);
  }
}
