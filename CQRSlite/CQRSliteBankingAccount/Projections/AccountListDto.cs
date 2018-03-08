using System;
using System.Collections.Generic;

namespace CQRSliteBankingAccount.Projections
{
    class AccountListDto : List<BankAccountDto>
    {
        public readonly Guid Id;

        public AccountListDto()
        {
            Id = Guid.NewGuid();
        }
    }
}
