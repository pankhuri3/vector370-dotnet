using System;
using System.Collections.Generic;
using System.Linq;

using Common.Logging;

using RestSharp;
using RestSharp.Contrib;

using ThreeSeventy.Vector.Client.Configuration;

namespace ThreeSeventy.Vector.Client.Utils
{
    internal class RequestBuilder
    {
        private readonly ILog m_log = LogManager.GetLogger(typeof(RequestBuilder));

        private readonly RestEntityConfigurationBase m_config;

        public RequestBuilder(RestEntityConfigurationBase configuration)
        {
            m_config = configuration;
        }

        private void MapProperites(
            MappingType type,
            Func<string, bool> filter,
            IDictionary<string, string> args,
            Action<string, string> a)
        {
            var parameters = m_config.GetProperties().Where(p => p.UrlMapType == type);

            foreach (var p in parameters)
            {
                string valueStr = String.Empty;

                if (args.ContainsKey(p.Name))
                    valueStr = args[p.Name];

                if ((filter != null) && filter(valueStr))
                    continue;

                a(p.MappingName, valueStr);
            }
        }

        private void SubUriSegments(IDictionary<string, string> args, ref string urlString)
        {
            var urlParams = m_config.GetProperties()
                .Where(p => p.UrlMapType == MappingType.UrlSegment || p.UrlMapType == MappingType.OptionalUrlSegment);

            foreach (var param in urlParams)
            {
                string valueStr;

                if (!args.TryGetValue(param.Name, out valueStr))
                {
                    if (param.UrlMapType == MappingType.UrlSegment)
                    {
                        string msg = String.Format("The {0} URL segment is required", param.MappingName);

                        throw new ArgumentNullException(param.Name, msg);
                    }
                }

                valueStr = HttpUtility.UrlEncode(valueStr);

                urlString = urlString.Replace('{' + param.MappingName + '}', valueStr);
            }
        }

        private void MakeAbsoluteUri(string baseUri, ref string urlString)
        {
            if (m_config.UriKind == UriKind.Relative)
            {
                if (urlString[0] == '/')
                    urlString = urlString.Substring(1);

                if (baseUri.Last() == '/')
                    baseUri = baseUri.Substring(0, baseUri.Length - 1);

                urlString = String.Format("{0}/{1}", baseUri, urlString);
            }
        }

        private string BuildGetParameters(IDictionary<string, string> args, Method method)
        {
            var result = new List<string>();

            MapProperites(
                MappingType.Get,
                String.IsNullOrEmpty,
                args,
                (name, value) =>
                {
                    value = HttpUtility.UrlEncode(value);
                    result.Add(String.Format("{0}={1}", name, value));
                });

            if ((method != Method.POST) || (method != Method.PUT) || (method != Method.PATCH))
            {
                MapProperites(
                    MappingType.GetOrPost,
                    String.IsNullOrEmpty,
                    args,
                    (name, value) =>
                    {
                        value = HttpUtility.UrlEncode(value);
                        result.Add(String.Format("{0}={1}", name, value));
                    });
            }

            return String.Join("&", result);
        }

        private string BuildUri(string baseUri, Method method, IDictionary<string, string> args)
        {
            string urlString = m_config.UriFormatStr;

            SubUriSegments(args, ref urlString);

            MakeAbsoluteUri(baseUri, ref urlString);

            string getParams = BuildGetParameters(args, method);

            if (!String.IsNullOrWhiteSpace(getParams))
                urlString += "?" + getParams;

            return urlString;
        }

        private void AddHeaderValues(IRestRequest request, IDictionary<string, string> args)
        {
            MapProperites(
                MappingType.Header,
                String.IsNullOrWhiteSpace,
                args,
                (name, value) => request.AddHeader(name, value));
        }

        private void AddCookieValues(IRestRequest request, IDictionary<string, string> args)
        {
            MapProperites(
                MappingType.Cookie,
                String.IsNullOrWhiteSpace,
                args,
                (name, value) => request.AddCookie(name, value));
        }

        public IRestRequest BuildRequest(string baseUri, Method method, object requestBody, object args, object extraArgs)
        {
            var arguments = ObjectHelper.AsArguments(args, extraArgs);

            string uri = BuildUri(baseUri, method, arguments);

            m_log.Debug(m => m("{0}: {1}", method, uri));

            var rval = new RestRequest
            {
                Resource = uri,
                Method = method,
                RequestFormat = DataFormat.Json,
                JsonSerializer = new NewtonsoftSerializer()
            };

            if (requestBody != null)
                rval.AddBody(requestBody);

            AddHeaderValues(rval, arguments);
            AddCookieValues(rval, arguments);
            
            return rval;
        }
    }
}
