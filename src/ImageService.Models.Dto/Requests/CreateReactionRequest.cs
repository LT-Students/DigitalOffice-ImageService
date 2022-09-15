using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests;

public record CreateReactionRequest : CreateReactionWithGroupRequest
{
  public Guid? GroupId { get; set; }      //when Groups will be added by front change to Guid
}
