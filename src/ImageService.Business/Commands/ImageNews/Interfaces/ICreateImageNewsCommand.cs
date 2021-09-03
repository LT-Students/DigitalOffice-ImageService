using System;
using System.Collections.Generic;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces
{
  [AutoInject]
  public interface ICreateImageNewsCommand
  {
    OperationResultResponse<List<Guid>> Execute(CreateImageRequest request);
  }
}
