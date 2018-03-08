using System;
using CQRSlite.Commands;

namespace CQRSliteBankingAccount.Commands
{
    internal class PayInCommand : ICommand
    {
        public readonly Guid Id;
        public readonly float Amount;

        public PayInCommand(Guid accountId, float amount)
        {
            Id = accountId;
            Amount = amount;
        }
    }
}