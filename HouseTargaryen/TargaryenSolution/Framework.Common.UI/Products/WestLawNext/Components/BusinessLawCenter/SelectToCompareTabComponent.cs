namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter
{
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SelectToCompareTabComponent
    /// </summary>
    public class SelectToCompareTabComponent : BaseComparisonToolTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@ng-show='selectedTab==1']");

        private static readonly By StartCompareButtonLocator = By.XPath("//input[@value='Start Compare']");        

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Select To Compare";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Select item
        /// </summary>
        /// <param name="itemIndex"> Item to select </param>
        /// <returns> The <see cref="SelectToCompareTabComponent"/>. </returns>
        public SelectToCompareTabComponent SelectItem(int itemIndex)
        {
            this.GetItem(itemIndex).SetCheckbox(true);
            return this;
        }

        /// <summary>
        /// Click Start Compare button
        /// </summary>
        /// <returns> The <see cref="RedlineReportPage"/>. </returns>
        public RedlineReportPage ClickStartCompareButton()
        {
            DriverExtensions.WaitForElement(StartCompareButtonLocator).Click();
            return new RedlineReportPage();
        }
    }
}