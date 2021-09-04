using System;
using System.Net;
using LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews
{
  public class GetImageNewsCommand : IGetImageNewsCommand
  {
    private readonly IImageNewsRepository _imageNewsRepository;
    private readonly IImageDataResponseMapper _imageNewsResponseMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageNewsCommand(
      IImageNewsRepository imageNewsRepository,
      IImageDataResponseMapper imageNewsResponseMapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _imageNewsRepository = imageNewsRepository;
      _imageNewsResponseMapper = imageNewsResponseMapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public OperationResultResponse<ImageDataResponse> Execute(Guid imageId)
    {
      OperationResultResponse<ImageDataResponse> response = new();

      DbImageNews dbImageNews = _imageNewsRepository.Get(imageId);

      if (dbImageNews == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        response.Status = OperationResultStatusType.Failed;
        response.Errors.Add("Image was not found.");
        return response;
      }

      response.Body = _imageNewsResponseMapper.Map(dbImageNews);
      response.Status = OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
