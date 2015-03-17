using System;
using System.Collections.Generic;

using Common.Logging;

using Newtonsoft.Json;

using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace ThreeSeventy.Vector.Client.Utils
{
    internal class NewtonsoftSerializer : ISerializer, IDeserializer
    {
        private readonly ILog m_log = LogManager.GetLogger(typeof(NewtonsoftSerializer));

        public string ContentType 
        {
            get { return "application/json"; }
            set { }
        }

        public string DateFormat { get; set; }

        public string Namespace { get; set; }

        public string RootElement { get; set; }

        public string Serialize(object obj)
        {
            m_log.Trace(m => m("Serialize called for {0}", obj.GetType().FullName));

            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }

        public T Deserialize<T>(RestSharp.IRestResponse response)
        {
            m_log.Trace(m => m("Deserialize called for {0}", typeof(T).FullName));

            return JsonConvert.DeserializeObject<T>(response.Content);
        }
    }
}
