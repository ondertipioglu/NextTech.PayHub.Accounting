using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application
{
    public interface ICardRepository
    {
        Task<Card?> GetAsync(string cardNumber, string cardOwner, string cardExpireDate, string cardCVC, CancellationToken cancellationToken);

        Task AddAsync(Card product, CancellationToken cancellationToken);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
