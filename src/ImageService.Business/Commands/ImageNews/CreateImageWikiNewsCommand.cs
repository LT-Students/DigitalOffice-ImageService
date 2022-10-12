using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.FluentValidationExtensions;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews
{
  public class CreateImageWikiNewsCommand : ICreateImageWikiNewsCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageRepository _repository;
    private readonly IDbImageMapper _mapper;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly ICreateImageRequestValidator _validator;

    public CreateImageWikiNewsCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IImageRepository repository,
      IDbImageMapper mapper,
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

    public async Task<OperationResultResponse<CreateImageWikiNewsResponse>> ExecuteAsync(CreateImageRequest request)
    {
      if (request.Purpose == ImagePurpose.News)
      {
        if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveNews))
        {
          _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

          return new OperationResultResponse<CreateImageWikiNewsResponse>
          {
            Errors = new() { "Not enough rights." }
          };
        }
      }
      else if (request.Purpose == ImagePurpose.Wiki)
      {
        if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveWiki))
        {
          _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;

          return new OperationResultResponse<CreateImageWikiNewsResponse>
          {
            Errors = new() { "Not enough rights." }
          };
        }
      }

      if (!_validator.ValidateCustom(request, out List<string> errors))
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        return new OperationResultResponse<CreateImageWikiNewsResponse>
        {
          Errors = errors
        };
      }

      OperationResultResponse<CreateImageWikiNewsResponse> response = new();

      DbImage dbImage = _mapper.Map(request);
      DbImage dbPreview = null;
      List<DbImage> dbImages = new() { dbImage };

      if (request.EnablePreview)
      {
        (bool isSuccess, string resizedContent, string extension) resizeResult = await _resizeHelper.ResizeAsync(request.Content, request.Extension);

        if (!resizeResult.isSuccess)
        {
          response.Errors.Add("Resize operation has been failed.");
        }

        if (!string.IsNullOrEmpty(resizeResult.resizedContent))
        {
          dbPreview = _mapper.Map(request, dbImage.Id, resizeResult.resizedContent, resizeResult.extension);
          dbImages.Add(dbPreview);
        }
        else
        {
          dbImage.ParentId = dbImage.Id;
        }
      }

      await _repository.CreateAsync((ImageSource)Enum.Parse(typeof(ImageSource), request.Purpose.ToString()), dbImages);

      response.Body = new CreateImageWikiNewsResponse() { ImageId = dbImage.Id, PreviewId = dbPreview?.Id };

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
