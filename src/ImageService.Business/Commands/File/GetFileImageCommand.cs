using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.StaticFiles;

namespace LT.DigitalOffice.ImageService.Business.Commands;

public class GetFileImageCommand : IGetFileImageCommand
{
  private readonly IImageRepository _repository;

  public GetFileImageCommand(
    IImageRepository messageRepository)
  {
    _repository = messageRepository;
  }

  public async Task<(byte[] content, string extension)> ExecuteAsync(Guid imageId, ImageSource source)
  {
    DbImage dbImageMessage = await _repository.GetAsync(source, imageId);

    new FileExtensionContentTypeProvider().TryGetContentType(dbImageMessage.Extension, out var contentType);

    return (Convert.FromBase64String(dbImageMessage.Content), contentType);
  }
}
