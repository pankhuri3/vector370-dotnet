using System;
using System.Collections.Generic;
using System.Web.Http;

using ThreeSeventy.Vector.Client.Models;

namespace CallbackReceiver.Controllers
{
    /// <summary>
    /// This controller demonstrates how to handle a dialog callback from Vector.
    /// </summary>
    public class DialogCallbackController : ApiController
    {
        // POST api/DialogCallback
        /// <summary>
        /// Handles a dialog callback from Vector.
        /// </summary>
        /// <remarks>
        /// Currently all callbacks made by the Vector system are POST.
        /// </remarks>
        /// <param name="eventData">Details about the dialog callback</param>
        public string Post(DialogCallbackEvent eventData)
        {
            if (eventData != null)
            {
                string msg = String.Format(
                    "Received callback on dialog campaign {0} from {1}: {2}",
                    eventData.DialogCampaignId,
                    eventData.PhoneNumber,
                    eventData.Answer);

                return msg;
            }

            return "Invalid event data!";
        }
    }
}
