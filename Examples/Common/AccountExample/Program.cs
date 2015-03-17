using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;


namespace AccountExample
{
    class Program
    {
        /*
         * NOTE:
         * Please review the various constants before you run this code and 
         * fill in the needed items with appropriate values.
         */

        // Account info
        private const int ACCOUNT_ID = 0; // TODO: Fill in with your account ID
        private const int PARENT_ID = 0;

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on during execution.
            //InitLogging();

            // User name and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            DetailsExample(context);
            GetAllExample(context);
            
            // The following examples make changes to the accounts, use with care.
            //CreateAccountExample(context);
            //UpdateAccountExample(context);
            
            Console.WriteLine();
            Console.WriteLine("Done, press a key to quit.");
            Console.ReadKey();
        }

        private static void InitLogging()
        {
            var properties = new NameValueCollection();
            properties["showDateTime"] = "true";

            Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter(properties);
        }

        public static void ShowAccountDetails(Account account)
        {
            Console.WriteLine("AccountId: {0}", account.Id);
            Console.WriteLine("ParentId: {0}", account.ParentId);
            Console.WriteLine("Name: {0}", account.Name);
            Console.WriteLine("StatusID: {0}", account.StatusId);
            Console.WriteLine("Status: {0}", account.Status);
            Console.WriteLine("Created: {0}", account.Created);
        }

        static void CreateAccountExample(T70Context context)
        {
            var accountRepo = context.Repository<Account>(new { });

            var account = new Account
            {
                ParentId = PARENT_ID,
                Name = "TestSubAccount"
            };

            accountRepo.Add(account);
            Console.WriteLine("Added account {0}: {1}", account.Id, account.Name);
        }

        static void DetailsExample(T70Context context)
        {
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });

            var account = accountRepo.Get(ACCOUNT_ID);

            ShowAccountDetails(account);
            Console.WriteLine();
        }

        static void GetAllExample(T70Context context)
        {
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });

            List<Account> contactlist = (accountRepo.GetAll()).ToList();

            foreach (Account account in contactlist)
            {
                ShowAccountDetails(account);
                Console.WriteLine();
            }
        }

        static void UpdateAccountExample(T70Context context)
        {
            var accountRepo = context.Repository<Account>(new { ParentId = PARENT_ID });

            var account = new Account
            {
                Id = ACCOUNT_ID,
                Name = "UpdatedTestAccount"
            };

            accountRepo.Update(account);
            var updateAccount = accountRepo.Get(ACCOUNT_ID);

            ShowAccountDetails(updateAccount);
            Console.WriteLine();
        }
    }
}
