using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters
{
  public record FindReactionFilter : BaseFindFilter
  {
    [FromQuery(Name = "nameIncludeSubstring")]
    public string NameIncludeSubstring { get; set; }

    [FromQuery(Name = "unicodeIncludeSubstring")]
    public string UnicodeIncludeSubstring { get; set; }

    [FromQuery(Name = "isAscendingSort")]
    public bool IsAscendingSort { get; set; } = true;

    [FromQuery(Name = "isActive")]
    public bool? IsActive { get; set; }
  }
}
