﻿using System;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces
{
  [AutoInject]
  public interface IGetImageMessageCommand
  {
    OperationResultResponse<ImageResponse> Execute(Guid parentId);
  }
}
