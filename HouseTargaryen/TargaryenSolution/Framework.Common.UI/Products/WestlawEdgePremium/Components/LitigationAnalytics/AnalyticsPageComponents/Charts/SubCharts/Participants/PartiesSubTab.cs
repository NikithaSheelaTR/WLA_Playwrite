namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.Participants
{
    using OpenQA.Selenium;

    /// <summary>
    /// 
    /// </summary>
    public class PartiesSubTab : ParticipantsTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "PartiesSubTab";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

    }
}