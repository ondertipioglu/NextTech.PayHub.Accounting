using NextTech.PayHub.Accounting.Shared;
using System.Linq;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class MasterCard : Card
    {       
        public override Regex CardNumberRegex => new Regex(Constants.MasterCard.CartNumberRegexFormat);

        public override Regex CvcNumberRegex => new Regex(Constants.MasterCard.CvcRegexFormat);

        public override ICard Initialize(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC)
        {
            return new MasterCard(cardNumber, cardOwner, cardExpireDate, cardCVC);
        }

        public MasterCard() { }

        private MasterCard(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId = null)
        {
            this.CardNumber = cardNumber;
            this.CardOwner = cardOwner;
            this.CardExpireDate = cardExpireDate;
            this.CardCVC = cardCVC;
            this.AccountId = accountId;

            var validator = new MasterCardValidator();
            var validatioResult = validator.Validate(this);

            ValidationsErrors.AddRange(validatioResult.Errors?.Select(x => x.ErrorMessage));
        }

        public static MasterCard New(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, int? accountId = null)
        {
            return new MasterCard(cardNumber, cardOwner, cardExpireDate, cardCVC, accountId);
        }

    }
}
