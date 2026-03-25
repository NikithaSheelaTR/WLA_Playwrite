namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter
{
    using OpenQA.Selenium;

    /// <summary>
    /// ViewSavedComparisonsTabComponent
    /// </summary>
    public class ViewSavedComparisonsTabComponent : BaseComparisonToolTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@ng-show='selectedTab==2']");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "View Saved Comparisons";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}