using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    // TODO: Add contact attribute definition support.

    /// <summary>
    /// User defined attribute data.
    /// </summary>
    /// <remarks>
    /// Contact attributes provide a way to save data to contacts via the API or a Dialog.
    /// 
    /// Attributes must be defined before they can be used.  The SDK does not currently support defining these
    /// attributes directly.  However you can do so using either the Vector Portal or making direct API calls.
    /// This only needs to be done once per attribute definition.
    /// 
    /// The name of the attribute must be unique, and must follow standard programming naming
    /// conventions (must start with an underscore or letter, followed by letters, numbers, and underscores).
    /// 
    /// Child accounts inherit those attributes defined by their parent.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class ContactAttribute
    {
        /// <summary>
        /// The name of the attribute that is set.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The value set for this attribute.
        /// </summary>
        [DataMember]
        public string Value { get; set; }
    }
}
