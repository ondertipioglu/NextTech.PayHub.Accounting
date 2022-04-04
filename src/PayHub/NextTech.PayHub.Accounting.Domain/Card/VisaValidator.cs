using FluentValidation;
namespace NextTech.PayHub.Accounting.Domain
{
    public class VisaValidator : AbstractValidator<Visa>
    {
        public VisaValidator()
        {
            CascadeMode = CascadeMode.Continue;

            RuleFor(x => x.CardNumber).SetValidator(model => new CardNumberValidator(model.CardNumberRegex));

            RuleFor(x => x.CardCVC).SetValidator(model => new CVCNumberValidator(model.CvcNumberRegex));

            RuleFor(x => x.CardExpireDate).SetValidator(model => new CardExpireDateValidator());
        }
    }
}
