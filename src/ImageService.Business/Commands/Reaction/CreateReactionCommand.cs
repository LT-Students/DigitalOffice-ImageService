﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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

namespace LT.DigitalOffice.ImageService.Business.Commands.Reaction
{
  public class CreateReactionCommand : ICreateReactionCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateReactionRequestValidator _validator;
    private readonly IDbReactionMapper _mapper;
    private readonly IReactionRepository _reactionRepository;
    private readonly IReactionGroupRepository _reactionGroupRepository;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateReactionCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICreateReactionRequestValidator validator,
      IDbReactionMapper mapper,
      IReactionRepository reactionRepository,
      IReactionGroupRepository reactionGroupRepository,
      IImageResizeHelper resizeHelper,
      IHttpContextAccessor httpContextAccessor)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
      _reactionRepository = reactionRepository;
      _reactionGroupRepository = reactionGroupRepository;
      _resizeHelper = resizeHelper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<Guid?>> ExecuteAsync(CreateReactionRequest request)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.Forbidden);
      }

      request.GroupId ??= _reactionGroupRepository.PickGroupAsync().Result;

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<Guid?> response = new();
      DbReaction dbReaction = _mapper.Map(request);

      (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(
        dbReaction.Content, dbReaction.Extension, resizeMinValue: 24);

      if (!resizeResult.isSuccess)
      {
        response.Errors.Add("Resize operation has been failed.");
      }

      if (!string.IsNullOrEmpty(resizeResult.resizedContent))
      {
        dbReaction.Content = resizeResult.resizedContent;
        dbReaction.Extension = resizeResult.extension;
      }

      response.Body = await _reactionRepository.CreateAsync(dbReaction);

      if (response.Body is null)
      {
        return _responseCreator.CreateFailureResponse<Guid?>(HttpStatusCode.BadRequest);
      }

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
      return response;
    }
  }
}
