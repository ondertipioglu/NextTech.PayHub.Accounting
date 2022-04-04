using System.ComponentModel.DataAnnotations;

namespace NextTech.PayHub.Accounting.Api
{
    public class AuthorizeCreditCardRequest
    {
        public string CardOwner { get; set; } 
        public string CardNumber { get; set; }
        public string CardExpireDate { get; set; }
        public string CvcNumber { get; set; }
    }
}
