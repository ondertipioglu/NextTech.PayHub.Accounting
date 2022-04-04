using NextTech.PayHub.Accounting.Shared;
using System.Linq;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class Visa : Card
    {
        public override Regex CardNumberRegex => new Regex(Constants.Visa.CartNumberRegexFormat);

        public override Regex CvcNumberRegex => new Regex(Constants.Visa.CvcRegexFormat);

        public override ICard Initialize(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            return new Visa(cardNumber, cardOwner, cardExpireDate, cardCVC);
        }

        public Visa() { }
        private Visa(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId = null)
        {
            this.CardNumber = cardNumber;
            this.CardOwner = cardOwner;
            this.CardExpireDate = cardExpireDate;
            this.CardCVC = cardCVC;
            this.AccountId = accountId;

            var validator = new VisaValidator();
            var validatioResult = validator.Validate(this);

            ValidationsErrors.AddRange(validatioResult.Errors?.Select(x => x.ErrorMessage));
        }

        public static Visa New(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId = null)
        {
            return new Visa(cardNumber, cardOwner, cardExpireDate, cardCVC, accountId);
        }
    }
}
