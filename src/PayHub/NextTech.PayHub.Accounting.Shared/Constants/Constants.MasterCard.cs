namespace NextTech.PayHub.Accounting.Shared
{
    public partial class Constants
    {
        public class MasterCard
        {
            public const string CvcRegexFormat = @"^\d{3}$";
            public const string CartNumberRegexFormat = @"^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$";
        }
    }
}
