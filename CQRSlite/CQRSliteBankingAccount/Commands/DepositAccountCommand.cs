using System;
using CQRSlite.Commands;

namespace CQRSliteBankingAccount.Commands
{
    internal class DepositAccountCommand : ICommand
    {
        public readonly Guid Id;
        public readonly float Amount;

        public DepositAccountCommand(Guid accountId, float amount)
        {
            Id = accountId;
            Amount = amount;
        }
    }
}