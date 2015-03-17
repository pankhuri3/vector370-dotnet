using System;
using System.Collections.Generic;

namespace ThreeSeventy.Vector.Client.Tests
{
    public static class LookupTable
    {
        [ThreadStatic] public static int AccountId;
        [ThreadStatic]public static int SubAccountId;
        [ThreadStatic]public static int CampaignId;
        [ThreadStatic]public static int QuestionCampaignId;
        [ThreadStatic]public static int ContentId;
        [ThreadStatic]public static int ChannelId;
        [ThreadStatic]public static int CallbackId;
        [ThreadStatic]public static int KeywordId;
        [ThreadStatic]public static int SubscriptionId;
        [ThreadStatic]public static int ContentTemplateId1;
        [ThreadStatic]public static int ContentTemplateId2;
        [ThreadStatic]public static int SurveyCampaignId;
        [ThreadStatic]public static int CampaignTemplateId;
        [ThreadStatic]public static int ContactId;
        [ThreadStatic]public static int ContactSubscriptionId;
        [ThreadStatic]public static int ContactListId;
        [ThreadStatic]public static int EventId;
        [ThreadStatic]public static int EventScheduleId;
        [ThreadStatic]public static string UserId;
        [ThreadStatic]public static string Keyword;
    }
}
