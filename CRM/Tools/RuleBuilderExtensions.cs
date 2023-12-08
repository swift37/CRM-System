using FluentValidation;

namespace CRM.Tools
{
    public static class RuleBuilderExtensions
    {
        public static void Login<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLenght = 4)
        {
            ruleBuilder
                .MinimumLength(minLenght)
                .WithMessage($"Minimum length wasn`t satisfied properly {minLenght}")
                .Matches("[a-zA-Z0-9]")
                .WithMessage($"Login must contain only latin characters and digits");
        }

        public static void Password<T>(this IRuleBuilder<T, string?> ruleBuilder, int minLenght = 8)
        {
            ruleBuilder
                .MinimumLength(minLenght)
                .WithMessage($"Minimum length wasn`t satisfied properly {minLenght}")
                .Matches("[a-z]")
                .WithMessage($"You need to have at least one lowercase letter")
                .Matches("[A-Z]")
                .WithMessage($"You need to have at least one uppercase letter")
                .Matches("[0-9]")
                .WithMessage($"You need to have at least one digit")
                .Matches("[^a-zA-Z0-9]")
                .WithMessage($"You need to have at least one special character");
        }
    }
}
