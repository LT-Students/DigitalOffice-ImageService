using LT.DigitalOffice.Kernel.Requests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters
{
  public record FindReactionFilter : BaseFindFilter
  {
    [FromQuery(Name = "isPreview")]
    public bool? IsPreview { get; set; } = null;

    [FromQuery(Name = "nameIncludeSubstring")]
    public string NameIncludeSubstring { get; set; }

    [FromQuery(Name = "isAscendingSort")]
    public bool? IsAscendingSort { get; set; }
  }
}
