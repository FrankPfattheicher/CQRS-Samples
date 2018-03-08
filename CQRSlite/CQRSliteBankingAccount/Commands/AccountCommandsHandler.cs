using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSliteBankingAccount.Domain;

namespace CQRSliteBankingAccount.Commands
{
    internal class AccountCommandsHandler : 
        ICommandHandler<CreateAccountCommand>,
        ICommandHandler<PayInCommand>,
        ICommandHandler<PayOutCommand>,
        ICommandHandler<TransferCommand>
    {
        private readonly IRepository _repository;

        public AccountCommandsHandler(IRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(CreateAccountCommand message)
        {
            var session = new Session(_repository);
            var account = new BankAccount(message.Id, message.Name);
            await session.Add(account);
            await session.Commit();
        }

        public async Task Handle(PayInCommand message)
        {
            var session = new Session(_repository);
            var account = await _repository.Get<BankAccount>(message.Id);
            await session.Add(account);
            account.AddMoney(message.Amount);
            await session.Commit();
        }

        public async Task Handle(PayOutCommand message)
        {
            var session = new Session(_repository);
            var account = await _repository.Get<BankAccount>(message.Id);
            await session.Add(account);
            account.RemoveMoney(message.Amount);
            await session.Commit();
        }

        public async Task Handle(TransferCommand message)
        {
            var session = new Session(_repository);

            var fromAccount = await _repository.Get<BankAccount>(message.FromAccountId);
            await session.Add(fromAccount);
            var toAccount = await _repository.Get<BankAccount>(message.ToAccountId);
            await session.Add(toAccount);

            fromAccount.RemoveMoney(message.Amount);
            toAccount.AddMoney(message.Amount);

            await session.Commit();
        }
    }
}
