using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Constants;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.Reaction
{
  public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>, ICreateReactionRequestValidator
  {
    public CreateReactionRequestValidator(
      IImageContentValidator imageContentValidator,
      IReactionRepository reactionRepository,
      IReactionGroupRepository groupRepository)
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

      RuleFor(reaction => reaction.GroupId)       //Guid.TryParse - ?
        .Cascade(CascadeMode.Stop)
        .NotNull()
        .WithMessage("Group can't be null. Pick a group wich is not full yet or create a new one.")
        .MustAsync(async (groupId, _) => await groupRepository.DoesExistAsync(groupId))
        .WithMessage("This group doesn't exist.")
        .MustAsync(async (groupId, _) => await reactionRepository.CountReactionsInGroupAsync(groupId) < 16)
        .WithMessage("There can't be more than 16 reactions in a group.");
    }
  }
}
