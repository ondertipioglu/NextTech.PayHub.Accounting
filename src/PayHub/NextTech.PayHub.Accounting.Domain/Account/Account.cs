using System.Collections.Generic;

namespace NextTech.PayHub.Accounting.Domain
{
    public class Account
    {
        private Account() { }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public virtual IEnumerable<Card> Cards { get; private set; }
        private Account(string name)
        {
            Name = name;
        }
        public static Account Create(string name)
        {
            //TODO : Guard
            return new Account(name);
        }
    }
}
