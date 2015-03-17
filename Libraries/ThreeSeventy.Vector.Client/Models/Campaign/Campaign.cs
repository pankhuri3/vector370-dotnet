using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ThreeSeventy.Vector.Client.Models
{
    /// <summary>
    /// Details of a campaign that are sent to a contact.
    /// </summary>
    [Serializable]
    [DataContract]
    public class Campaign : SoftDeletable
    {
        /// <summary>
        /// The account ID to which the campaign belongs.
        /// </summary>
        [DataMember]
        public int AccountId { get; set; }

        /// <summary>
        /// The ID of the subscription that contacts who respond to this campaign are opted into.
        /// </summary>
        /// <seealso cref="Subscription" />
        [DataMember]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// The campaign's name.
        /// </summary>
        /// <remarks>
        /// This is a free form name.
        /// </remarks>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// The type of campaign.
        /// </summary>
        /// <seealso cref="CampaignType" />
        [DataMember]
        public int CampaignTypeId { get; set; }

        /// <summary>
        /// The ID of the content that this campaign sends.
        /// </summary>
        /// <remarks>
        /// This is not used for Gateway or Survey campaign types.
        /// </remarks>
        /// <seealso cref="Content" />
        [DataMember]
        public int? ContentId { get; set; }

        /// <summary>
        /// Indicates if this campaign will start a new session
        /// </summary>
        /// <remarks>
        /// If set then when the contact texts into an attached keyword or the campaign is pushed to a contact they are
        /// placed into a session. This is used by dialog campaigns to manage responses without colliding with reserved
        /// keywords.
        /// Currently this value cannot be customized.
        /// </remarks>
        [DataMember]
        public bool Session { get; internal set; }

        /// <summary>
        /// The duration of sessions in milliseconds from start.
        /// </summary>
        /// <remarks>
        /// Currently this value cannot be customized.
        /// </remarks>
        [DataMember]
        public int? SessionLength { get; internal set; }

        /// <summary>
        /// Arbitrary user data field.
        /// </summary>
        /// <remarks>
        /// This an area to store free form data.
        /// 
        /// The Vector Portal uses this field to store some UI hints.
        /// </remarks>
        [DataMember]
        public string UserData { get; set; }

        /// <summary>
        /// Indicates if the campaign is a one time use campaign.
        /// </summary>
        /// <remarks>
        /// Single use campaigns can only be sent to a contact once.  If the campaign is pushed to a contact more than
        /// once then nothing is sent to that contact.
        /// 
        /// If the contact texts into a keyword that is attached to a single use campaign then they are sent the contents of
        /// the SingleUseContentId value.
        /// </remarks>
        /// 
        /// <see cref="SingleUseContentId" />
        [DataMember]
        public bool SingleUse { get; set; }

        /// <summary>
        /// This is the ID of the content to send if a contact texts into a single use campaign more than once.
        /// </summary>
        /// <remarks>
        /// This field is only valid for campaigns that are marked as SingleUse
        /// 
        /// This content is only sent if a contact texts into a keyword attached to the single use campaign. If the campaign
        /// is pushed to the contact then nothing is sent to them.
        /// </remarks>
        /// <see cref="SingleUse" />
        [DataMember]
        public int? SingleUseContentId { get; set; }

        /// <summary>
        /// Enumeration mapping for CampaignTypeId
        /// </summary>
        [IgnoreDataMember]
        public CampaignType CampaignType
        {
            get { return (CampaignType)CampaignTypeId; }
            set { CampaignTypeId = (int)value; }
        }
    }
}
