using System;
using System.Net;
using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Business.Commands.Image.Interfaces;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Mappers.Responses.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Helpers.Interfaces;
using LT.DigitalOffice.Kernel.Responses;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Business.Commands.Image;

public class GetImageCommand : IGetImageCommand
{
  private readonly IImageRepository _imageRepository;
  private readonly IResponseCreator _responseCreator;
  private readonly IImageResponseMapper _mapper;

  public GetImageCommand(
    IImageRepository imageRepository,
    IResponseCreator responseCreator,
    IImageResponseMapper mapper)
  {
    _imageRepository = imageRepository;
    _responseCreator = responseCreator;
    _mapper = mapper;
  }

  public async Task<OperationResultResponse<ImageResponse>> ExecuteAsync(Guid imageId, ImageSource source)
  {
    OperationResultResponse<ImageResponse> response = new();

    response.Body = _mapper.Map(await _imageRepository.GetAsync(source, imageId));

    if(response.Body is null)
    {
      return _responseCreator.CreateFailureResponse<ImageResponse>(HttpStatusCode.NotFound);
    }

    return response;
  }
}
