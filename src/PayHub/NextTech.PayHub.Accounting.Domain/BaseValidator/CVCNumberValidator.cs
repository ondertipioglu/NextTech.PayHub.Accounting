using FluentValidation;
using NextTech.PayHub.Accounting.Shared;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class CVCNumberValidator : AbstractValidator<string>
    {
        public CVCNumberValidator(Regex format)
        {
            this.RuleFor(x => x)
                .Matches(format).WithMessage(Constants.Card.Cvc_Number_Invalid_Message);
        }
    }
}
