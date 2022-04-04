using NextTech.PayHub.Accounting.Application;
using NextTech.PayHub.Accounting.Domain;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NextTech.PayHub.Accounting.Infrastructure.EF
{
    public class DataMigrator
    {
        private readonly ICardRepository cardRepository;
        private readonly IAccountRepository accountRepository;

        public DataMigrator(ICardRepository cardRepository, IAccountRepository accountRepository)
        {
            this.cardRepository = cardRepository;
            this.accountRepository = accountRepository;
        }
        public async Task Seed()
        {
            await SeedAccounts();
            await SeedCards();
        }
        public async Task SeedAccounts()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            foreach (var item in GetAccounts())
            {
                var account = await accountRepository.GetAsync(item.account.Name, cancellationToken);
                if (account == null)
                    await accountRepository.AddAsync(item.account, cancellationToken);
            }
            await accountRepository.SaveChangesAsync(cancellationToken);
        }
        public async Task SeedCards()
        {
            var cancellationToken = new CancellationTokenSource().Token;

            foreach (var item in GetCards())
            {
                var card = await cardRepository.GetAsync(item.card.CardNumber, item.card.CardOwner, item.card.CardExpireDate, item.card.CardCVC, cancellationToken);

                if (card == null)
                    await cardRepository.AddAsync(item.card, cancellationToken);
            }
            await cardRepository.SaveChangesAsync(cancellationToken);
        }
        IEnumerable<(int id, Account account)> GetAccounts()
        {
            yield return (1, Account.Create("John Doe"));
            yield return (2, Account.Create("Gordon Norman"));
            yield return (3, Account.Create("Marta Moe"));
            yield return (4, Account.Create("Larry Loe"));
            yield return (5, Account.Create("Karren Koe"));
        }

        IEnumerable<(int id, Card card)> GetCards()
        {
            yield return (1, AmericanExpress.New("375991270307470", "John Doe", "06/2024", "1258",1));
            yield return (2, MasterCard.New("5573611704017326", "John Doe", "06/2025", "123",1));
            yield return (3, Visa.New("4532996986610937", "John Doe", "06/2026", "325",1));

            yield return (4, AmericanExpress.New("343899323972191", "Gordon Norman", "01/2025", "8597", 2));
            yield return (5, MasterCard.New("5241491523617826", "Gordon Norman", "06/2023", "325", 2));
            yield return (6, Visa.New("4916198834172618", "Gordon Norman", "09/2022", "379", 2));

            yield return (7, AmericanExpress.New("347425294152997", "Marta Moe", "06/2025", "528", 3));
            yield return (8, MasterCard.New("5110616165365783", "Marta Moe", "06/2026", "386", 3));
            yield return (9, Visa.New("4916891903488481", "Marta Moe", "06/2027", "958", 3));

            yield return (10, AmericanExpress.New("345742344802146", "Larry Loe", "02/2023", "785", 4));
            yield return (11, MasterCard.New("5321674746107807", "Larry Loe", "06/2021", "453", 4));
            yield return (12, Visa.New("4228420255793299", "Larry Loe", "12/2026", "755", 4));

            yield return (13, AmericanExpress.New("343876755249611", "Karren Koe", "01/2023", "4526", 5));
            yield return (14, MasterCard.New("5199046253286366", "Karren Koe", "12/2024", "796", 5));
            yield return (15, Visa.New("4556810326147131", "Karren Koe", "07/2027", "458", 5));
        }
    }
}
