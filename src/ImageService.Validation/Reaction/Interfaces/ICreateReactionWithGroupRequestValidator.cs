using FluentValidation;
using LT.DigitalOffice.ImageService.Models.Dto.Requests;
using LT.DigitalOffice.Kernel.Attributes;

namespace LT.DigitalOffice.ImageService.Validation.Reaction.Interfaces;

[AutoInject]
public interface ICreateReactionWithGroupRequestValidator : IValidator<CreateReactionWithGroupRequest>
{
}
