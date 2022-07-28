using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests
{
  public record CreateImageRequest
  {
    [Required]
    public string Name { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public string Extension { get; set; }
    public bool EnablePreview { get; set; }
  }
}
