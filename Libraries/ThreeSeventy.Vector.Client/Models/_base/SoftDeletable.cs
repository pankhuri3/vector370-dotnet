using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Base type for objects that are not removed when delete is called, but rather are placed in a deleted state.
    /// </summary>
    /// <remarks>
    /// The statusId field is not directly settable by us.  It is instead changed when an HTTP <tt>DELETE</tt> call is
    /// made on this object type.
    /// </remarks>
    [Serializable]
    [DataContract]
    public abstract class SoftDeletable : BaseAudited
    {
        /// <summary>
        /// Gets the current status of the object.
        /// </summary>
        /// <remarks>
        /// The object's status is not directly settable. Instead it is changed when an HTTP <tt>DELETE</tt> method is made.
        /// </remarks>
        /// <seealso cref="Status" />
        [DataMember]
        public int StatusId { get; internal set; }
        
        /// <summary>
        /// Gets the current status of the object.
        /// </summary>
        /// <remarks>
        /// The object's status is not directly settable. Instead it is changed when an HTTP DELETE call is made.
        /// 
        /// This is an enumeration wrapper for the StatusId
        /// </remarks>
        /// <seealso cref="StatusId" />
        [IgnoreDataMember]
        public ResourceStatus Status
        {
            get { return (ResourceStatus)StatusId; }
        }
    }
}
