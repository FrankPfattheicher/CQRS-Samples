using System;
using CQRSlite.Domain;
using CQRSliteBankingAccount.Events;
// ReSharper disable UnusedMember.Local
// ReSharper disable NotAccessedField.Local
#pragma warning disable 414

namespace CQRSliteBankingAccount.Domain
{
    internal class BankAccount : AggregateRoot
    {
        private string _name;
        private bool _active;
        private float _balance;

        private BankAccount() { }
        public BankAccount(Guid id, string name)
        {
            Id = id;
            ApplyChange(new AccountCreated(id, name));
        }

        private void Apply(AccountCreated e)
        {
            _name = e.Name;
            _active = true;
            _balance = 0;
        }

        private void Apply(MoneyReceived e)
        {
            _balance += e.Amount;
        }

        private void Apply(MoneyRemoved e)
        {
            _balance -= e.Amount;
        }


        public void AddMoney(float amount)
        {
            ApplyChange(new MoneyReceived(Id, amount));
        }

        public void RemoveMoney(float amount)
        {
            ApplyChange(new MoneyRemoved(Id, amount));
        }
    }
}