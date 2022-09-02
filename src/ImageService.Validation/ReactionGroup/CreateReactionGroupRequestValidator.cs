using FluentValidation;
using LT.DigitalOffice.ImageService.Data.Interfaces;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.ImageService.Validation.ReactionGroup.Interfaces;

namespace LT.DigitalOffice.ImageService.Validation.ReactionGroup
{
  public class CreateReactionGroupRequestValidator : AbstractValidator<CreateReactionGroupRequest>, ICreateReactionGroupRequestValidator
  {
    public CreateReactionGroupRequestValidator(
      IReactionGroupRepository repository)
    {
      RuleFor(x => x.Name)
        .Cascade(CascadeMode.Stop)
        .MaximumLength(10)
        .WithMessage("Group's name is too long.")
        .MustAsync(async(n,_) => !await repository.DoesSameNameExistAsync(n))
        .WithMessage("Group with this name already exists.");
    }
  }
}
