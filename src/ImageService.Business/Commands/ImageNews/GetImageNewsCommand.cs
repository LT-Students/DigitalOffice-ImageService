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
    private readonly IImageResponseMapper _imageNewsResponseMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageNewsCommand(
      IImageNewsRepository imageNewsRepository,
      IImageResponseMapper imageNewsResponseMapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _imageNewsRepository = imageNewsRepository;
      _imageNewsResponseMapper = imageNewsResponseMapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public OperationResultResponse<ImageResponse> Execute(Guid imageId)
    {
      OperationResultResponse<ImageResponse> response = new();

      response.Body = _imageNewsResponseMapper.Map(_imageNewsRepository.Get(imageId));
      response.Status = OperationResultStatusType.FullSuccess;
      if (response.Body == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;

        response.Status = OperationResultStatusType.Failed;
        response.Errors.Add("Image was not found.");
      }

      return response;
    }
  }
}
