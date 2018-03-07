using System;
using CQRSlite.Events;

namespace CQRSliteBankingAccount.Events
{
    internal class AccountDeposited : IEvent
    {
        public readonly float Amount;

        public AccountDeposited(Guid id, float amount)
        {
            Id = id;
            TimeStamp = DateTimeOffset.Now;
            Amount = amount;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}
