using System;
using CQRSlite.Commands;

namespace CQRSliteBankingAccount.Commands
{
    internal class PayOutCommand : ICommand
    {
        public readonly Guid Id;
        public readonly float Amount;

        public PayOutCommand(Guid accountId, float amount)
        {
            Id = accountId;
            Amount = amount;
        }
    }
}