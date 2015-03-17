using System;
using System.Collections.Generic;

using RestSharp;

using ThreeSeventy.Vector.Client.Configuration;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Base for a REST context.
    /// </summary>
    /// <remarks>
    /// Supports configuring a set of models to a set of URLs and then getting 
    /// a RestRepository for those models when needed.
    /// </remarks>
    public abstract class RestContext : IContext
    {
        private readonly IRestClientFactory m_clientFactory;

        private readonly IRetryPolicy m_retryPolicy;
        
        private RestModelBuilder m_modelBuilder;

        /// <summary />
        /// <remarks>
        /// Note that the base URI can be overridden in the specific entity's by specifying an Absolute URI kind.
        /// </remarks>
        /// <param name="config">Configuration details</param>
        /// <param name="clientFactory">The factory object for creating new instances of IRestClient</param>
        protected RestContext(IConfiguration config, IRestClientFactory clientFactory)
        {
            Configuration = config;

            m_clientFactory = clientFactory ?? new DefaultRestClientFactory();
            BaseUri = Configuration.BaseUrl;

            var strategy = GetRetryStrategy();
            m_retryPolicy = new RetryPoliciy<RestErrorDetectionStrategy>(strategy);
        }

        /// <summary>
        /// Builds a RetryStrategy object based on the config.
        /// </summary>
        /// <returns></returns>
        private RetryStrategy GetRetryStrategy()
        {
            var rval = (RetryStrategy)Activator.CreateInstance(Configuration.RetryPolicy.Type);

            rval.MaxTries = (byte)Configuration.RetryPolicy.MaxTries;
            rval.MaxInterval = Configuration.RetryPolicy.MaxInterval;
            rval.MinInterval = Configuration.RetryPolicy.MinInterval;
            rval.Interval = Configuration.RetryPolicy.Interval;

            return rval;
        }
        
        /// <summary>
        /// Gets or sets the base URI for calls to the REST endpoint.
        /// </summary>
        /// <remarks>
        /// Models may individual override this setting by specifying a UrlKind of Absolute.
        /// 
        /// The base URI parameter will be removed at some later date when we start tying the
        /// SDKs to a specific API version.
        /// </remarks>
        public string BaseUri { get; private set; }
        
        internal IRestClient GetClient()
        {
            IRestClient rval = m_clientFactory.Create(Configuration);

            return rval;
        }

        /// <summary>
        /// Gets the current retry policy.
        /// </summary>
        /// <returns></returns>
        protected IRetryPolicy GetRetryPolicy()
        {
            return m_retryPolicy;
        }

        /// <summary>
        /// Gets the configuration for this context.
        /// </summary>
        protected IConfiguration Configuration { get; private set; }

        private void CreateModels()
        {
            if (m_modelBuilder != null)
                return;

            m_modelBuilder = new RestModelBuilder();
            OnModelCreating(m_modelBuilder);

            // Make sure the registered models are valid.
            m_modelBuilder.CheckEntries();
        }

        /// <summary>
        /// Abstract method for configuring the model via a fluent interface.
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected abstract void OnModelCreating(RestModelBuilder modelBuilder);

        /// <summary>
        /// Gets a RestRepository for the specified entity.
        /// </summary>
        /// <remarks>
        /// If the entity has a set of required arguments, then they need to be supplied with this
        /// function here.
        /// 
        /// E.g.: For the content:
        /// var contentRepo = context.Repository&lt;Content&gt;(new { AccountId = 5 });
        /// 
        /// Note that any required parameters which are mapped to a different HTTP name, will use
        /// the name in the model, and not the mapped HTTP name.
        /// 
        /// E.g. the following will result in an exception:
        /// var contentRepo = context.Repository&lt;Content&gt;(new { accountId = 5 });
        /// </remarks>
        /// <typeparam name="TEntity">The type of entity to get the repository of.</typeparam>
        /// <param name="args">A list of required arguments for this model.</param>
        public IRepository<TEntity> Repository<TEntity>(object args = null)
            where TEntity : class, new()
        {
            CreateModels();

            RestEntityConfiguration<TEntity> config;

            try
            {
                config = m_modelBuilder.GetConfig<TEntity>();
            }
            catch (Exception ex)
            {
                string msg = String.Format("Entity {0} not configured.", typeof(TEntity).FullName);
                throw new Exception(msg, ex);
            }

            args = args ?? new object();

            config.ValidateHasNeededKeys(args);

            return new RestRepository<TEntity>(
                GetRetryPolicy(),
                GetClient(),
                BaseUri,
                args,
                config);
        }
    }
}
