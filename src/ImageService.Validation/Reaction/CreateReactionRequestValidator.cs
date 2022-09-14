using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;
using LT.DigitalOffice.Kernel.Validators.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.Reaction;

public class CreateReactionRequestValidator : AbstractValidator<CreateReactionRequest>, ICreateReactionRequestValidator
{
  public CreateReactionRequestValidator(
    ICreateReactionWithGroupRequestValidator createReactionWithGroupRequestValidator,
    IReactionRepository reactionRepository,
    IReactionGroupRepository groupRepository)
  {
    RuleFor(reaction => new CreateReactionWithGroupRequest
    {
      Name = reaction.Name,
      Content = reaction.Content,
      Unicode = reaction.Content,
      Extension = reaction.Extension
    })
      .SetValidator(createReactionWithGroupRequestValidator);

    RuleFor(reaction => reaction.GroupId)
      .Cascade(CascadeMode.Stop)
      .NotNull()                                                                                    //remove when Groups will be added by front
      .WithMessage("Group can't be null. Pick a group wich is not full yet or create a new one.")
      .MustAsync(async (groupId, _) => await groupRepository.DoesExistAsync(groupId.Value))
      .WithMessage("This group doesn't exist.")
      .MustAsync(async (groupId, _) => await reactionRepository.CountReactionsInGroupAsync(groupId.Value) < 16)
      .WithMessage("There can't be more than 16 reactions in a group.");
  }
}
