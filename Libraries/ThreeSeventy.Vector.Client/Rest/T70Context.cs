using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using RestSharp;

using ThreeSeventy.Vector.Client.Models;
using ThreeSeventy.Vector.Client.Utils;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// The 3Seventy REST context.
    /// </summary>
    /// <remarks>
    /// Specific implementation of RestContext
    /// </remarks>
    public class T70Context : RestContext
    {
        private const string KEYWORD_ATTACH_URI = "/account/{accountId}/channel/{channelId}/keyword/{keywordId}/campaign";

        private const string CONTACT_SEARCH_URI = "/account/{accountId}/contact-search/";

        private readonly ILog m_log = LogManager.GetLogger(typeof(T70Context));

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="config">Configuration details</param>
        /// <param name="clientFactory">The IRestClientFactory implementation to use (OPTIONAL)</param>
        public T70Context(IConfiguration config, IRestClientFactory clientFactory = null)
            : base(config, clientFactory)
        {
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks>
        /// This will setup a T70Context which hits production.
        /// </remarks>
        public T70Context(IRestClientFactory clientFactory = null)
            : this(VectorConfigSection.GetConfig(), clientFactory)
        {
        }

        /// <summary>
        /// Sets up the model for the 3Seventy REST clients.
        /// </summary>
        /// <param name="modelBuilder">The model building configuration object.</param>
        override protected void OnModelCreating(RestModelBuilder modelBuilder)
        {
            AccountSetup(modelBuilder);

            ContactSetup(modelBuilder);

            ContentSetup(modelBuilder);

            SubscriptionSetup(modelBuilder);

            CampaignSetup(modelBuilder);

            KeywordSetup(modelBuilder);

            EventSetup(modelBuilder);

            CallbackSetup(modelBuilder);

            m_log.Trace("Model setup");
        }

        #region Model Setup

        private static void AccountSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
                .UriFormat("/account/{accountId}")
            ;

            modelBuilder.Entity<Account>()
                .Property(a => a.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("accountId")
            ;
        }

        private static void ContactSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>()
                .UriFormat("/account/{accountId}/contact/{contactId}")
            ;

            modelBuilder.Entity<Contact>()
                .Property(c => c.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("contactId")
            ;

            modelBuilder.Entity<Contact>()
                .Property(c => c.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void ContentSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>()
                .UriFormat("/account/{accountId}/content/{contentId}")
            ;

            modelBuilder.Entity<Content>()
                .Property(a => a.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("contentId")
            ;

            modelBuilder.Entity<Content>()
                .Property(a => a.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;

            modelBuilder.Entity<ContentTemplate>()
                .UriFormat("/account/{accountId}/content/{contentId}/template/{templateId}")
            ;

            modelBuilder.Entity<ContentTemplate>()
                .Property(a => a.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("templateId")
            ;

            modelBuilder.Entity<ContentTemplate>()
                .Property(a => a.ContentId)
                .UrlSegment()
                .MapTo("contentId")
            ;

            modelBuilder.Entity<ContentTemplate>()
                .Property(a => a.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void SubscriptionSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subscription>()
                .UriFormat("/account/{accountId}/subscription/{subscriptionId}")
                ;

            modelBuilder.Entity<Subscription>()
                .Property(s => s.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("subscriptionId")
            ;

            modelBuilder.Entity<Subscription>()
                .Property(s => s.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void CampaignSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Campaign>()
                .UriFormat("/account/{accountId}/campaign/{campaignId}")
            ;

            modelBuilder.Entity<Campaign>()
                .Property(c => c.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("campaignId")
            ;

            modelBuilder.Entity<Campaign>()
                .Property(c => c.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void KeywordSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Keyword>()
                .UriFormat("/account/{accountId}/channel/{channelId}/keyword/{keywordId}")
            ;

            modelBuilder.Entity<Keyword>()
                .Property(k => k.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("keywordId")
            ;

            modelBuilder.Entity<Keyword>()
                .Property(k => k.ChannelId)
                .UrlSegment()
                .MapTo("channelId")
            ;

            modelBuilder.Entity<Keyword>()
                .Property(k => k.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void EventSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventPushCampaign>()
                .UriFormat("/account/{accountId}/event-pushcampaign/{eventId}")
            ;

            modelBuilder.Entity<EventPushCampaign>()
                .Property(e => e.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("eventId")
            ;

            modelBuilder.Entity<EventPushCampaign>()
                .Property(e => e.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        private static void CallbackSetup(RestModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Callback>()
                .UriFormat("/account/{accountId}/callback/{callbackId}")
            ;

            modelBuilder.Entity<Callback>()
                .Property(c => c.Id)
                .OptionalUrlSegment()
                .PrimaryKey()
                .MapTo("callbackId")
            ;

            modelBuilder.Entity<Callback>()
                .Property(c => c.AccountId)
                .UrlSegment()
                .MapTo("accountId")
            ;
        }

        #endregion Model Setup

        #region Utility Function Support

        private void Execute(IRestRequest request)
        {
            var client = GetClient();

            Uri uri = client.BuildUri(request);

            m_log.Trace(m => m("Performing {0} request: {1}", request.Method, uri));

            client.ExecuteRequestFor(
                GetRetryPolicy(),
                request,
                false);
        }

        private TEntity Execute<TEntity>(IRestRequest request)
            where TEntity : class, new()
        {
            var client = GetClient();

            Uri uri = client.BuildUri(request);

            m_log.Trace(m => m("Performing {0} request: {1}", request.Method, uri));

            var response = client.ExecuteRequestFor(
                GetRetryPolicy(),
                request,
                false);

            return response.Deserialize<TEntity>();
        }

        #endregion Utility Function Support

        #region Utility Functions

        // These functions are for performing operations which have REST data that do not map well to a strict model.

        /// <summary>
        /// Provides a way to attach (or detach) a keyword to (from) a campaign.
        /// </summary>
        /// <param name="keyword">The keyword to attach (or detach)</param>
        /// <param name="campaignId">The ID of the campaign to attach to (NULL to detach the keyword)</param>
        public void AttachKeywordTo(Keyword keyword, int? campaignId)
        {
            if (keyword == null)
                throw new ArgumentNullException("keyword");
            
            IRestRequest request;

            if (!campaignId.HasValue)
            {
                request = new RestRequest(BaseUri + KEYWORD_ATTACH_URI, Method.DELETE)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new NewtonsoftSerializer()
                };
            }
            else
            {
                request = new RestRequest(BaseUri + KEYWORD_ATTACH_URI, Method.POST)
                {
                    RequestFormat = DataFormat.Json,
                    JsonSerializer = new NewtonsoftSerializer()
                };

                request.AddBody(campaignId.Value);
            }

            request.AddUrlSegment("keywordId", keyword.Id.ToString());
            request.AddUrlSegment("channelId", keyword.ChannelId.ToString());
            request.AddUrlSegment("accountId", keyword.AccountId.ToString());

            Execute(request);
        }

        /// <summary>
        /// Searches for a contact via a wild card pattern.
        /// </summary>
        /// <remarks>
        /// This provides a way to search for a contact via a wild card which will
        /// match to a PhoneNumber or Email address.
        /// 
        /// This method is more efficient than attempting to filter on the list of all 
        /// contacts returned by GetAll(), as this will preform a server side search.
        /// 
        /// Contacts returned by this endpoint will not contain their subscription
        /// information or attributes.
        /// </remarks>
        /// <param name="accountId">The account to search under.</param>
        /// <param name="wildcard">The wild card pattern to apply.</param>
        /// <returns>A list of contacts</returns>
        public IQueryable<Contact> ContactSearch(int accountId, string wildcard)
        {
            var request = new RestRequest(BaseUri + CONTACT_SEARCH_URI, Method.POST)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new NewtonsoftSerializer()
            };

            request.AddUrlSegment("accountId", accountId.ToString());
            request.AddBody(wildcard);

            var res = Execute<List<Contact>>(request) ?? new List<Contact>();

            // TODO: Map a flattened contact to an unflattened one.

            return res.AsQueryable();
        }

        #endregion Utility Functions
    }
}
