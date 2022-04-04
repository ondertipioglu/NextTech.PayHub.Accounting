namespace NextTech.PayHub.Accounting.Shared
{
    public partial class Constants
    {
        public class AmericanExpress
        {
            public const string CvcRegexFormat = @"^[0-9]{3,4}$";
            public const string CartNumberRegexFormat = @"^3[47][0-9]{13}$";
        }
    }
}
