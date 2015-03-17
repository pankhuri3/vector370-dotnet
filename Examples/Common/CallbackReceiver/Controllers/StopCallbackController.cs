using System;
using System.Collections.Generic;
using System.Web.Http;

using ThreeSeventy.Vector.Client.Models;

namespace CallbackReceiver.Controllers
{
    /// <summary>
    /// This controller demonstrates how to handle a stop callback from Vector.
    /// </summary>
    public class StopCallbackController : ApiController
    {
        // POST api/DialogCallback
        /// <summary>
        /// Handles a stop callback from Vector.
        /// </summary>
        /// <remarks>
        /// Currently all callbacks made by the Vector system are POST.
        /// </remarks>
        /// <param name="eventData">Details about the callback that were made.</param>
        public string Post(StopCallbackEvent eventData)
        {
            if (eventData != null)
            {
                string msg = String.Format(
                    "Received stop callback from contact {0} ({1}) on channel {2}, opted out of subscription {3}",
                    eventData.ContactId,
                    eventData.PhoneNumber,
                    eventData.ChannelId,
                    eventData.SubscriptionId);

                return msg;
            }

            return "Invalid event data!";
        }
    }
}
