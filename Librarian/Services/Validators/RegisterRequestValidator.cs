using FluentValidation;
using Librarian.Models;
using Librarian.Tools;

namespace Librarian.Services.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Password).Password();
        }
    }
}
