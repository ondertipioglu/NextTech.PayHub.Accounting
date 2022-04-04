namespace NextTech.PayHub.Accounting.Shared
{
    public partial class Constants
    {
        public class Card
        {
            public const string ExpireDateMonthCheckFormat = @"^(0[1-9]|1[0-2])$";
            public const string ExpireDateYearCheckFormat = @"^20[0-9]{2}$";
            public const string Invalid_Card_Message = @"The credit card entered is invalid";
            public const string Card_Expired_Date_Or_Invalid_Date_Message = @"The credit card used has expired or invalid credit card expiration date";
            public const string Invalid_Account_Information_Message = @"The account information is invalid";
            public const string Cvc_Number_Invalid_Message = @"The CVV provide is invalid";            
        }       
    }
}
