using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// Enumeration for identifying the type of campaign we are working with.
    /// </summary>
    public enum CampaignType
    {
        /// <summary>
        /// A campaign with no content for pushing out unformatted messages.
        /// </summary>
        /// <remarks>
        /// If pushed, then the EventPushCampaign must contain a message in order to be successful.
        /// </remarks>
        /// <seealso cref="EventPushCampaign" />
        Gateway = 0,

        /// <summary>
        /// A campaign with preformatted content.
        /// </summary>
        Basic = 1,

        /// <summary>
        /// A campaign which contains a group of question campaigns.
        /// </summary>
        /// <remarks>
        /// Dialog campaigns do not have content in of themselves.  Their content comes from the linked question campaigns.
        /// 
        /// TODO: Not supported by this SDK yet.
        /// </remarks>
        /// <see cref="Question" />
        /// <see cref="Campaign" />
        Dialog = 2,

        /// <summary>
        /// A campaign that asks a single question in a survey.
        /// </summary>
        /// <remarks>
        /// Question campaigns hold the actual content for dialog campaigns.
        /// 
        /// These are chained together to for a series of questions that will get sent to the contact.
        /// 
        /// TODO: Not supported by this SDK yet.
        /// </remarks>
        Question = 3,

        /// <summary>
        /// A campaign which sends a coupon code to a customer.
        /// </summary>
        Coupon = 4,

        /// <summary>
        /// Reserved, do not use.
        /// </summary>
        Sweapstakes = 5,
    }
}