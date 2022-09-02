using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.ImageService.Business.Commands.ReactionGroup.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.ReactionGroup.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ReactionGroup
{
  public class CreateReactionGroupCommand : ICreateReactionGroupCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateReactionGroupRequestValidator _validator;
    private readonly IReactionGroupRepository _repository;
    private readonly IDbReactionGroupMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;

    public CreateReactionGroupCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICreateReactionGroupRequestValidator validator,
      IReactionGroupRepository repository,
      IDbReactionGroupMapper mapper,
      IHttpContextAccessor contextAccessor)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _repository = repository;
      _mapper = mapper;
      _contextAccessor = contextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionGroupRequest request)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new(body: await _repository.CreateAsync(_mapper.Map(request)));

      if (response.Body is null)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      _contextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
