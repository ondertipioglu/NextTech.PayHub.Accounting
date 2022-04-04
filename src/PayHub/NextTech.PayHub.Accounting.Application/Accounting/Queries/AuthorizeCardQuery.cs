using NextTech.Core.MediatR.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application
{
    public class AuthorizeCardQuery : IQuery<AuthorizeCardQueryResponse>
    {
        public string CardOwner { get; }
        public string CardNumber { get; }
        public string CardExpireDate { get; }
        public string CvcNumber { get; }
        private AuthorizeCardQuery(string cardOwner, string cardNumber, string cardExpireDate, string cvcNumber)
        {
            CardOwner = cardOwner;
            CardNumber = cardNumber;
            CardExpireDate = cardExpireDate;
            CvcNumber = cvcNumber;
        }
        public static AuthorizeCardQuery Create(string cardOwner, string cardNumber, string cardExpireDate, string cvcNumber)
        {
            return new AuthorizeCardQuery(cardOwner, cardNumber, cardExpireDate, cvcNumber);
        }
    }
}
