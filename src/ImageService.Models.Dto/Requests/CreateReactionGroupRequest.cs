using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests
{
  public record CreateReactionGroupRequest
  {
    [Required]
    public string Name { get; set; }
  }
}
