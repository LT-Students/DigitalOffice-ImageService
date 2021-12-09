using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews
{
  public class CreateImageNewsCommand : ICreateImageNewsCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageNewsRepository _repository;
    private readonly IDbImageNewsMapper _mapper;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly ICreateImageRequestValidator _validator;

    public CreateImageNewsCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IImageNewsRepository repository,
      IDbImageNewsMapper mapper,
      IImageResizeHelper resizeHelper,
      ICreateImageRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _repository = repository;
      _mapper = mapper;
      _resizeHelper = resizeHelper;
      _validator = validator;
    }

    public async Task<OperationResultResponse<CreateImageNewsResponse>> ExecuteAsync(CreateImageRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveNews))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

        return new OperationResultResponse<CreateImageNewsResponse>
        {
          Status = OperationResultStatusType.Failed,
          Errors = new() { "Not enough rights." }
        };
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<CreateImageNewsResponse>
        {
          Status = OperationResultStatusType.Failed,
          Errors = errors
        };
      }

      OperationResultResponse<CreateImageNewsResponse> response = new();

      DbImageNews dbImageNews = _mapper.Map(request);
      DbImageNews dbPreviewNews = null;
      List<DbImageNews> dbImagesNews = new() { dbImageNews };

      if (request.EnablePreview)
      {
        (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(request.Content, request.Extension);

        if (!resizeResult.isSuccess)
        {
          response.Errors.Add("Resize operation has been failed.");
        }

        if (!string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbPreviewNews = _mapper.Map(request, dbImageNews.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImagesNews.Add(dbPreviewNews);
        }
        else
        {
          dbImageNews.ParentId = dbImageNews.Id;
        }
      }

      if (await _repository.CreateAsync(dbImagesNews) == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
        response.Status = OperationResultStatusType.Failed;

        return response;
      }

      response.Body = new CreateImageNewsResponse() { ImageId = dbImageNews.Id, PreviewId = dbPreviewNews?.Id };

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      response.Status = response.Errors.Any()
        ? OperationResultStatusType.PartialSuccess
        : OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
