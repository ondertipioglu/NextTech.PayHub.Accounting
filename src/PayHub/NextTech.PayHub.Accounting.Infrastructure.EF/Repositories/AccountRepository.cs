using Microsoft.EntityFrameworkCore;
using NextTech.PayHub.Accounting.Application;
using NextTech.PayHub.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Infrastructure.EF.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AccountingDBContext _dbContext;
        public AccountRepository(AccountingDBContext context)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task AddAsync(Account account, CancellationToken cancellationToken)
        {
            await _dbContext.Accounts.AddAsync(account, cancellationToken);
        }

        public async Task AddRangeAsync(List<Account> accounts, CancellationToken cancellationToken)
        {
            await _dbContext.Accounts.AddRangeAsync(accounts, cancellationToken);
        }

        public async Task<Account> GetAsync(string name, CancellationToken cancellationToken)
        {
            return await _dbContext.Accounts.SingleOrDefaultAsync(x => x.Name == name, cancellationToken: cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Account category, CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
            _dbContext.Accounts.Update(category);
        }
    }
}
