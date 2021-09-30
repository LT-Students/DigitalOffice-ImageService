using System;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Business.Commands.Interfaces
{
  [AutoInject]
  public interface IGetFileImageCommand
  {
    (byte[] content, string extension) Execute(Guid imageId, ImageSource source);
  }
}
