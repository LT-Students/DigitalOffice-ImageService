using System;
using System.Net;
using LT.DigitalOffice.ImageService.Business.Commands.ImageProject.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageProject
{
  public class GetImageProjectCommand : IGetImageProjectCommand
  {
    private readonly IImageProjectRepository _imageProjectRepository;
    private readonly IImageResponseMapper _imageResponseMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageProjectCommand(
      IImageProjectRepository imageProjectRepository,
      IImageResponseMapper imageResponseMapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _imageProjectRepository = imageProjectRepository;
      _imageResponseMapper = imageResponseMapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public OperationResultResponse<ImageResponse> Execute(Guid parentId)
    {
      OperationResultResponse<ImageResponse> response = new();

      response.Body = _imageResponseMapper.Map(_imageProjectRepository.Get(parentId));
      if (response.Body == null)
      {
        _httpContextAccessor.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        response.Status = OperationResultStatusType.Failed;
        response.Errors.Add("Image was not found.");
        return response;
      }
      response.Status = OperationResultStatusType.FullSuccess;

      return response;
    }
  }
}
