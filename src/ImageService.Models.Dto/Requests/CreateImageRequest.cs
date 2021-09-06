namespace LT.DigitalOffice.ImageService.Models.Dto.Requests
{
  public record CreateImageRequest
  {
    public string Name { get; set; }
    public string Content { get; set; }
    public string Extension { get; set; }
    public bool EnablePrewiew { get; set; }
  }
}
