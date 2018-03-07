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
        private static IRepository _repository;
        private static InMemoryDatabase _database;

        private static void Main()
        {
            Console.WriteLine("CQRSlite Banking Account sample");

            // Infrastructure setup
            _database = new InMemoryDatabase();
            _router = new Router();
            _repository = new Repository(new InMemoryEventStore(_router));

            var locator = new ServiceLocator(_router, _repository, _database);
            var registrar = new RouteRegistrar(locator);
            registrar.RegisterHandlers(
                    typeof(AccountCommandsHandler),
                    typeof(BankAccountsListView)
                );

            // start using it
            Console.WriteLine("Create bank accounts");
            CreateAccountRequest("Frank");
            CreateAccountRequest("Peter");

            DepositAccountRequest("Frank", 15);

            Console.WriteLine("First read model: List of available bank accounts");
            var list = GetAccounts();
            foreach (var account in list)
            {
                var balance = GetBalance(account.Id);
                Console.WriteLine($"Account {account.Name}: {balance.Balance}");
            }

            Console.WriteLine("Sample done.");
            Console.ReadLine();
        }

        private static BankAccountBalanceDto GetBalance(Guid accountId)
        {
            return _database.Get<BankAccountBalanceDto>(accountId) ?? new BankAccountBalanceDto(accountId, 0);
        }


        private static BankAccountListDto GetAccounts()
        {
            return _database.GetAll<BankAccountListDto>().FirstOrDefault()
                ?? new BankAccountListDto();
        }

        private static BankAccountDto GetAccountByName(string name)
        {
            var list = GetAccounts();
            return list.FirstOrDefault(a => a.Name == name);
        }


        private static void CreateAccountRequest(string name)
        {
            Console.WriteLine($"Request create account for {name}");

            Console.WriteLine("Validate request");
            var list = GetAccounts();
            if (list.Any(a => a.Name == name))
            {
                Console.WriteLine("Validation failed: Account already exists.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new CreateAccountCommand(Guid.NewGuid(), name));
        }

        private static void DepositAccountRequest(string name, float amount)
        {
            Console.WriteLine($"Request deposit to account {name}");

            Console.WriteLine("Validate request");
            var account = GetAccountByName(name);
            if(account == null)
            {
                Console.WriteLine("Validation failed: Account does not exist.");
                return;
            }

            Console.WriteLine("Validation succeeded - execute command.");
            _router.Send(new DepositAccountCommand(account.Id, amount));
        }

    }
}
