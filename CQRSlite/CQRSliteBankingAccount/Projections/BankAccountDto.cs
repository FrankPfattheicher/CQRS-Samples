using System;

namespace CQRSliteBankingAccount.Projections
{
    internal class BankAccountDto
    {
        public readonly Guid Id;
        public readonly string Name;

        public BankAccountDto(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
