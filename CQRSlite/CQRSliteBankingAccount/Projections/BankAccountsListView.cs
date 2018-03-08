using System.Threading.Tasks;
using CQRSlite.Events;
using CQRSliteBankingAccount.Events;
using CQRSliteBankingAccount.Infrastructure;

namespace CQRSliteBankingAccount.Projections
{
    internal class BankAccountsListView : 
        IEventHandler<AccountCreated>,
        IEventHandler<MoneyReceived>,
        IEventHandler<MoneyRemoved>
    {
        private readonly InMemoryDatabase _database;

        public BankAccountsListView(InMemoryDatabase database)
        {
            _database = database;
        }

        public Task Handle(AccountCreated message)
        {
            var list = _database.GetAccountListDto();
            list.Add(new BankAccountDto(message.Id, message.Name));
            _database.SaveAccountListDto(list);
            return Task.FromResult(0);
        }

        public Task Handle(MoneyReceived message)
        {
            var balance = _database.GetAccountBalanceDto(message.Id);
            balance.Balance += message.Amount;
            _database.SaveAccountBalanceDto(balance);
            return Task.FromResult(0);
        }

        public Task Handle(MoneyRemoved message)
        {
            var balance = _database.GetAccountBalanceDto(message.Id);
            balance.Balance -= message.Amount;
            _database.SaveAccountBalanceDto(balance);
            return Task.FromResult(0);
        }
    }
}
