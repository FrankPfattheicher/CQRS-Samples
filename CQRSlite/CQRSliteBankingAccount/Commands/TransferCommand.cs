using System;
using CQRSlite.Commands;

namespace CQRSliteBankingAccount.Commands
{
    internal class TransferCommand : ICommand
    {
        public readonly Guid FromAccountId;
        public readonly Guid ToAccountId;
        public readonly float Amount;

        public TransferCommand(Guid fromAccountId, Guid toAccountId, float amount)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
        }
    }
}