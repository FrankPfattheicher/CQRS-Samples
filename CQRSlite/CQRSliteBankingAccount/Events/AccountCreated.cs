using System;
using CQRSlite.Events;

namespace CQRSliteBankingAccount.Events
{
    internal class AccountCreated : IEvent
    {
        public readonly string Name;

        public AccountCreated(Guid id, string name)
        {
            Id = id;
            TimeStamp = DateTimeOffset.Now;
            Name = name;
        }

        public Guid Id { get; set; }
        public int Version { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
    }
}