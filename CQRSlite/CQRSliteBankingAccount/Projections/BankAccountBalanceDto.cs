using System;

namespace CQRSliteBankingAccount.Projections
{
    internal class BankAccountBalanceDto
    {
        public readonly Guid Id;
        public float Balance;

        public BankAccountBalanceDto(Guid id, long balance)
        {
            Id = id;
            Balance = balance;
        }
    }
}
