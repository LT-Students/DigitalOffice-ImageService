using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ReactionGroup
{
  public class GetReactionGroupCommand : IGetReactionGroupCommand
  {
    private readonly IGetReactionGroupResponseMapper _mapper;
    private readonly IReactionGroupRepository _repository;
    private readonly IResponseCreator _responseCreator;

    public GetReactionGroupCommand(
      IGetReactionGroupResponseMapper mapper,
      IReactionGroupRepository repository,
      IResponseCreator responseCreator)
    {
      _mapper = mapper;
      _repository = repository;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<GetReactionGroupResponse>> ExecuteAsync(GetReactionGroupFilter filter)
    {
      OperationResultResponse<GetReactionGroupResponse> response = new();
      response.Body = _mapper.Map(await _repository.GetAsync(filter));

      return response.Body is null
        ? _responseCreator.CreateFailureResponse<GetReactionGroupResponse>(HttpStatusCode.NotFound)
        : response;
    }
  }
}
