using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public interface ICard
    {
        public long Id{ get; set; }
        bool ValidCardInformation { get;  }
        Regex CardNumberRegex { get; }
        Regex CvcNumberRegex { get; }
        bool IsMatch(string url);
        ICard Initialize(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC);
        List<string> ValidationsErrors { get; set; }
    }
}
