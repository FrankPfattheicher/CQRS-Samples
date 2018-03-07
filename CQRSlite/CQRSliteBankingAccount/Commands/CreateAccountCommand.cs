using System;
using CQRSlite.Commands;

namespace CQRSliteBankingAccount.Commands
{
    internal class CreateAccountCommand : ICommand
    {
        public readonly Guid Id;
        public readonly string Name;

        public CreateAccountCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
