﻿using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces
{
  [AutoInject]
  public interface IImageResponseMapper
  {
    ImageResponse Map(DbImage dbImage);
  }
}
