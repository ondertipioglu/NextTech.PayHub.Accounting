using Microsoft.EntityFrameworkCore;
using NextTech.PayHub.Accounting.Application;
using NextTech.PayHub.Accounting.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Infrastructure.EF
{
    public class CardRepository : ICardRepository
    {
        private readonly AccountingDBContext _dbContext;
        public CardRepository(AccountingDBContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task AddAsync(Card card, CancellationToken cancellationToken)
        {
            await _dbContext.Cards.AddAsync(card, cancellationToken);
        }

        public async Task<Card> GetAsync(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, CancellationToken cancellationToken)
        {
            return await _dbContext.Cards
                                    .Include(x => x.Account)
                                    .SingleOrDefaultAsync(x => 
                                                            x.CardNumber == cardNumber 
                                                            && x.CardExpireDate == cardExpireDate 
                                                            && x.CardCVC == cardCVC 
                                                            && x.Account.Name == cardOwner
                                    , cancellationToken: cancellationToken);
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
