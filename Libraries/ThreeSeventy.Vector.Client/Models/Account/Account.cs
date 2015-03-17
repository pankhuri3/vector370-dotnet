using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

using Newtonsoft.Json;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Details for an account.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Account : Base
    {
        /// <summary>
        /// The current status of the account.
        /// </summary>
        /// <remarks>
        /// This is the soft delete status.
        /// </remarks>
        [DataMember]
        [JsonProperty("status")] // TODO: Remove this in the future
        public int StatusId { get; internal set; }

        /// <summary>
        /// The parent account which owns this account.
        /// </summary>
        /// <remarks>
        /// This will be NULL for the root account.
        /// </remarks>
        [DataMember]
        public int? ParentId { get; set; }

        /// <summary>
        /// The name of the account.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// When the account was created.
        /// </summary>
        [DataMember]
        public DateTime Created { get; set; }

        /// <summary>
        /// Set if the account is allowed to make callbacks.
        /// </summary>
        [DataMember]
        public bool AllowCallback { get; internal set; }

        /// <summary>
        /// The token that is used when making callbacks.
        /// </summary>
        /// <remarks>
        /// When 3Seventy makes a callback this token will be sent along with that callback request.
        /// 
        /// This token can be whatever GUID of your choosing.
        /// </remarks>
        [DataMember]
        public Guid? CallbackToken { get; set; }
        
        /// <summary>
        /// Enumeration wrapper for StatusId
        /// </summary>
        [IgnoreDataMember]
        public ResourceStatus Status
        {
            get { return (ResourceStatus)StatusId; }
        }
    }
}
