using System;
using System.Collections.Generic;
using System.Web.Http;

using ThreeSeventy.Vector.Client.Models;

namespace CallbackReceiver.Controllers
{
    /// <summary>
    /// This controller demonstrates how to handle a new account callback from Vector.
    /// </summary>
    public class NewAccountCallbackController : ApiController
    {
        // POST api/DialogCallback
        /// <summary>
        /// Handles a new account callback from Vector.
        /// </summary>
        /// <remarks>
        /// Currently all callbacks made by the Vector system are POST.
        /// </remarks>
        /// <param name="eventData">Details about the callback that were made.</param>
        public string Post(NewAccountCallbackEvent eventData)
        {
            if (eventData != null)
            {
                string msg = String.Format(
                    "New account {0} created under {2}",
                    eventData.AccountId,
                    eventData.ParentId);

                return msg;
            }

            return "Invalid event data!";
        }
    }
}
