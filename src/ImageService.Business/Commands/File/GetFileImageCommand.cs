using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.StaticFiles;

namespace LT.DigitalOffice.ImageService.Business.Commands
{
  public class GetFileImageCommand : IGetFileImageCommand
  {
    private readonly IImageMessageRepository _messageRepository;
    private readonly IImageNewsRepository _newsRepository;
    private readonly IImageProjectRepository _projectRepository;
    private readonly IImageUserRepository _userRepository;

    public GetFileImageCommand(
      IImageMessageRepository messageRepository,
      IImageNewsRepository newsRepository,
      IImageProjectRepository projectRepository,
      IImageUserRepository userRepository)
    {
      _messageRepository = messageRepository;
      _newsRepository = newsRepository;
      _projectRepository = projectRepository;
      _userRepository = userRepository;
    }

    public async Task<(byte[] content, string extension)> Execute(Guid imageId, ImageSource source)
    {
      string content = null;
      string extension = null;

      if (source == ImageSource.Message)
      {
        DbImageMessage dbImageMessage = await _messageRepository.GetAsync(imageId);
        content = dbImageMessage.Content;
        extension = dbImageMessage.Extension;
      }
      else if (source == ImageSource.News)
      {
        DbImageNews dbImageNews = await _newsRepository.GetAsync(imageId);
        content = dbImageNews.Content;
        extension = dbImageNews.Extension;
      }
      else if (source == ImageSource.Project)
      {
        DbImageProject dbImageProject = await _projectRepository.GetAsync(imageId);
        content = dbImageProject.Content;
        extension = dbImageProject.Extension;
      }
      else if (source == ImageSource.User)
      {
        DbImageUser dbImageUser = await _userRepository.GetAsync(imageId);
        content = dbImageUser.Content;
        extension = dbImageUser.Extension;
      }

      new FileExtensionContentTypeProvider().TryGetContentType(extension, out var contentType);

      return (Convert.FromBase64String(content), contentType);
    }
  }
}
