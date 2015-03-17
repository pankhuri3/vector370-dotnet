using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Details for a contact's subscription options.
    /// </summary>
    /// <remarks>
    /// IMPORTANT NOTE: Setting the correct subscription type is important for following correct CTIA compliance
    /// guidelines.
    /// </remarks>
    /// <seealso cref="Contact"/>
    /// <seealso cref="Subscription" />
    /// <seealso cref="SubscriptionType" />
    [Serializable]
    [DataContract]
    public class ContactSubscription
    {
        /// <summary>
        /// The subscription ID the contact is a member of.
        /// </summary>
        [DataMember]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Set if the contact wishes to receive SMS messages.
        /// </summary>
        /// <remarks>
        /// Note that according to current CTIA requirements we MUST send a handset verification when a contact is 
        /// manually opted in to a recurring subscription.
        /// </remarks>
        [DataMember]
        public bool SmsEnabled { get; set; }

        /// <summary>
        /// Set if the contact wishes to receive MMS messages.
        /// </summary>
        /// <remarks>
        /// Not currently used.
        /// 
        /// Note that according to current CTIA requirements we MUST send a handset verification when a contact is 
        /// manually opted in.
        /// </remarks>
        [DataMember]
        [Obsolete("Not implemented, do not use")]
        public bool MmsEnabled { get; set; }

        /// <summary>
        /// Set if the contact wishes to receive email messages.
        /// </summary>
        [DataMember]
        public bool EmailEnabled { get; set; }

        /// <summary>
        /// Set if the contact wishes to receive voice messages.
        /// </summary>
        /// <remarks>
        /// Not currently used.
        /// </remarks>
        [DataMember]
        [Obsolete("Not implemented, do not use")]
        public bool VoiceEnabled { get; set; }
    }
}
