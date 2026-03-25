namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.Delivery
{
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;

    /// <summary>
    /// The Basics Tab
    /// </summary>
    public class LitigationAnalyticsTheBasicsAlalyticsTabComponent : TheBasicsTabComponent
    {
        /// <summary>
        /// What To Deliver component
        /// </summary>
        public new LitigationAnalyticsWhatToDeliverComponent WhatToDeliver { get; } = new LitigationAnalyticsWhatToDeliverComponent();

        ///<summary>
        /// Analytics Items To Include
        ///</summary>
        public LitigationAnalyticsItemsToIncludeComponent ItemsToInclude { get; } = new LitigationAnalyticsItemsToIncludeComponent();
    }
}