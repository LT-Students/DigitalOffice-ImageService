using System.Threading.Tasks;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Models.Dto.Responses;
using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Responses;

namespace LT.DigitalOffice.ImageService.Business.Commands.ImageNews.Interfaces
{
  [AutoInject]
  public interface ICreateImageNewsCommand
  {
    Task<OperationResultResponse<CreateImageNewsResponse>> Execute(CreateImageRequest request);
  }
}
