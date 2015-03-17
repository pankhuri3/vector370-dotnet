using System;
using System.Collections.Generic;

using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace SetupCampaign
{
    class Program
    {
        // Account info
        private const int ACCOUNT_ID = 0; // TODO: Fill in with your account ID

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on durring execution.
            //InitLogging();

            // Username and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            Campaign campaign = CampaignSetup(context);

            Console.WriteLine("Campaign {0} has been created", campaign);

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

        private static Campaign CampaignSetup(T70Context context)
        {
            /*
             * NOTE: A campaign only needs to be setup once.
             * 
             * After the initial setup you can send that campaign as many times as you would like.
             */
            var contentRepo = context.Repository<Content>(new { AccountId = ACCOUNT_ID });

            var content = new Content
            {
                Name = "SDK Test Content",
            };

            contentRepo.Add(content);

            var templateRepo = context.Repository<ContentTemplate>(new { AccountId = ACCOUNT_ID, ContentId = content.Id });

            var template = new ContentTemplate
            {
                LanguageType = LanguageType.English,
                ChannelType = ChannelType.Sms,
                EncodingType = EncodingType.Text,
                Template = "Hi there from our demo!"
            };

            templateRepo.Add(template);

            var campaign = new Campaign
            {
                Name = "SDK Test Campaign",
                SubscriptionId = 183,
                CampaignType = CampaignType.Basic,
                ContentId = content.Id
            };

            var campaignRepo = context.Repository<Campaign>(new { AccountId = ACCOUNT_ID });

            campaignRepo.Add(campaign);

            return campaign;
        }
    }
}
