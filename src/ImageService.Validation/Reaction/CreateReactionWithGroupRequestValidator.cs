using System.Collections.Immutable;
using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;
using LT.DigitalOffice.Kernel.Constants;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.Reaction;

public class CreateReactionWithGroupRequestValidator : AbstractValidator<CreateReactionWithGroupRequest>, ICreateReactionWithGroupRequestValidator
{
  public CreateReactionWithGroupRequestValidator(
    IImageContentValidator imageContentValidator,
    IReactionRepository reactionRepository)
  {
    RuleFor(reaction => reaction.Name)
     .MaximumLength(8)
     .WithMessage("Image name is too long.")
     .MustAsync(async (name, _) => !await reactionRepository.DoesSameNameExistAsync(name))
     .WithMessage("Reaction with this name already exists.");

    RuleFor(reaction => reaction.Content)
      .SetValidator(imageContentValidator);

    RuleFor(reaction => reaction.Extension)
       .Must(extension => ImmutableList.Create(
         ImageFormats.jpg,
         ImageFormats.jpeg,
         ImageFormats.png,
         ImageFormats.svg,
         ImageFormats.gif,
         ".webp")                        //update Kernel and change to ImageFormats.webp
       .Contains(extension))
       .WithMessage("Wrong image extension.");
  }
}
