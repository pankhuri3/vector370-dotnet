using System;
using System.Collections.Generic;

using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace SetupKeyword
{
    class Program
    {
        // Account info
        private const int ACCOUNT_ID = 0;       // TODO: Fill in with your account ID

        /// Keyword info
        private const string KEYWORD_NAME = ""; // TODO: Fill in with the keyword name you wish to reserve.

        private const int CHANNEL_ID = 11;      // This is the 370370 short code

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on durring execution.
            //InitLogging();

            // Username and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            Keyword keyword = KeywordSetup(context);
            
            Console.WriteLine("Keyword {0} (ID: {1}) setup on channel {2}", keyword.Name, keyword.Id, CHANNEL_ID);

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

        private static Keyword KeywordSetup(T70Context context)
        {
            /*
             * NOTE: You only need to create a keyword once.
             * 
             * After it is created you can attach and detach campaigns from it as needed.
             */
            var keyword = new Keyword
            {
                Name = KEYWORD_NAME
            };

            var keywordRepo = context.Repository<Keyword>(new {AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID});

            keywordRepo.Add(keyword);

            return keyword;
        }
    }
}
