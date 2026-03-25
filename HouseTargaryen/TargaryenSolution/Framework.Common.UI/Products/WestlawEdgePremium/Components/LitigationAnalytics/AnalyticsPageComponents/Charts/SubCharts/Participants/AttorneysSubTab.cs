namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Charts.SubCharts.Participants
{
    using OpenQA.Selenium;

    /// <summary>
    /// AttorneySubTab
    /// </summary>
    public class AttorneysSubTab : ParticipantsTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class= 'la-Layout-chartContainer']");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "AttorneysSubTab";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

    }
}