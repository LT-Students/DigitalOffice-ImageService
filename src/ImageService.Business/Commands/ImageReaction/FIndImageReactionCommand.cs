using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.ImageService.Business.Commands.ImageReaction.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Models.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Models;
using LT.DigitalOffice.ImageService.Models.Dto.Requests.Filters;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageReaction
{
  public class FIndImageReactionCommand : IFindImageReactionCommand
  {
    private readonly IBaseFindFilterValidator _baseFindValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly IImageRepository _repository;
    private readonly IReactionInfoMapper _mapper;

    public FIndImageReactionCommand(
      IBaseFindFilterValidator baseValidator,
      IResponseCreator responseCreator,
      IImageRepository imageRepository,
      IReactionInfoMapper mapper)
    {
      _baseFindValidator = baseValidator;
      _responseCreator = responseCreator;
      _repository = imageRepository;
      _mapper = mapper;
    }

    public async Task<FindResultResponse<ReactionInfo>> ExecuteAsync(FindReactionFilter findReactionFilter)
    {
      ValidationResult validationResult = await _baseFindValidator.ValidateAsync(findReactionFilter);
      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureFindResponse<ReactionInfo>(HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      FindResultResponse<ReactionInfo> response = new();
      (List<DbImage> dbRectionList, int totalCount) = await _repository.FindReactionAsync(findReactionFilter);

      response.Body = dbRectionList?.Select(dbImage => _mapper.Map(dbImage)).ToList();
      response.TotalCount = totalCount;

      return response;
    }
  }
}
