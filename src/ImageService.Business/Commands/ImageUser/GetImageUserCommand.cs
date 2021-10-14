using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageUser.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Db;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageUser
{
  public class GetImageUserCommand : IGetImageUserCommand
  {
    private readonly IImageUserRepository _imageUserRepository;
    private readonly IImageResponseMapper _imageUserResponseMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageUserCommand(
      IImageUserRepository imageUserRepository,
      IImageResponseMapper imageUserResponseMapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _imageUserRepository = imageUserRepository;
      _imageUserResponseMapper = imageUserResponseMapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<ImageResponse>> Execute(Guid imageId)
    {
      OperationResultResponse<ImageResponse> response = new();

      response.Body = _imageUserResponseMapper.Map(await _imageUserRepository.GetAsync(imageId));
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
