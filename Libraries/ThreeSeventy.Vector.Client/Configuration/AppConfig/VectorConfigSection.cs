using System;
using System.Collections.Generic;
using System.Configuration;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// App/Web.config mapping
    /// </summary>
    public class VectorConfigSection : ConfigurationSection, IConfiguration
    {
        /// <summary>
        /// Pulls config from the configuration file.
        /// </summary>
        public static VectorConfigSection GetConfig()
        {
            var rval = (VectorConfigSection)ConfigurationManager.GetSection("vectorClient");

            return rval ?? new VectorConfigSection();
        }

        [ConfigurationProperty("auth", IsRequired = true)]
        internal VectorAuthElement InnerAuthorization
        {
            get { return (VectorAuthElement)this["auth"]; }
            set { this["auth"] = value; }
        }

        /// <summary>
        /// Details on how we should connect to the remote service.
        /// </summary>
        public IAuthConfig Authorization
        {
            get { return InnerAuthorization; }
            set { InnerAuthorization = (VectorAuthElement)value; }
        }

        /// <summary>
        /// The base URL to use.
        /// </summary>
        [ConfigurationProperty("baseUrl", DefaultValue = "https://api.3seventy.com/api/v2.0", IsRequired = false)]
        public string BaseUrl
        {
            get { return (string)this["baseUrl"]; }
            set { this["baseUrl"] = value; }
        }

        /// <summary>
        /// The user agent string to use when sending requests.
        /// </summary>
        [ConfigurationProperty("userAgent", IsRequired = false)]
        public string UserAgent
        {
            get { return (string)this["userAgent"]; }
            set { this["userAgent"] = value; }
        }

        /// <summary>
        /// Timespan to wait before we give up on a request.
        /// </summary>
        /// <remarks>
        /// Default is 2 minutes.
        /// </remarks>
        [ConfigurationProperty("timeout", IsRequired = false, DefaultValue = "0:2:0")]
        public TimeSpan Timeout
        {
            get { return (TimeSpan)this["timeout"]; }
            set { this["timeone"] = value; }
        }

        /// <summary>
        /// Determins how we should attempt to retry errors.
        /// </summary>
        [ConfigurationProperty("retryPolicy")]
        internal RetryStrategyElement InnerRetryPolicy
        {
            get { return (RetryStrategyElement)this["retryPolicy"]; }
            set { this["retryPolicy"] = value; }
        }

        /// <summary />
        public IRetryConfig RetryPolicy
        {
            get { return InnerRetryPolicy; }
            set { InnerRetryPolicy = (RetryStrategyElement)value; }
        }
    }
}
