using System;
using System.Collections.Generic;
using System.Net;

using RestSharp;

namespace ThreeSeventy.Vector.Client.Tests
{
    public class MockResponse<TEntity> : IRestResponse<TEntity>
    {
        #region Implementation of IRestResponse

        /// <summary>
        /// The RestRequest that was made to get this RestResponse
        /// </summary>
        /// <remarks>
        /// Mainly for debugging if ResponseStatus is not OK
        /// </remarks>
        public IRestRequest Request { get; set; }

        /// <summary>
        /// MIME content type of response
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Length in bytes of the response content
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Encoding of the response content
        /// </summary>
        public string ContentEncoding { get; set; }

        /// <summary>
        /// String representation of response content
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// HTTP response status code
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Description of HTTP status returned
        /// </summary>
        public string StatusDescription { get; set; }

        /// <summary>
        /// Response content
        /// </summary>
        public byte[] RawBytes { get; set; }

        /// <summary>
        /// The URL that actually responded to the content (different from request if redirected)
        /// </summary>
        public Uri ResponseUri { get; set; }

        /// <summary>
        /// HttpWebResponse.Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Cookies returned by server with the response
        /// </summary>
        public IList<RestResponseCookie> Cookies { get; private set; }

        /// <summary>
        /// Headers returned by server with the response
        /// </summary>
        public IList<Parameter> Headers { get; private set; }

        /// <summary>
        /// Status of the request. Will return Error for transport errors.
        ///             HTTP errors will still return ResponseStatus.Completed, check StatusCode instead
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// Transport or other non-HTTP error generated while attempting request
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Exceptions thrown during the request, if any.  
        /// </summary>
        /// <remarks>
        /// Will contain only network transport or framework exceptions thrown during the request.  HTTP protocol errors are handled by RestSharp and will not appear here.
        /// </remarks>
        public Exception ErrorException { get; set; }

        #endregion

        #region Implementation of IRestResponse<TEntity>

        /// <summary>
        /// Deserialized entity data
        /// </summary>
        public TEntity Data { get; set; }

        #endregion
    };

}
