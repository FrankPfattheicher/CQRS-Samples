using System;
using System.Linq;
using CQRSlite.Domain;
using CQRSlite.Routing;
using CQRSliteBankingAccount.Commands;
using CQRSliteBankingAccount.Infrastructure;
using CQRSliteBankingAccount.Projections;

namespace CQRSliteBankingAccount
{
    internal class Program
    {
        private static Router _router;  // ICommandSender, IEventPublisher, IHandlerRegistrar
        private static InMemoryDatabase _database;
        private static InMemoryEventStore _eventStore;

        private static void Main()
        {
            Console.WriteLine("CQRSlite Banking Account sample");

            // Infrastructure setup
            _database = new InMemoryDatabase();
            _router = new Router();
            _eventStore = new InMemoryEventStore(_router);

            var locator = new ServiceLocator(_router, _eventStore, _database);
            var registrar = new RouteRegistrar(locator);
            registrar.RegisterHandlers(
                    typeof(AccountCommandsHandler),
                    typeof(BankAccountsListView)
                );

            // start using it
            Console.WriteLine("Create bank accounts");
            CreateAccountRequest("Frank");
            CreateAccountRequest("Peter");

            PayInRequest("Frank", 15);
            TransferRequest("Frank", "Peter", 10);
            PayOutRequest("Peter", 5);

            Console.WriteLine("First read model: List of available bank accounts");
            var list = _database.GetAccounts();
            foreach (var account in list)
            {
                var balance = _database.GetAccountBalanceDto(account.Id);
                Console.WriteLine($"Account {account.Name}: {balance.Balance}");
            }

            Console.WriteLine("Sample done.");
            Console.ReadLine();
        }




        private static void CreateAccountRequest(string name)
        {
            Console.WriteLine($"Request create account for {name}");

            Console.WriteLine("Validate request");
            var list = _database.GetAccounts();
            if (list.Any(a => a.Name == name))
            {
                Console.WriteLine("Validation failed: Account already exists.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new CreateAccountCommand(Guid.NewGuid(), name));
        }

        private static void PayInRequest(string name, float amount)
        {
            Console.WriteLine($"Request to pay in for an account {name}");

            Console.WriteLine("Validate request");
            if (amount <= 0)
            {
                Console.WriteLine("Validation failed: Amount must be positive value.");
                return;
            }

            var account = _database.GetAccountByName(name);
            if(account == null)
            {
                Console.WriteLine("Validation failed: Account does not exist.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new PayInCommand(account.Id, amount));
        }

        private static void PayOutRequest(string name, float amount)
        {
            Console.WriteLine($"Request to pay in for an account {name}");

            Console.WriteLine("Validate request");
            if (amount <= 0)
            {
                Console.WriteLine("Validation failed: Amount must be positive value.");
                return;
            }

            var account = _database.GetAccountByName(name);
            if (account == null)
            {
                Console.WriteLine("Validation failed: Account does not exist.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new PayOutCommand(account.Id, amount));
        }

        private static void TransferRequest(string fromName, string toName, float amount)
        {
            Console.WriteLine($"Request to transfer {amount} from {fromName} to {toName}");

            Console.WriteLine("Validate request");
            if(amount <= 0)
            {
                Console.WriteLine("Validation failed: Amount to transfer must be positive value.");
                return;
            }

            var fromAccount = _database.GetAccountByName(fromName);
            if (fromAccount == null)
            {
                Console.WriteLine("Validation failed: From-account does not exist.");
                return;
            }
            var toAccount = _database.GetAccountByName(toName);
            if (toAccount == null)
            {
                Console.WriteLine("Validation failed: To-account does not exist.");
                return;
            }

            if (fromAccount.Id == toAccount.Id)
            {
                Console.WriteLine("Validation failed: From-account is equal to To-account.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new TransferCommand(fromAccount.Id, toAccount.Id, amount));
        }

    }
}
