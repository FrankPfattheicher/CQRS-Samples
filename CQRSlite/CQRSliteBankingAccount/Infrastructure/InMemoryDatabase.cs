using System;
using System.Collections.Generic;
using System.Linq;

namespace CQRSliteBankingAccount.Infrastructure
{
    class InMemoryDatabase
    {
        private readonly Dictionary<Guid, object> _storage;

        public InMemoryDatabase()
        {
            _storage = new Dictionary<Guid, object>();
        }

        public void Set(Guid id, object data)
        {
            if (!_storage.ContainsKey(id))
            {
                _storage.Add(id, data);
                return;
            }

            _storage[id] = data;
        }

        public T Get<T>(Guid id) where T : class
        {
            if (!_storage.ContainsKey(id))
                return null;

            return _storage[id] as T;
        }

        public List<T> GetAll<T>() where T : class
        {
            var all = _storage
                .Select(d => d.Value as T)
                .Where(d => d != null)
                .ToList();

            return all;
        }

    }
}
