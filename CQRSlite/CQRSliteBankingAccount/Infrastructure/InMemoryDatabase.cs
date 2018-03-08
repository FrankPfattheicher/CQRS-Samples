using System;
using System.Collections.Generic;
using System.Linq;
using CQRSliteBankingAccount.Projections;

namespace CQRSliteBankingAccount.Infrastructure
{
    internal class InMemoryDatabase
    {
        private readonly Dictionary<Guid, object> _storage;

        public InMemoryDatabase()
        {
            _storage = new Dictionary<Guid, object>();
        }

        private void Set(Guid id, object data)
        {
            if (!_storage.ContainsKey(id))
            {
                _storage.Add(id, data);
                return;
            }

            _storage[id] = data;
        }

        private T Get<T>(Guid id) where T : class
        {
            if (!_storage.ContainsKey(id))
                return null;

            return _storage[id] as T;
        }

        private  List<T> GetAll<T>() where T : class
        {
            var all = _storage
                .Select(d => d.Value as T)
                .Where(d => d != null)
                .ToList();

            return all;
        }


        public AccountListDto GetAccounts()
        {
            return GetAll<AccountListDto>().FirstOrDefault() ?? new AccountListDto();
        }

        public BankAccountDto GetAccountByName(string name)
        {
            return GetAccounts().FirstOrDefault(a => a.Name == name);
        }

        public AccountBalanceDto GetAccountBalanceDto(Guid accountId)
        {
            return Get<AccountBalanceDto>(accountId) ?? new AccountBalanceDto(accountId, 0);
        }

        public void SaveAccountBalanceDto(AccountBalanceDto balance)
        {
            Set(balance.Id, balance);
        }

        public AccountListDto GetAccountListDto()
        {
            return GetAll<AccountListDto>().FirstOrDefault() ?? new AccountListDto();
        }

        public void SaveAccountListDto(AccountListDto list)
        {
            Set(list.Id, list);
        }
    }
}
