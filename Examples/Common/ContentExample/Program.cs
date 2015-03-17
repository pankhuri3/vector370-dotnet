using System;
using System.Collections.Generic;
using System.Linq;
using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace ContentExample
{
    class Program
    {
        // Account info
        private const int ACCOUNT_ID = 0;           // TODO: Fill in with your account ID
        private const int CONTENT_ID = 0;           // TODO: Fill in with an existing contact ID

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on durring execution.
            //InitLogging();

            // Username and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            //CreateContent(context);
            //ContentAddRange(context);
            //ContentDetails(context);
            //ContentGetAll(context);
            //ContentUpdate(context);
            //ContactDeleteExample(context);
            //ContentUpdateRange(context);
            
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

        static void CreateContent(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var content = new Content
            {
                Name = "SDK_Test_Content190",
                Description = "Content 190 for test .net SDK by Ravi"
            };

            contentRepo.Add(content);
            Console.WriteLine("Added content {0}: {1}", content.Id, content.Name);
        }

        static void ContentAddRange(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var contentList = new List<Content>
            {
                new Content { Name = "SDK_Test_Content2", Description = "Content 2 for test .net SDK." },
                new Content { Name = "SDK_Test_Content3", Description = "Content 3 for test .net SDK." }
            };

            contentRepo.AddRange(contentList);

            foreach (Content aContent in contentList)
            {
                Console.WriteLine("Added content {0}: {1}", aContent.Id, aContent.Name);
                Console.WriteLine();
            }
        }

        static void ContentDetails(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var content = contentRepo.Get(CONTENT_ID);

            Console.WriteLine("Id: {0}", content.Id);
            Console.WriteLine("AccountId: {0}", content.AccountId);
            Console.WriteLine("Name: {0}", content.Name);
            Console.WriteLine("Description: {0}", content.Description);
            Console.WriteLine("Created: {0}", content.Created);
            Console.WriteLine("Modified: {0}", content.Modified);

        }

        static void ContentGetAll(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            List<Content> contentlist = (contentRepo.GetAll()).ToList();

            foreach (Content content in contentlist)
            {
                Console.WriteLine("Id: {0}", content.Id);
                Console.WriteLine("AccountId: {0}", content.AccountId);
                Console.WriteLine("Name: {0}", content.Name);
                Console.WriteLine("Description: {0}", content.Description);
                Console.WriteLine("Created: {0}", content.Created);
                Console.WriteLine("Modified: {0}", content.Modified);
                Console.WriteLine();
            }
        }

        static void ContentUpdate(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var content = new Content
            {
                Id = CONTENT_ID,
                Name = "SDK_Test_Content3",
                Description = "Content 3 for test .net SDK."
            };

            contentRepo.Update(content);
            var updateContent = contentRepo.Get(CONTENT_ID);

            Console.WriteLine("Id: {0}", updateContent.Id);
            Console.WriteLine("AccountId: {0}", updateContent.AccountId);
            Console.WriteLine("Name: {0}", updateContent.Name);
            Console.WriteLine("Description: {0}", updateContent.Description);
            Console.WriteLine("Created: {0}", updateContent.Created);
            Console.WriteLine("Modified: {0}", updateContent.Modified);
            Console.WriteLine();
            Console.WriteLine("Content updated successfully");
        }

        static void ContactDeleteExample(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var content = new Content
            {
                Id = CONTENT_ID
            };

            contentRepo.Delete(content);
            Console.WriteLine("Content deleted successfully");
        }

        private static void ContentUpdateRange(T70Context context)
        {
            var contentRepo = context.Repository<Content>(new {AccountId = ACCOUNT_ID});

            // TODO: Remove hard coded ID values!
            var contentList = new List<Content>
            {
                new Content {Id = 0, Name = "Content 0", Description = "Content 0 for test content"},
                new Content {Id = 0, Name = "Content 0", Description = "Content 0 for test content"}
            };

            contentRepo.UpdateRange(contentList);

            foreach (Content aContent in contentList)
            {
                Console.WriteLine("Update content {0}: {1}", aContent.Id, aContent.Name);
                Console.WriteLine();
            }
        }
    }
}
