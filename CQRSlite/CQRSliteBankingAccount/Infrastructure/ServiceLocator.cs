using System;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Routing;
using CQRSliteBankingAccount.Commands;
using CQRSliteBankingAccount.Projections;

namespace CQRSliteBankingAccount.Infrastructure
{
    internal class ServiceLocator : IServiceProvider
    {
        private readonly IHandlerRegistrar _registrar;
        private readonly IEventStore _eventStore;
        private readonly InMemoryDatabase _database;

        public ServiceLocator(IHandlerRegistrar registrar, IEventStore eventStore, InMemoryDatabase database)
        {
            _registrar = registrar;
            _eventStore = eventStore;
            _database = database;
        }

        public object GetService(Type type)
        {
            if (type == typeof(IHandlerRegistrar))
                return _registrar;

            // command handlers
            if (type == typeof(AccountCommandsHandler))
            {
                return new AccountCommandsHandler(new Session(new Repository(_eventStore)));
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