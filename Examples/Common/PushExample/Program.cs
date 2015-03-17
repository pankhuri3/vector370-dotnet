using System;
using System.Collections.Generic;

using Common.Logging.Configuration;

using ThreeSeventy.Vector.Client;
using ThreeSeventy.Vector.Client.Models;

namespace PushExample
{
    class Program
    {
        /*
         * NOTE:
         * Please review the various constants before you run this code and 
         * fill in the needed items with appropriate values.
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

        #region Event Info
        
        /// <summary>
        /// TODO: Fill in with the basic campaign you have already setup.
        /// </summary>
        /// <remarks>
        /// See the SetupCampaign project for more details
        /// </remarks>
        private const int CAMPAIGN_ID = 0;

        /// <summary>
        /// TODO: Fill in with a phone # you wish to send to.
        /// </summary>
        /// <remarks>
        /// NOTE: Phone numbers must have the international dialing prefix. (For the U.S. and Canada that is "+1")
        /// </remarks>
        private const string SEND_TO = "";

        /// <summary>
        /// This is the short code you wish to send from.
        /// </summary>
        /// <remarks>
        /// Channel ID 11 is the 370370 short code.
        /// </remarks>
        private const int CHANNEL_ID = 11;

        #endregion

        static void Main(string[] args)
        {
            // Uncomment out the following line to see more details about what is going on during execution.
            //InitLogging();

            // User name and password are pulled from App.config by default.
            // (For web applications this will be Web.config)
            var context = new T70Context();

            var eventPushRepo = context.Repository<EventPushCampaign>(new {AccountId = ACCOUNT_ID});

            PushCampaignExample(eventPushRepo);

            //GetPushEventExample(eventPushRepo);

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

        private static void GetPushEventExample(IRepository<EventPushCampaign> eventPushRepo)
        {
            var e = eventPushRepo.Get(4251);

            Console.WriteLine("Fetched event: {0}", e.Id);
            Console.WriteLine("Channels: {0}", String.Join(", ", e.ChannelIds));
            Console.WriteLine("Targets: {0}", String.Join(", ", e.Targets));
        }
        
        private static void PushCampaignExample(IRepository<EventPushCampaign> eventPushRepo)
        {
            var pushEvent = new EventPushCampaign
            {
                CampaignId = CAMPAIGN_ID,
                ChannelIds = { CHANNEL_ID }, // You can push to several channels in one go if you wish.
                Targets = { SEND_TO }, // Note that is possible to send to more than one target at a time.
                ScheduleType = ScheduleType.Now
            };

            eventPushRepo.Add(pushEvent);

            Console.WriteLine(
                "Sent campaign {0} to channel(s) {1}",
                CAMPAIGN_ID,
                String.Join(", ", pushEvent.ChannelIds));
        }
    }
}
