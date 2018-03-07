using System;
using CQRSlite.Domain;
using CQRSlite.Routing;
using CQRSliteBankingAccount.Commands;
using CQRSliteBankingAccount.Projections;

namespace CQRSliteBankingAccount.Infrastructure
{
    internal class ServiceLocator : IServiceProvider
    {
        private readonly IHandlerRegistrar _registrar;
        private readonly IRepository _repository;
        private readonly InMemoryDatabase _database;

        public ServiceLocator(IHandlerRegistrar registrar, IRepository repository, InMemoryDatabase database)
        {
            _registrar = registrar;
            _repository = repository;
            _database = database;
        }

        public object GetService(Type type)
        {
            if (type == typeof(IHandlerRegistrar))
                return _registrar;

            // command handlers
            if (type == typeof(AccountCommandsHandler))
            {
                return new AccountCommandsHandler(_repository);
            }

            // read model handlers
            if (type == typeof(BankAccountsListView))
            {
                return new BankAccountsListView(_database);
            }
            

            return null;
        }
    }
}