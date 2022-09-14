using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests;

public record CreateReactionRequest : CreateReactionWithGroupRequest
{
 /* [Required]
  public string Name { get; set; }
  public string Unicode { get; set; }
  [Required]
  public string Content { get; set; }
  [Required]
  public string Extension { get; set; }*/
  public Guid? GroupId { get; set; }      //when Groups will be added by front change to Guid
}
