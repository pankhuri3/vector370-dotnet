using System;
using System.Collections.Generic;
using System.Reflection;

using RestSharp;
using RestSharp.Deserializers;

using ThreeSeventy.Vector.Client.Utils;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Default implementation of IRestClientFactory.
    /// </summary>
    public class DefaultRestClientFactory : IRestClientFactory
    {
        private readonly string m_version;

        /// <summary />
        public DefaultRestClientFactory()
        {
            Assembly thisAssembly = Assembly.GetCallingAssembly();
            AssemblyName asmName = thisAssembly.GetName();
            m_version = asmName.Version.ToString();
        }

        /// <summary>
        /// Simply returns a new RestClient() object.
        /// </summary>
        public IRestClient Create(IConfiguration config)
        {
            string userName = config.Authorization.UserName;
            string password = config.Authorization.Password;

            string userAgent = config.UserAgent;

            if (String.IsNullOrWhiteSpace(userAgent))
                userAgent = String.Format("3Seventy SDK.NET {0}", m_version);

            // TODO: As of RestSharp 105.x you have to supply a base URL here!

            var rval = new RestClient
            {
                Authenticator = new HttpBasicAuthenticator(userName, password),
                UserAgent = userAgent,
                Timeout = (int)config.Timeout.TotalMilliseconds
            };

            rval.ClearHandlers();
            rval.AddHandler("application/json", new NewtonsoftSerializer());
            rval.AddHandler("text/json", new NewtonsoftSerializer());

            rval.AddHandler("application/xml", new XmlDeserializer());
            rval.AddHandler("text/xml", new XmlDeserializer());

            return rval;
        }
    }
}
