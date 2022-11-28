using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses
{
  public record CreateImageWikiNewsResponse
  {
    public Guid ImageId { get; set; }
    public Guid? PreviewId { get; set; }
  }
}
