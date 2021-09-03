using System.Collections.Generic;
using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Helpers;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.ImageNews
{
  public class CreateImageRequestValidator : AbstractValidator<CreateImageRequest>, ICreateImageRequestValidator
  {
    public readonly static List<string> AllowedExtensions = new()
    { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tga" };

    public CreateImageRequestValidator()
    {
      RuleFor(image => image.Name)
          .MaximumLength(150)
          .WithMessage("Image name is too long.");

      RuleFor(image => image.Content)
          .NotNull()
          .WithMessage("Image content is null or incorrect.")
          .Must(EncodeHelper.IsBase64Coded)
          .WithMessage("Inccorect content");

      RuleFor(image => image.Extension)
          .Must(AllowedExtensions.Contains)
          .WithMessage($"Image extension is not {string.Join('/', AllowedExtensions)}");
    }
  }
}
