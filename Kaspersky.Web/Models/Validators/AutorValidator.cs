using FluentValidation;

namespace Kaspersky.Web.Models.Validators
{
    public class AutorValidator : AbstractValidator<AutorModel>
    {
        public AutorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .MaximumLength(20);
            RuleFor(a => a.Surname)
                .NotEmpty()
                .MaximumLength(20);
        }
    }
}
