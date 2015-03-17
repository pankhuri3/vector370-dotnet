using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Contact details
    /// </summary>
    /// <remarks>
    /// A contact is a unique phone number and/or email address per account.
    /// 
    /// Note that either PhoneNumber or Email are required.  You may have both, but you must have at least one of these two.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class Contact : SoftDeletable
    {
        private IList<ContactAttribute> m_attributes = new List<ContactAttribute>();

        private IList<ContactSubscription> m_subscriptions = new List<ContactSubscription>();
        
        /// <summary>
        /// The account that this contact belongs to.
        /// </summary>
        /// <seealso cref="Account" />
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// Mobile number if available.
        /// </summary>
        /// <remarks>
        /// Either this or the Email field are _REQUIRED_.
        /// 
        /// Note that contact phone numbers must have the dialing prefix on them to be valid. Phone number validity is
        /// checked according to the supplied country code. The currently supported country codes are:
        /// * +1   -- United States and Canada
        /// * +44  -- United Kingdom
        /// * +52  -- Mexico
        /// * +86  -- China (main land)
        /// * +852 -- China (Hong Kong)
        /// * +853 -- China (Macau)
        /// * +886 -- China (Taiwan)
        ///  
        /// Valid Example: +12215550100
        /// 
        /// While the area code 221 (as of this writing) is not currently in use, it follows the NANP rules for a valid
        /// area code.
        ///
        /// Invalid Examples:
        /// * +11234560100 -- Area codes cannot start with 0 or 1
        /// * +12210550100 -- Exchange codes cannot start with 0 or 1
        /// * +15129110000 -- Exchange and area codes cannot have the form of X11
        /// * +18005550100 -- Toll free numbers are not allowed.
        /// * +19005550100 -- Toll numbers are not allowed.
        /// </remarks>
        /// <see cref="Email"/>
        [DataMember]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Email address of the contact.
        /// </summary>
        /// <remarks>
        /// Either this or the PhoneNumber field are _REQUIRED_.
        /// </remarks>
        /// <see cref="PhoneNumber"/>
        [DataMember]
        public string Email { get; set; }
        
        /// <summary>
        /// List of attributes set on this contact
        /// </summary>
        /// <remarks>
        /// You can use contact attributes to set pieces of tagged data on each contact. These data can then be used in
        /// contact lists to help filter your contacts to a select group on large pushes.
        ///
        /// Currently the SDK does not support defining these attributes, but you can use the Vector Portal or the API
        /// to do this. It is a one time setup, and once defined the attribute does not need to be defined again.
        /// </remarks>
        /// <seealso cref="ContactAttribute" />
        [DataMember]
        public IList<ContactAttribute> Attributes
        {
            get { return m_attributes; }
            set { m_attributes = value ?? new List<ContactAttribute>(); }
        }

        /// <summary>
        /// List of subscription details for this contact.
        /// </summary>
        /// <remarks>
        /// The contact may be opted out, but still listed here. There are various enabled fields which detail how the
        /// contact wishes to receive messages.
        /// </remarks>
        /// <seealso cref="ContactSubscription" />
        [DataMember]
        public IList<ContactSubscription> Subscriptions
        {
            get { return m_subscriptions; }
            set { m_subscriptions = value ?? new List<ContactSubscription>(); }
        }

    }
}
