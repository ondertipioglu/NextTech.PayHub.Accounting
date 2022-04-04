using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace NextTech.PayHub.Accounting.Application.Helper
{
    public interface ICardTypeParser
    {
        ReadOnlyCollection<Card> RegisteredCardTypes { get; }
    }
    public class CardTypeParser : ICardTypeParser
    {
        public ReadOnlyCollection<Card> RegisteredCardTypes { get; private set; }

        public CardTypeParser()
        {
            RegisteredCardTypes = typeof(Card).Assembly.DefinedTypes
             .Where(type => type.IsSubclassOf(typeof(Card)) && !type.IsAbstract)
             .Select(x => Activator.CreateInstance(x) as Card)
             .ToList().AsReadOnly(); ;
        }
    }
}
