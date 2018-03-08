﻿using System;
using CQRSlite.Events;

namespace CQRSliteBankingAccount.Events
{
    internal class MoneyReceived : IEvent
    {
        public readonly float Amount;

        public MoneyReceived(Guid id, float amount)
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
