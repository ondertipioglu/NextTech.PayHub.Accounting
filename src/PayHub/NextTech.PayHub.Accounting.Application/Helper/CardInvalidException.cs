using System;

namespace NextTech.PayHub.Accounting.Application.Helper
{
    public class CardInvalidException : Exception
    {
        public CardInvalidException()
        {
        }

        public CardInvalidException(string message) : base(message)
        {
        }
    }
}
