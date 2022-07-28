using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.ImageService.Business.Commands.ImageReaction.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.ImageService.Validation.ImageReaction.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageReaction
{
  public class CreateImageReactionCommand : ICreateImageReactionCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IResponseCreator _responseCreator;
    private readonly ICreateImageReactionRequestValidator _validator;
    private readonly IDbImageMapper _mapper;
    private readonly IImageRepository _repository;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateImageReactionCommand(
      IAccessValidator accessValidator,
      IResponseCreator responseCreator,
      ICreateImageReactionRequestValidator validator,
      IDbImageMapper mapper,
      IImageRepository repository,
      IImageResizeHelper resizeHelper,
      IHttpContextAccessor httpContextAccessor)
    {
      _accessValidator = accessValidator;
      _responseCreator = responseCreator;
      _validator = validator;
      _mapper = mapper;
      _repository = repository;
      _resizeHelper = resizeHelper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<CreateImageReactionResponse>> ExecuteAsync(CreateImageRequest request)
    {
      if (!await _accessValidator.IsAdminAsync())
      {
        return _responseCreator.CreateFailureResponse<CreateImageReactionResponse>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<CreateImageReactionResponse>(
          HttpStatusCode.BadRequest,
          validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<CreateImageReactionResponse> response = new();
      DbImage dbImageReaction = _mapper.Map(request);
      List<DbImage> dbImagesReactions = new() { dbImageReaction };
      DbImage dbPreviewReaction = null;

      if (request.EnablePreview)
      {
        (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(request.Content, request.Extension);

        if (!resizeResult.isSuccess)
        {
          response.Errors.Add("Resize operation has been failed.");
        }

        if (!string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbPreviewReaction = _mapper.Map(
            request: request,
            parentId: dbImageReaction.Id,
            content: resizeResult.resizedContent,
            extension: resizeResult.extension);

          dbImagesReactions.Add(dbPreviewReaction);
        }
        else
        {
          dbImageReaction.ParentId = dbImageReaction.Id;
        }
      }

      await _repository.CreateAsync(ImageSource.Reaction, dbImagesReactions);
      response.Body = new CreateImageReactionResponse() { ImageId = dbImageReaction.Id, PreviewId = dbPreviewReaction?.Id };
      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
