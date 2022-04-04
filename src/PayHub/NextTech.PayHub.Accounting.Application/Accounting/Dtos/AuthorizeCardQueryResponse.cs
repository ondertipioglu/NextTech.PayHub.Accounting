using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application
{
    public class AuthorizeCardQueryResponse
    {
        public string ValidationErrorMessage { get; set; }
        public string CartType { get; set; }
    }
}
