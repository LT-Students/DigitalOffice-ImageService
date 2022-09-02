using System;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters
{
  public record GetReactionGroupFilter
  {
    [FromQuery(Name = "groupId")]
    public Guid GroupId { get; set; }

    [FromQuery(Name = "includeReactions")]
    public bool IncludeReactions { get; set; } = true;

    [FromQuery(Name = "includeActiveReactions")]
    public bool? IncludeActiveReactions { get; set; }
  }
}
