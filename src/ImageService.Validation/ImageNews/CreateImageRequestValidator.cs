using System;
using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Validation.ImageNews
{
  public class CreateImageRequestValidator : AbstractValidator<CreateImageRequest>, ICreateImageRequestValidator
  {
    public CreateImageRequestValidator(
      IImageContentValidator imageContentValidator,
      IImageExtensionValidator imageExtensionValidator)
    {
      RuleFor(image => image.Name)
        .MaximumLength(150).WithMessage("Image name is too long.");

      RuleFor(image => image.Content)
        .SetValidator(imageContentValidator);

      RuleFor(image => image.Extension)
        .SetValidator(imageExtensionValidator);

      RuleFor(image => image.Purpose)
        .Must(image => Enum.IsDefined(typeof(ImageSource), (ImageSource)image))
        .WithMessage("This image purpose doesn't exist.");
    }
  }
}
