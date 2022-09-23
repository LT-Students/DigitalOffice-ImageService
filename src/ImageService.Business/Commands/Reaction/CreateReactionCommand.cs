using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using FluentValidation.Results;
using LT.DigitalOffice.ImageService.Business.Commands.Reaction.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction;

public class CreateReactionCommand : ICreateReactionCommand
{
  private readonly IAccessValidator _accessValidator;
  private readonly IResponseCreator _responseCreator;
  private readonly ICreateReactionRequestValidator _validator;
  private readonly IDbReactionMapper _mapper;
  private readonly IReactionRepository _reactionRepository;
  private readonly IReactionGroupRepository _reactionGroupRepository;
  private readonly IImageResizeHelper _resizeHelper;
  private readonly IImageCompressHelper _compressHelper;
  private readonly IHttpContextAccessor _httpContextAccessor;

  public CreateReactionCommand(
    IAccessValidator accessValidator,
    IResponseCreator responseCreator,
    ICreateReactionRequestValidator validator,
    IDbReactionMapper mapper,
    IReactionRepository reactionRepository,
    IReactionGroupRepository reactionGroupRepository,
    IImageResizeHelper resizeHelper,
    IImageCompressHelper compressHelper,
    IHttpContextAccessor httpContextAccessor)
  {
    _accessValidator = accessValidator;
    _responseCreator = responseCreator;
    _validator = validator;
    _mapper = mapper;
    _reactionRepository = reactionRepository;
    _reactionGroupRepository = reactionGroupRepository;
    _resizeHelper = resizeHelper;
    _compressHelper = compressHelper;
    _httpContextAccessor = httpContextAccessor;
  }

  public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionRequest request)
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

    DbReaction dbReaction = _mapper.Map(request);
    OperationResultResponse<Guid?> response = new(body: dbReaction.Id);

    (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(
      dbReaction.Content, dbReaction.Extension, resizeMinValue: 24);    //reaction image should be 24*24 px

    if (!resizeResult.isSuccess)
    {
      response.Errors.Add("Resize operation has been failed.");
    }
    if (!string.IsNullOrEmpty(resizeResult.resizedContent))
    {
      dbReaction.Content = resizeResult.resizedContent;
      dbReaction.Extension = resizeResult.extension;
    }

    (bool isSuccess, string compressedContent, string extension) compressedResult = await _compressHelper.CompressAsync(
      dbReaction.Content, dbReaction.Extension, MaxWeighInKB: 10);  //reaction image should be less 10KB

    if (resizeResult.isSuccess)
    {
      dbReaction.Content = compressedResult.compressedContent;
      dbReaction.Extension = compressedResult.extension;
    }
    else
    {
      response.Errors.Add("Compress operation has been failed.");
    }

    await _reactionRepository.CreateAsync(dbReaction);

    _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

    return response;
  }
}
