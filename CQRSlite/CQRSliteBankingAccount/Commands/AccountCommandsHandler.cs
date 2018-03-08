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
        private readonly ISession _session;

        public AccountCommandsHandler(ISession session)
        {
            _session = session;
        }

        public async Task Handle(CreateAccountCommand message)
        {
            var account = new BankAccount(message.Id, message.Name);
            await _session.Add(account);
            await _session.Commit();
        }

        public async Task Handle(PayInCommand message)
        {
            var account = await _session.Get<BankAccount>(message.Id);
            account.AddMoney(message.Amount);
            await _session.Commit();
        }

        public async Task Handle(PayOutCommand message)
        {
            var account = await _session.Get<BankAccount>(message.Id);
            account.RemoveMoney(message.Amount);
            await _session.Commit();
        }

        public async Task Handle(TransferCommand message)
        {

            var fromAccount = await _session.Get<BankAccount>(message.FromAccountId);
            var toAccount = await _session.Get<BankAccount>(message.ToAccountId);

            fromAccount.RemoveMoney(message.Amount);
            toAccount.AddMoney(message.Amount);
            await _session.Commit();
        }
    }
}
