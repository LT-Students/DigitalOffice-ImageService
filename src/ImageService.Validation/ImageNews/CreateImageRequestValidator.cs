using System;
using System.Collections.Generic;
using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.ImageNews.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.ImageNews
{
  public class CreateImageRequestValidator : AbstractValidator<CreateImageRequest>, ICreateImageRequestValidator
  {
    private List<string> AllowedExtensions = new()
    { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tga" };

    public CreateImageRequestValidator()
    {
      RuleFor(image => image.Name)
          .MaximumLength(150)
          .WithMessage("Image name is too long.");

      RuleFor(image => image.Content)
          .NotNull()
          .WithMessage("Image content is null.")
          .Must(x =>
          {
            try
            {
              var byteString = new Span<byte>(new byte[x.Length]);
              return Convert.TryFromBase64String(x, byteString, out _);
            }
            catch
            {
              return false;
            }
          }).WithMessage("Wrong image content.");

      RuleFor(image => image.Extension)
          .Must(AllowedExtensions.Contains)
          .WithMessage($"Image extension is not {string.Join('/', AllowedExtensions)}");
    }
  }
}
