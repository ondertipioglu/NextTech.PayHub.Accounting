using FluentValidation;
using NextTech.PayHub.Accounting.Shared;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class CardNumberValidator : AbstractValidator<string>
    {
        public CardNumberValidator(Regex format)
        {
            this.RuleFor(x => x)
                .Matches(format).WithMessage(Constants.Card.Invalid_Card_Message);
        }
    }
}
