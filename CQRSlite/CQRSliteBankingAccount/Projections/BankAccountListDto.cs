using System;
using System.Collections.Generic;

namespace CQRSliteBankingAccount.Projections
{
    class BankAccountListDto : List<BankAccountDto>
    {
        public readonly Guid Id;

        public BankAccountListDto()
        {
            Id = Guid.NewGuid();
        }
    }
}
