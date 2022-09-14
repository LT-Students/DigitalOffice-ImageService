using System;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;

[AutoInject]
public interface ICreateReactionCommand
{
  Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionRequest request);
}

