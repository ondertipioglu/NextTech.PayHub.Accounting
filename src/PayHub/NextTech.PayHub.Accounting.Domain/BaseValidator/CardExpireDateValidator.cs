using FluentValidation;
using NextTech.PayHub.Accounting.Shared;
using System;
using System.Text.RegularExpressions;

namespace NextTech.PayHub.Accounting.Domain
{
    public class CardExpireDateValidator : AbstractValidator<string>
    {
        Regex monthCheck = new Regex(Constants.Card.ExpireDateMonthCheckFormat);
        Regex yearCheck = new Regex(Constants.Card.ExpireDateYearCheckFormat);

        public CardExpireDateValidator()
        {
            this.RuleFor(x => x)
                .Must(CheckExpireDate).WithMessage(Constants.Card.Card_Expired_Date_Or_Invalid_Date_Message);
        }

        private bool CheckExpireDate(string expiryDate)
        {
            var dateSplit = expiryDate.Split('/');
            if (!monthCheck.IsMatch(dateSplit[0]) || !yearCheck.IsMatch(dateSplit[1]))
                return false;

            var year = int.Parse(dateSplit[1]);
            var month = int.Parse(dateSplit[0]);
            var lastDateOfMonth = DateTime.DaysInMonth(year, month);
            var cardExpiryDate = new DateTime(year, month, lastDateOfMonth, 23, 59, 59);

            var result = (cardExpiryDate > DateTime.Now);

            return result;
        }
    }
}
