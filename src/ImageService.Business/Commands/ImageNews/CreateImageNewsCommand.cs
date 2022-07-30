using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentValidation.Results;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Db.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;
using LT.DigitalOffice.Kernel.BrokerSupport.AccessValidatorEngine.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.ImageSupport.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews
{
  public class CreateImageNewsCommand : ICreateImageNewsCommand
  {
    private readonly IAccessValidator _accessValidator;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IImageRepository _repository;
    private readonly IDbImageMapper _mapper;
    private readonly IImageResizeHelper _resizeHelper;
    private readonly ICreateImageRequestValidator _validator;
    private readonly IResponseCreator _responseCreator;

    public CreateImageNewsCommand(
      IAccessValidator accessValidator,
      IHttpContextAccessor httpContextAccessor,
      IImageRepository repository,
      IDbImageMapper mapper,
      IImageResizeHelper resizeHelper,
      ICreateImageRequestValidator validator,
      IResponseCreator responseCreator)
    {
      _accessValidator = accessValidator;
      _httpContextAccessor = httpContextAccessor;
      _repository = repository;
      _mapper = mapper;
      _resizeHelper = resizeHelper;
      _validator = validator;
      _responseCreator = responseCreator;
    }

    public async Task<OperationResultResponse<CreateImageNewsResponse>> ExecuteAsync(CreateImageRequest request)
    {
      if (!await _accessValidator.HasRightsAsync(Rights.AddEditRemoveNews))
      {
        return _responseCreator.CreateFailureResponse<CreateImageNewsResponse>(HttpStatusCode.Forbidden);
      }

      ValidationResult validationResult = await _validator.ValidateAsync(request);

      if (!validationResult.IsValid)
      {
        return _responseCreator.CreateFailureResponse<CreateImageNewsResponse>(
            HttpStatusCode.BadRequest,
            validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
      }

      OperationResultResponse<CreateImageNewsResponse> response = new();

      DbImage dbImageNews = _mapper.Map(request);
      DbImage dbPreviewNews = null;
      List<DbImage> dbImagesNews = new() { dbImageNews };

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

      await _repository.CreateAsync(ImageSource.News, dbImagesNews);

      response.Body = new CreateImageNewsResponse() { ImageId = dbImageNews.Id, PreviewId = dbPreviewNews?.Id };

      _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;

      return response;
    }
  }
}
