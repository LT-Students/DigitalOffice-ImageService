using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.ImageReaction.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;
using LT.DigitalOffice.Models.Broker.Enums;

namespace LT.DigitalOffice.ImageService.Validation.ImageReaction
{
  public class CreateImageReactionRequestValidator : AbstractValidator<CreateImageRequest>, ICreateImageReactionRequestValidator
  {
    public CreateImageReactionRequestValidator(
      IImageContentValidator imageContentValidator,
      IImageExtensionValidator imageExtensionValidator,
      IImageRepository repository)
    {
      RuleFor(image => image.Name)
        .MaximumLength(150)
        .WithMessage("Image name is too long.")
        .MustAsync(async (name,_) => !await repository.DoesSameNameExistAsync(name, ImageSource.Reaction))
        .WithMessage("Reaction with this name already exists.");

      RuleFor(image => image.Content)
        .SetValidator(imageContentValidator);

      RuleFor(image => image.Extension)
        .SetValidator(imageExtensionValidator);
    }
  }
}
