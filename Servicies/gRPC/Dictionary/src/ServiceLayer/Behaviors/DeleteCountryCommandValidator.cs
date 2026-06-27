using FluentValidation;
using ServiceLayer.Command;

namespace ServiceLayer.Behaviors
{
    public class DeleteCountryCommandValidator : AbstractValidator<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidator()
        {
            RuleFor(command => command.CountryId).NotEmpty().WithMessage("CountryId is required");
        }

    }
}
