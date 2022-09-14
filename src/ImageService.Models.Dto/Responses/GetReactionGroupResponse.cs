using System.Collections.Generic;
using LT.DigitalOffice.ImageService.Models.Dto.Models;

namespace LT.DigitalOffice.ImageService.Models.Dto.Responses;

public record GetReactionGroupResponse
{
  public string Name { get; set; }
  public bool IsActive { get; set; }
  public List<ReactionInfo> ReactionsInfo { get; set; }
}
