using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Validation.ImageReaction.Interfaces
{
  [AutoInject]
  public interface ICreateImageReactionRequestValidator : IValidator<CreateImageRequest>
  {
  }
}
