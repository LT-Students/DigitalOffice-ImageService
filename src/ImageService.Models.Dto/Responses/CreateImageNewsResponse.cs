using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses;

public record CreateImageNewsResponse
{
  public Guid ImageId { get; set; }
  public Guid? PreviewId { get; set; }
}
