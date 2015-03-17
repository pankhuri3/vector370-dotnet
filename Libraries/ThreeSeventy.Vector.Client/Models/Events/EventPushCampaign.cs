using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Used for sending a campaign to a list of contacts.
    /// </summary>
    /// <remarks>
    /// Note that the list of contacts can be specified in a variety of ways:
    /// * Targets: This is a list of phone numbers and/or email addresses.
    /// * Contacts: This is a list of contact ids.
    /// * ContactListId: This is the ID of a contact list which runs on the 3Seventy servers.
    /// 
    /// All three types can be specified and the lists will be merged with duplicate contacts removed.
    /// </remarks>
    [Serializable]
    [DataContract]
    public class EventPushCampaign : BaseAudited
    {
        #region Private Members

        /// <summary>
        /// So we won't return a null value for the list object.
        /// </summary>
        private IList<int> m_channels = new List<int>();

        /// <summary>
        /// So we won't return a null value for the list object.
        /// </summary>
        private IList<string> m_targets = new List<string>();

        /// <summary>
        /// So we won't return a null value for the list object.
        /// </summary>
        private IList<int> m_contacts = new List<int>();

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public EventPushCampaign()
        {
            ScheduleType = ScheduleType.Now;
        }

        /// <summary>
        /// The AccountID which this event was run as.
        /// </summary>
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// The name of the event.
        /// </summary>
        [DataMember]
        [Obsolete("This data member is no longer needed.")]
        public string Name { get; set; }

        /// <summary>
        /// HACK: This is for some backwards compatibility. DO NOT USE!
        /// </summary>
        /// <remarks>
        /// Get is deliberately missing so that Newtonsoft will not attempt to serialize it out.
        /// (We only want to serialize it in, and then map it to ChannelIds  (That's ChannelId**s** with an 's'))
        /// </remarks>
        [DataMember]
        [Obsolete("Use ChannelIds")]
        internal IList<int> ChannelId
        {
            set { ChannelIds = value; }
        }

        /// <summary>
        /// The channels the campaign will be (or was) sent to.
        /// </summary>
        [DataMember]
        public IList<int> ChannelIds
        {
            get { return m_channels; }
            set { m_channels = value ?? new List<int>(); }
        }

        /// <summary>
        /// A list of targets this event will be (or was) sent to.
        /// </summary>
        /// <remarks>
        /// Targets can be a mixture of email addresses, phone numbers, and {@link Contact} ids.
        /// 
        /// Note that in order for the system to differentiate a phone number from a contact ID, phone numbers must be
        /// prefixed with their country dialing code. E.g.: {@code (221) 555-0100} should be listed as {@code +12215550100}
        /// </remarks>
        [DataMember]
        public IList<string> Targets
        {
            get { return m_targets; }
            set { m_targets = value ?? new List<string>(); }
        }

        /// <summary>
        /// A list of contact ids this event will be (or was) sent to.
        /// </summary>
        [DataMember]
        public IList<int> Contacts
        {
            get { return m_contacts; }
            set { m_contacts = value ?? new List<int>(); }
        }

        /// <summary>
        /// The contact list to use for getting a list of contacts.
        /// </summary>
        /// <remarks>
        /// Generation of contact lists are not yet supported by this SDK, but they can be created via 
        /// raw API calls and using our Portal.  If you do create a contact list in this way, you can
        /// supply the generated ID here without any issues.
        /// </remarks>
        public int? ContactListId { get; set; }

        /// <summary>
        /// The campaign to send (or was sent)
        /// </summary>
        [DataMember]
        public int CampaignId { get; set; }
        
        /// <summary>
        /// The "from" line to use for sending to an email channel.
        /// </summary>
        /// <remarks>
        /// If it is not filled in, a default from field will be used.
        /// </remarks>
        /// <seealso cref="Campaign" />
        /// <seealso cref="Content" />
        /// <seealso cref="ContentTemplate" />
        /// <seealso cref="CampaignType" />
        /// <seealso cref="EncodingType" />
        [DataMember]
        public string From { get; set; }

        /// <summary>
        /// Ignore a campaign's SingleUse flag.
        /// </summary>
        /// <remarks>
        /// If a campaign is marked as single use, then it will only get sent to a particular 
        /// contact once and only once.  Setting this value will force the message to get
        /// sent regardless of the SingleUse flag setting on the campaign.
        /// 
        /// This can be handy if you have a particular contact who did not receive the message
        /// and you would like to resend it to them.
        /// </remarks>
        /// <seealso cref="Campaign.SingleUse" />
        public bool IgnoreSingleUse { get; set; }

        /// <summary>
        /// Forces the contacts to be opted into the subscription defined on the campaign.
        /// </summary>
        public bool ForceOptIn { get; set; }

        #region Gateway Campaign Parameters

        // These fields are only needed for gateway campaigns.  For all other campaign types they will be ignored.

        /// <summary>
        /// The message text for use on gateway campaigns.
        /// </summary>
        /// <remarks>
        /// IMPORTANT: You cannot send Razor formatted text with this system.
        /// </remarks>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// The "Subject" line to use when sending to an email channel.
        /// </summary>
        /// <remarks>
        /// This field is ignored when being sent to all other types of channels.
        /// </remarks>
        [DataMember]
        public string Subject { get; set; }

        #endregion

        #region Scheduling

        // NOTE: Not all scheduling options are supported at this time..

        /// <summary>
        /// Schedule Type Id: Now, OneTime, second, minute, hour, daily, weekly, monthly, yearly.
        /// </summary>
        /// <remarks>
        /// Currently the only supported types are: Now, OneTime and Daily.
        /// </remarks>
        [DataMember]
        public int ScheduleTypeId { get; set; }

        /// <summary>
        /// how often the event should recur.
        /// </summary>
        /// <remarks>
        /// The meaning of this value is dependent on the ScheduleType.
        ///
        /// * Seconds: Valid values (0-60)
        /// * Minutes: Valid values (0-60)
        /// * Hours: Valid values (0-12)
        /// * Daily: Interpreted as a bit mask day of the week:
        ///   * 0x00 = Alias for Every Day
        ///   * 0x01 = Monday
        ///   * 0x02 = Tuesday
        ///   * 0x04 = Wednesday
        ///   * 0x08 = Thursday
        ///   * 0x10 = Friday
        ///   * 0x20 = Saturday
        ///   * 0x40 = Sunday
        ///   * 0x7F = Every day
        /// </remarks>
        [DataMember]
        public int Interval { get; set; }

        /// <summary>
        /// Starting time for the recurring schedule.
        /// </summary>
        /// <remarks>
        /// This is ignored for events scheduled to process now.
        /// </remarks>
        /// <seealso cref="ScheduleType" />
        [DataMember]
        public DateTime? StartDateTime { get; set; }

        /// <summary>
        /// End time for the recurring schedule.
        /// </summary>
        /// <remarks>
        /// This is ignored for events scheduled to process now.
        /// </remarks>
        /// <seealso cref="ScheduleType" />
        [DataMember]
        public DateTime? EndDateTime { get; set; }
        
        /// <summary>
        /// Day of the month.
        /// </summary>
        /// <remarks>
        /// Valid values are from 1 to 31, with * indicating every day.
        /// </remarks>
        [DataMember]
        public string DayOfMonth { get; set; }

        /// <summary>
        /// Month of the year.
        /// </summary>
        /// <remarks>
        /// Valid values are from 1 to 12, with * indicating every month.
        /// </remarks>
        [DataMember]
        public string Month { get; set; }

        /// <summary>
        /// Year. 4 digit or comma delimited for multiple years.
        /// </summary>
        /// <example>
        /// "2013" or "2013,2014,2016"
        /// </example>
        [DataMember]
        public string Year { get; set; }

        #endregion

        /// <summary>
        /// Enumeration mapping for ScheduleTypeId
        /// </summary>
        [IgnoreDataMember]
        public ScheduleType ScheduleType
        {
            get { return (ScheduleType)ScheduleTypeId; }
            set { ScheduleTypeId = (int)value; }
        }
    }
}
