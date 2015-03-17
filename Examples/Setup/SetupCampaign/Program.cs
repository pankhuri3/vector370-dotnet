using System;
using System.Collections.Generic;

using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace SetupCampaign
{
    class Program
    {
        /*
         * NOTES:
         * 
         * A campaign only needs to be setup once. After the initial setup you
         * can send that campaign as many times as you would like.
         * 
         * Content and subscriptions can be reused accross multiple campaigns,
         * so there is no need to recreate these over and over again if you do
         * not want to do so.
         * 
         * Please review the various contants before you run this code and 
         * fill in the needed items with appropreate values.
         */

        #region Account Info

        /// <summary>
        /// TODO: Fill in with your account ID
        /// </summary>
        /// <remarks>
        /// You should have received your account ID when you first signed up with Vector.
        /// </remarks>
        private const int ACCOUNT_ID = 0;

        #endregion

        /// <summary>
        /// The name of the subscription
        /// </summary>
        /// <remarks>
        /// This is used when users need to opt out.  It must not contain spaces.
        /// </remarks>
        private const string SUBSCRIPTION_NAME = "testsub";

        /// <summary>
        /// The fancy label to use for the subscription.
        /// </summary>
        private const string SUBSCRIPTION_LABEL = "Test Sub";

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on durring execution.
            //InitLogging();

            // Username and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            Campaign campaign = CampaignSetup(context);


            Console.WriteLine("Campaign {0} has been created.", campaign.Id);
            Console.WriteLine("You can attach a keyword to this campaign or you can push it directly via a push event.");
            Console.WriteLine("For more details see the KeywordAttachExample and PushExample projects.");

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
            var content = CreateContent(context);

            var sub = CreateSubscription(context);

            var campaign = new Campaign
            {
                Name = "SDK Test Campaign",
                SubscriptionId = sub.Id,
                CampaignType = CampaignType.Basic,
                ContentId = content.Id
            };

            var campaignRepo = context.Repository<Campaign>(new { AccountId = ACCOUNT_ID });

            campaignRepo.Add(campaign);

            return campaign;
        }

        private static Content CreateContent(T70Context context)
        {
            /*
             * Content provides a grouping mechanisim that can be used to 
             * hold multiple translations for a signel campaign.
             */
            var contentRepo = context.Repository<Content>(new {AccountId = ACCOUNT_ID});

            var content = new Content
            {
                Name = "SDK Test Content",
            };

            contentRepo.Add(content);

            var templateRepo = context.Repository<ContentTemplate>(new {AccountId = ACCOUNT_ID, ContentId = content.Id});

            var template = new ContentTemplate
            {
                LanguageType = LanguageType.English,
                ChannelType = ChannelType.Sms,
                EncodingType = EncodingType.Text,
                Template = "Hi there from our demo!"
            };

            templateRepo.Add(template);

            // Also provide a French translation (not required)
            template = new ContentTemplate
            {
                LanguageType = LanguageType.French,
                ChannelType = ChannelType.Sms,
                EncodingType = EncodingType.Text,
                Template = "Bonjour!"
            };

            templateRepo.Add(template);

            return content;
        }

        private static Subscription CreateSubscription(T70Context context)
        {
            /* 
             * The subscription provides a way for a contact to opt into or out of a group of campaigns.
             * 
             * You might for example have one subscription for promotions related events and another for
             * contests.
             */
            var subRepo = context.Repository<Subscription>(new { AccountId = ACCOUNT_ID });

            var sub = new Subscription
            {
                Name = SUBSCRIPTION_NAME,
                Label = SUBSCRIPTION_LABEL,
            };
            
            subRepo.Add(sub);
            return sub;
        }
    }
}
