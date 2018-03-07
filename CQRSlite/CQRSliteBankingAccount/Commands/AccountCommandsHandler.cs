using System.Threading.Tasks;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSliteBankingAccount.Domain;

namespace CQRSliteBankingAccount.Commands
{
    internal class AccountCommandsHandler : 
        ICommandHandler<CreateAccountCommand>,
        ICommandHandler<DepositAccountCommand>
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

        public async Task Handle(DepositAccountCommand message)
        {
            var session = new Session(_repository);
            var account = await _repository.Get<BankAccount>(message.Id);
            await session.Add(account);
            account.Deposit(message.Amount);
            await session.Commit();
        }

    }
}
