using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests;

public record CreateReactionRequest : CreateReactionWithGroupRequest
{
  public Guid GroupId { get; set; }
}
