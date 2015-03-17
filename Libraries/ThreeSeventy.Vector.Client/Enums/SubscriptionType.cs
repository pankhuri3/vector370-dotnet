using ThreeSeventy.Vector.Client.Models;

namespace ThreeSeventy.Vector.Client
{
    /// <summary>
    /// The type of subscription, either one-time or recurring.
    /// </summary>
    /// <remarks>
    /// IMPORTANT NOTE: Setting the correct subscription type is important for following correct CTIA compliance
    /// guidelines.
    /// </remarks>
    /// <seealso cref="ContactSubscription" />
    /// <seealso cref="Subscription" />
    public enum SubscriptionType
    {
        /// <summary>
        /// Recurring subscriptions are for situations where multiple messages are going to be sent to a contact on an
        /// ongoing basis.  Most types of subscriptions will fall into this category.  A good example is for a marketing
        /// campaign where events and coupons might be sent out to a list of interested customers.
        /// </summary>
        Recurring = 1,
        
        /// <summary>
        /// One time subscriptions are for one off messages where the intention is to send a single message to that
        /// customer once.  A good example would be to notify specific people they have a package waiting in an office,
        /// or that their scheduled service call will be arriving shortly.
        /// </summary>
        OneTime = 2
    }
}