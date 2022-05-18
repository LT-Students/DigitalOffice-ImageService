using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.ImageMessage.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Enums;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;
using Microsoft.AspNetCore.Http;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageMessage
{
  public class GetImageMessageCommand : IGetImageMessageCommand
  {
    private readonly IImageRepository _repository;
    private readonly IImageResponseMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetImageMessageCommand(
      IImageRepository repository,
      IImageResponseMapper mapper,
      IHttpContextAccessor httpContextAccessor)
    {
      _repository = repository;
      _mapper = mapper;
      _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResultResponse<ImageResponse>> ExecuteAsync(Guid parentId)
    {
      OperationResultResponse<ImageResponse> response = new();

      response.Body = _mapper.Map(await _repository.GetAsync(ImageSource.Message, parentId));
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
