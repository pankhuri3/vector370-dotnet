using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

using Common.Logging;

using RestSharp;
using RestSharp.Deserializers;

using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client.Utils
{
    static class RequestHelper
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(RequestHelper));

        /// <summary>
        /// Utility method for finding out if a status code should be treated as a success or an error.
        /// </summary>
        /// <remarks>
        /// Note that this function would also treat 300-399 codes as an error.  Because RestSharp handles
        /// those automatically that should not be a problem.
        /// </remarks>
        /// <param name="code">The HttpStatusCode value to check.</param>
        /// <returns>True if the value is a successful call, false if it is an error.</returns>
        public static bool IsSuccessCode(this HttpStatusCode code)
        {
            var val = (int)code;
            return ((val >= 200) && (val <= 299));
        }

        /// <summary>
        /// Deserializes an entity from the response stream.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        public static TEntity Deserialize<TEntity>(this IRestResponse response)
            where TEntity : class, new()
        {
            if (String.IsNullOrWhiteSpace(response.Content))
                return default(TEntity);

            IDeserializer des;

            string contentType = response.ContentType.ToLower();
            contentType = contentType.Split(new[] { ';' }).FirstOrDefault().Trim();
            
            switch (contentType)
            {
            case "application/json":
            case "text/json":
                des = new NewtonsoftSerializer();
                break;

            case "application/xml":
            case "text/xml":
                des = new XmlDeserializer();
                break;

            default:
                s_log.WarnFormat(
                    "Unknown content type {0} for {1} deserialization",
                    contentType,
                    typeof(TEntity).FullName);

                return default(TEntity);
            }

            return des.Deserialize<TEntity>(response);
        }

        /// <summary>
        /// Attempts to deserialize a list of errors when we received an error response from the HTTP server.
        /// </summary>
        /// <remarks>
        /// Unlike the TryForSimpleErrors this method attempts to pull out more specific details.
        /// </remarks>
        /// <param name="response">The response to parse</param>
        private static void TryForDetailedErrors(IRestResponse response)
        {
            List<ErrorDetail> errors = null;

            try
            {
                errors = response.Deserialize<List<ErrorDetail>>();
            }
            catch (Exception ex)
            {
                // We deliberately throw away the exception

                s_log.Debug("Unable to deserialize error message details", ex);
            }

            if (errors != null)
                throw new RemoteException(response.StatusCode, errors);
        }

        /// <summary>
        /// Attempts to deserialize a list of errors when we received an error response from the HTTP server.
        /// </summary>
        /// <param name="response">The response to parse</param>
        private static void TryForSimpleErrors(IRestResponse response)
        {
            List<string> errorList = null;
            
            try
            {
                errorList = response.Deserialize<List<string>>();
            }
            catch (Exception ex)
            {
                // We deliberately throw away the exception

                s_log.Debug("Unable to deserialize error message details", ex);
            }

            if (errorList != null)
                throw new ValidationException(errorList.FirstOrDefault());
        }

        /// <summary>
        /// Wrapper for calling a request and parsing the response if there is an error.
        /// </summary>
        /// <param name="client">The client to use for making the call.</param>
        /// <param name="retryPolicy">The retry policy to use for this request.</param>
        /// <param name="request">The details of the request to make.</param>
        /// <param name="nullOn404">Set if we should return NULL on a 404, false will cause an exception to get thrown.</param>
        /// <returns>The IRestResponse or NULL if the entity was not found.</returns>
        public static IRestResponse ExecuteRequestFor(
            this IRestClient client, 
            IRetryPolicy retryPolicy,
            IRestRequest request, 
            bool nullOn404)
        {
            var resource = client.BuildUri(request);

            IRestResponse response = 
                retryPolicy.Execute(
                attempt =>
                {
                    s_log.Trace(m => m("REQUESTING {0}: {1}  (ATTEMPT: {2})", request.Method, resource, attempt));

                    var res = client.Execute(request);

                    s_log.Debug(m => m("{0} {1} - {2}", request.Method, res.StatusCode, resource));

                    if (!res.StatusCode.IsSuccessCode())
                        return HandleErrorResponse(request, res, resource, nullOn404);

                    return res;
                });

            return response;
        }

        /// <summary>
        /// Handles error response conditions.
        /// </summary>
        /// <param name="request">The request that was made</param>
        /// <param name="response">The response we got back</param>
        /// <param name="resource">The URI that we attmped.</param>
        /// <param name="nullOn404">Set if we should just return NULL on 404 errors</param>
        private static IRestResponse HandleErrorResponse(
            IRestRequest request,
            IRestResponse response,
            Uri resource,
            bool nullOn404)
        {
            if (nullOn404 && (response.StatusCode == HttpStatusCode.NotFound))
                return null;

            if (response.StatusCode == 0)
            {
                /*
                 * There was some sort of error communicationg with the server, and we did not get a response.
                 * 
                 * E.g.: Connection timed out, Connectin Refused, No route, etc.
                 */
                
                s_log.WarnFormat("Unable to perform request: {0}", response.ErrorMessage);

                throw new NetworkException(response.ErrorMessage, response.ErrorException);
            }

            string[] contentTypeParts = response.ContentType.Split(';');
            string contentType = contentTypeParts[0];

            switch (contentType)
            {
            case "application/xml":
            case "text/xml":
            case "application/json":
            case "text/json":
                TryForDetailedErrors(response);
                TryForSimpleErrors(response);
                break;

            //case "text/plain":
            //case "text/html":
            //default:
            //    break;
            }

            var sb = new StringBuilder();

            sb.AppendFormat(
                "Unable to perform {0} on {1}, additionally the error message could not be parsed",
                request.Method,
                resource);

            if (!String.IsNullOrWhiteSpace(response.Content))
            {
                sb.Append(": ");
                sb.Append(response.Content);
            }

            throw new HttpException((int)response.StatusCode, sb.ToString());
        }
    }
}
