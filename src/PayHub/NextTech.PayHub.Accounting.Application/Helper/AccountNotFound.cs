using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application.Helper
{
    public class AccountNotFound : Exception
    {

        public AccountNotFound()
        {
        }

        public AccountNotFound(string message) : base(message)
        {
        }
    }
}
