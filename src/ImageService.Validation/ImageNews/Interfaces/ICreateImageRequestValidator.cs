using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces
{
  [AutoInject]
  public interface ICreateImageRequestValidator : IValidator<CreateImageRequest>
  {
  }
}
