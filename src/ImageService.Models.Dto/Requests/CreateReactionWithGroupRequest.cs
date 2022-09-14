﻿using System;
using System.ComponentModel.DataAnnotations;

namespace LT.DigitalOffice.ImageService.Models.Dto.Requests;

public record CreateReactionWithGroupRequest
{
  [Required]
  public string Name { get; set; }
  public string Unicode { get; set; }
  [Required]
  public string Content { get; set; }
  [Required]
  public string Extension { get; set; }
}
