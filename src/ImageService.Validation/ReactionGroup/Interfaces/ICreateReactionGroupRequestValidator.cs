using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Validation.ReactionGroup.Interfaces
{
  [AutoInject]
  public interface ICreateReactionGroupRequestValidator : IValidator<CreateReactionGroupRequest>
  {
  }
}
