using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CQRSlite.Events;
using CQRSliteBankingAccount.Events;
using CQRSliteBankingAccount.Infrastructure;

namespace CQRSliteBankingAccount.Projections
{
    internal class BankAccountsListView : 
        ICancellableEventHandler<AccountCreated>,
        ICancellableEventHandler<AccountDeposited>
    {
        private readonly InMemoryDatabase _database;

        public BankAccountsListView(InMemoryDatabase database)
        {
            _database = database;
        }

        public Task Handle(AccountCreated message, CancellationToken token = new CancellationToken())
        {
            var list = _database.GetAll<BankAccountListDto>().FirstOrDefault() ?? new BankAccountListDto();
            list.Add(new BankAccountDto(message.Id, message.Name));
            _database.Set(list.Id, list);
            return Task.CompletedTask;
        }

        public Task Handle(AccountDeposited message, CancellationToken token = new CancellationToken())
        {
            var balance = _database.GetAll<BankAccountBalanceDto> ().FirstOrDefault() 
                          ?? new BankAccountBalanceDto(message.Id, 0);
            balance.Balance += message.Amount;
            _database.Set(balance.Id, balance);
            return Task.CompletedTask;
        }
    }
}
