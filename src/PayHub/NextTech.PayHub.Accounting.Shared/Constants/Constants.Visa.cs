namespace NextTech.PayHub.Accounting.Shared
{
    public partial class Constants
    {
        public class Visa
        {
            public const string CvcRegexFormat = @"^\d{3}$";
            public const string CartNumberRegexFormat = @"^4[0-9]{12}(?:[0-9]{3})?$";
        }
    }
}
