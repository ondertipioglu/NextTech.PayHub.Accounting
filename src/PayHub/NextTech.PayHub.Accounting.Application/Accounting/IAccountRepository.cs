using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Application
{  
    public interface IAccountRepository
    {
        Task UpdateAsync(Account account, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        Task<Account?> GetAsync(string name, CancellationToken cancellationToken);
        Task AddAsync(Account account, CancellationToken cancellationToken);
        Task AddRangeAsync(List<Account> accounts, CancellationToken cancellationToken);
    }
}
