using FluentValidation;
using ServiceLayer.Command;

namespace ServiceLayer.Behaviors
{
    public class CreateCountryCommandValidator : AbstractValidator<CreateCountryCommand>
    {
        public CreateCountryCommandValidator()
        {
            RuleFor(command => command.FullName).NotEmpty().WithMessage("Country FullName is required");
            RuleFor(command => command.IsoCode).NotEmpty().WithMessage("Country IsoCode is required");
            RuleFor(command => command.Alfa2Code).Must(ValidationAlfa2Code).WithMessage("If country Alfa2Code is not null, then it length must be 2");
            RuleFor(command => command.Alfa3Code).Must(ValidationAlfa3Code).WithMessage("If country Alfa3Code is not null, then it length must be 3");
        }

        public bool ValidationAlfa2Code(string alfa2code)
        {
            if (!string.IsNullOrEmpty(alfa2code))
                return alfa2code.Length == 2;
            return true;
        }

        public bool ValidationAlfa3Code(string alfa3code)
        {
            if (!string.IsNullOrEmpty(alfa3code))
                return alfa3code.Length == 3;
            return true;
        }
    }
}
