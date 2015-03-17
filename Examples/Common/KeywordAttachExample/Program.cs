using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace KeywordAttachExample
{
    class Program
    {
        // Account info
        private const int ACCOUNT_ID = 0;   // TODO: Fill in with your account ID
        private const int KEYWORD_ID = 0;   // TODO: Fill in with your keyword ID
        private const int CAMPAIGN_ID = 0;  // TODO: Fill in with your campaign ID
        private const int CHANNEL_ID = 0;  // This is the 370370 short code

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on durring execution.
            //InitLogging();

            // Username and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();
            //Createkeyword(context);
            //KeywordAddRange(context);
            //KeywordDetails(context);
            //KeywordGetAll(context);
            //KeywordUpdate(context);
            //KeywordUpdateRange(context);
            //KeywordDelete(context);
            //KeywordDeleteAll(context);

            //Keyword kw = AttachKeyword(context);

            //Console.WriteLine(
            //    "Keyword {0} attached to campaign {1}, try texting \"{2}\" to channel ID {3} to see your message.",
            //    kw.Id,
            //    CAMPAIGN_ID,
            //    kw.Name,
            //    kw.ChannelId);

            //Console.WriteLine();
            //Console.WriteLine("Press a key to detach the keyword");
            //Console.ReadKey();

            //DetachKeyword(context, kw);

            //Console.WriteLine(
            //    "Keyword {0} now detached, try texting \"{1}\" to channel ID {2}.",
            //    kw.Id,
            //    kw.Name,
            //    kw.ChannelId);

            //Console.WriteLine("(You should get a \"Message not understood\" response)");

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

        private static void DisplayKeyword(Keyword updateKeyword)
        {
            Console.WriteLine("Id: {0}", updateKeyword.Id);
            Console.WriteLine("AccountId: {0}", updateKeyword.AccountId);
            Console.WriteLine("Name: {0}", updateKeyword.Name);
            Console.WriteLine("CallbackRequired: {0}", updateKeyword.CallbackRequired);
            Console.WriteLine("StatusID: {0}", updateKeyword.StatusId);
            Console.WriteLine("Status: {0}", updateKeyword.Status);
            Console.WriteLine("Created: {0}", updateKeyword.Created);
            Console.WriteLine();
        }

        private static Keyword AttachKeyword(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new
            {
                AccountId = ACCOUNT_ID,
                ChannelId = CHANNEL_ID
            });

            var keyword = keywordRepo.Get(KEYWORD_ID);

            if (keyword.CampaignId.HasValue)
            {
                if (keyword.CampaignId == CAMPAIGN_ID)
                {
                    Console.WriteLine("Keyword {0} already attached to campaign {1}", keyword.Id, CAMPAIGN_ID);
                    return keyword;
                }

                Console.WriteLine(
                    "NOTICE: Keyword {0} is already attached to campaign {1}, this will get changed.",
                    keyword.Id,
                    keyword.CampaignId);
            }

            context.AttachKeywordTo(keyword, CAMPAIGN_ID);
            
            return keyword;
        }

        static void KeywordGetAll(T70Context context)
        {

            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            List<Keyword> keywordlist = (keywordRepo.GetAll()).ToList();

            foreach (var keyword in keywordlist)
                DisplayKeyword(keyword);
        }

        static void Createkeyword(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            var keyword = new Keyword
            {
                Name = "Keyword_SDK_Test1",
                CallbackRequired = false
            };

            keywordRepo.Add(keyword);
            Console.WriteLine("Added Keyword {0}: {1}", keyword.Id, keyword.Name);
        }

        static void KeywordAddRange(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            var keywordList = new List<Keyword>
            {
                new Keyword { Name = "Keyword_SDK_Test1" },
                new Keyword { Name = "Keyword_SDK_Test2" }
            };

            keywordRepo.AddRange(keywordList);

            foreach (Keyword akeyword in keywordList)
            {
                Console.WriteLine("Added keyword {0}: {1}", akeyword.Id, akeyword.Name);
                Console.WriteLine();
            }
        }

        static void KeywordDetails(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            var keyword = keywordRepo.Get(KEYWORD_ID);

            Console.WriteLine("Got Keyword:");
            DisplayKeyword(keyword);
        }

        static void KeywordUpdate(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });
            var keyword = new Keyword
            {
                Id = KEYWORD_ID,
                CallbackRequired = true
            };

            keywordRepo.Update(keyword);
            var updateKeyword = keywordRepo.Get(KEYWORD_ID);
            
            Console.WriteLine("Updated Keyword:");
            DisplayKeyword(updateKeyword);
        }
        
        static void KeywordDelete(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            var keyword = new Keyword
            {
                Id = KEYWORD_ID
            };

            keywordRepo.Delete(keyword);
            var deletekeyword = keywordRepo.Get(KEYWORD_ID);

            Console.WriteLine("Deleted Keyword");
            DisplayKeyword(keyword);
        }

        static void KeywordUpdateRange(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            // TODO: Remove hard coded ID values!
            var keywordList = new List<Keyword>
            {
                new Keyword() { Id = 0, Name = "Keyword_SDK_Test_update by add range" },
                new Keyword() { Id = 0, Name = "Keyword_SDK_Test_update by add range" }
            };

            keywordRepo.UpdateRange(keywordList);

            foreach (Keyword aKeyword in keywordList)
            {
                Console.WriteLine("Update contact {0}: {1}", aKeyword.Id, aKeyword.Name);
                Console.WriteLine();
            }

        }

        static void KeywordDeleteAll(T70Context context)
        {
            var keywordRepo = context.Repository<Keyword>(new { AccountId = ACCOUNT_ID, ChannelId = CHANNEL_ID });

            keywordRepo.DeleteAll(keywordRepo.GetAll().ToList());

            Console.WriteLine("Delete all keyword successfully");
        }

        private static void DetachKeyword(T70Context context, Keyword keyword)
        {
            context.AttachKeywordTo(keyword, null);
        }
    }
}
