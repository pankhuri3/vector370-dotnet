using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Provides a container for templates.
    /// </summary>
    /// <remarks>
    /// Content objects group related templates.
    /// 
    /// IMPORTANT NOTE:
    /// 
    /// There is no requirement to have more than one template under a content object.  However you must have at least 
    /// one template and it must be part of a content group.
    /// 
    /// Additionally, you may only have one template per type per language. For example, you may have a template for
    /// SMS/ENGLISH and another template for SMS/FRENCH, but not two templates with SMS/ENGLISH.
    /// </remarks>
    /// <seealso cref="ChannelType" />
    /// <seealso cref="LanguageType" />
    [Serializable]
    [DataContract]
    public class Content : BaseAudited
    {
        /// <summary>
        /// The account which owns this content object.
        /// </summary>
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// The name of this content object.
        /// </summary>
        /// <remarks>
        /// This is a free form name you can use to provide a human readable identifier to each object
        /// </remarks>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// A free form description field.
        /// </summary>
        /// <remarks>
        /// The free form description field allows you to set a human readable detailed description of the object.
        /// </remarks>
        [DataMember]
        public string Description { get; set; }
    }
}
