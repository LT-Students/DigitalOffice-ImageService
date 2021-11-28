using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Business.Commands.Interfaces
{
  [AutoInject]
  public interface IGetFileImageCommand
  {
    Task<(byte[] content, string extension)> ExecuteAsync(Guid imageId, ImageSource source);
  }
}
