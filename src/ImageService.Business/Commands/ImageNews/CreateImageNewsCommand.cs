using System;
using System.Collections.Generic;
using System.Net;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Helpers.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;
using LT.DigitalOffice.Kernel.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
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
    private readonly IResizeImageHelper _resizeHelper;
    private readonly ICreateImageRequestValidator _validator;

    public CreateImageNewsCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IImageNewsRepository repository,
      IDbImageNewsMapper mapper,
      IResizeImageHelper resizeHelper,
      ICreateImageRequestValidator validator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _repository = repository;
      _mapper = mapper;
      _resizeHelper = resizeHelper;
      _validator = validator;
    }

    public OperationResultResponse<CreateImageNewsResponse> Execute(CreateImageRequest request)
    {
      if (!_accessValidator.HasRights(Rights.AddEditRemoveNews))
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
      List<DbImageNews> dbImagesNews = new() { dbImageNews } ;

      if (request.EnablePrewiew)
      {
        string resizedContent = _resizeHelper.Resize(request.Content, request.Extension);

        if (resizedContent != null)
        {
          dbPreviewNews = _mapper.Map(request, dbImageNews.Id, resizedContent);
          dbImagesNews.Add(dbPreviewNews);
        }
        else
        {
          _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

          response.Status = OperationResultStatusType.Failed;
          response.Errors = new() { "Image resize failed." };
          return response;
        }
      }

      if (_repository.Create(dbImagesNews) == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        response.Status = OperationResultStatusType.Failed;
        return response;
      }

      response.Body = new CreateImageNewsResponse() { ImageId = dbImageNews.Id, PreviewId = dbPreviewNews?.Id };

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      response.Status = OperationResultStatusType.FullSuccess;
      return response;
    }
  }
}
