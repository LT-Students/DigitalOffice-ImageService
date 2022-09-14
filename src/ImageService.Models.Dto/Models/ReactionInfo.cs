﻿using System;

namespace LT.DigitalOffice.ImageService.Models.Dto.Models;

public record ReactionInfo
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Unicode { get; set; }
  public string Content { get; set; }
  public string Extension { get; set; }
  public Guid GroupId { get; set; }
  public bool IsActive { get; set; }
}
