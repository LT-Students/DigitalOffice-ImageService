﻿using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.Reaction;

public class CreateReactionWithGroupRequestValidator : AbstractValidator<CreateReactionWithGroupRequest>, ICreateReactionWithGroupRequestValidator
{
  public CreateReactionWithGroupRequestValidator(
    IImageContentValidator imageContentValidator,
    IReactionRepository reactionRepository)
  {
    RuleFor(reaction => reaction.Name)
     .MaximumLength(8)                            //check max length in task!!!!!!!!!
     .WithMessage("Image name is too long.")
     .MustAsync(async (name, _) => !await reactionRepository.DoesSameNameExistAsync(name))
     .WithMessage("Reaction with this name already exists.");

    RuleFor(reaction => reaction.Content)
      .SetValidator(imageContentValidator);

    RuleFor(reaction => reaction.Extension)
       .Cascade(CascadeMode.Stop)
       .NotEmpty().WithMessage("Image extension can't be empty.")
       .Must(extension => ReactionFormats.formats.Contains(extension))
       .WithMessage("Wrong image extension.");
  }
}
