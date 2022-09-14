using System;

namespace LT.DigitalOffice.ImageService.Models.Db;

public class DbImage
{
  public Guid Id { get; set; }
  public Guid? ParentId { get; set; }
  public string Name { get; set; }
  public string Content { get; set; }
  public string Extension { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public Guid CreatedBy { get; set; }
}
