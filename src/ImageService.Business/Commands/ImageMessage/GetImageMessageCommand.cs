using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageMessage
{
  public class GetImageMessageCommand : IGetImageMessageCommand
  {
    private readonly IImageMessageRepository _imageMessageRepository;
    private readonly IImageResponseMapper _imageMessageResponseMapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageMessageCommand(
      IImageMessageRepository imageMessageRepository,
      IImageResponseMapper imageMessageResponseMapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _imageMessageRepository = imageMessageRepository;
      _imageMessageResponseMapper = imageMessageResponseMapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<ImageResponse>> ExecuteAsync(Guid parentId)
    {
      OperationResultResponse<ImageResponse> response = new();

      response.Body = _imageMessageResponseMapper.Map(await _imageMessageRepository.GetAsync(parentId));
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
