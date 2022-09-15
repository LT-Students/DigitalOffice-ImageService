using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction;

public class GetReactionCommand : IGetReactionCommand
{
  private readonly IReactionRepository _repository;
  private readonly IGetReactionResponseMapper _mapper;
  private readonly IResponseCreator _responseCreator;

  public GetReactionCommand(
    IReactionRepository repository,
    IGetReactionResponseMapper mapper,
    IResponseCreator responseCreator)
  {
    _repository = repository;
    _mapper = mapper;
    _responseCreator = responseCreator;
  }

  public async Task<OperationResultResponse<GetReactionResponse>> ExecuteAsync(Guid reactionId)
  {
    OperationResultResponse<GetReactionResponse> response = new(body: _mapper.Map(await _repository.GetAsync(reactionId)));

    return response.Body is null
      ? _responseCreator.CreateFailureResponse<GetReactionResponse>(HttpStatusCode.NotFound)
      : response;
  }
}
