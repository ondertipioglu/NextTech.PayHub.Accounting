using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public abstract class Card : ICard
    {
        public long Id { get; set; }

        public abstract Regex CardNumberRegex { get; }
        public abstract Regex CvcNumberRegex { get; }
        public int? AccountId { get; internal set; }
        public Account Account { get; private set; }
        public List<string> ValidationsErrors { get; set; } = new List<string>();
        public string CardNumber { get; internal set; }
        public string CardOwner { get; internal set; }
        public string CardExpireDate { get; internal set; }
        public string CardCVC { get; internal set; }
        public bool ValidCardInformation
        {
            get
            {
                if (ValidationsErrors.Any()) return false; else return true;
            }
        }
        public bool IsMatch(string cardNumber)
        {
            return CardNumberRegex.IsMatch(cardNumber);
        }
        public abstract ICard Initialize(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC);
    }
}