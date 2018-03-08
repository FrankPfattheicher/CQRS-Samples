using System;

namespace CQRSliteBankingAccount.Projections
{
    internal class AccountBalanceDto
    {
        public readonly Guid Id;
        public float Balance;

        public AccountBalanceDto(Guid id, long balance)
        {
            Id = id;
            Balance = balance;
        }
    }
}
