using System;
using System.Collections.Generic;
using System.Web.Http;

using ThreeSeventy.Vector.Client.Models;

namespace CallbackReceiver.Controllers
{
    /// <summary>
    /// This controller demonstrates how to handle a keyword callback from Vector.
    /// </summary>
    public class KeywordCallbackController : ApiController
    {
        // POST api/KeywordCallback
        /// <summary>
        /// Handles a keyword callback from Vector.
        /// </summary>
        /// <remarks>
        /// Currently all callbacks made by the Vector system are POST.
        /// </remarks>
        /// <param name="eventData">Details about the callback that were made.</param>
        public string Post(KeywordCallbackEvent eventData)
        {
            if (eventData != null)
            {
                string msg = String.Format(
                    "{0}: Received callback on keyword {1} from {2}",
                    eventData.Timestamp,
                    eventData.Keyword,
                    eventData.PhoneNumber);

                return msg;
            }

            return "Invalid event data!";
        }

    }
}
