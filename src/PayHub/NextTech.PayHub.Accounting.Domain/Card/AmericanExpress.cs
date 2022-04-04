using NextTech.PayHub.Accounting.Shared;
using System.Linq;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class AmericanExpress : Card
    {
        public override Regex CardNumberRegex => new Regex(Constants.AmericanExpress.CartNumberRegexFormat);
        public override Regex CvcNumberRegex => new Regex(Constants.AmericanExpress.CvcRegexFormat);
        public override ICard Initialize(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            return new AmericanExpress(cardNumber, cardOwner, cardExpireDate, cardCVC);
        }

        public AmericanExpress() { }
        private AmericanExpress(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId = null)
        {
            this.CardNumber = cardNumber;
            this.CardOwner = cardOwner;
            this.CardExpireDate = cardExpireDate;
            this.CardCVC = cardCVC;
            this.AccountId = accountId;

            var validator = new AmericanExpressValidator();
            var validatioResult = validator.Validate(this);

            ValidationsErrors.AddRange(validatioResult.Errors?.Select(x => x.ErrorMessage));
        }
        public static AmericanExpress New(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId=null)
        {
            return new AmericanExpress(cardNumber, cardOwner, cardExpireDate, cardCVC, accountId);
        }

    }
}
