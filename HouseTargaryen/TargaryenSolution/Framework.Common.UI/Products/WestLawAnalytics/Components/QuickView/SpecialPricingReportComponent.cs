namespace Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Special pricing report item
    /// </summary>
    public class SpecialPricingReportComponent : BaseModuleRegressionComponent
    {
        private static readonly By FrameTitleLocator = By.Id("lblPageTitle");

        private static readonly By SubmitButtonBottomLocator = By.Id("btnBottomSubmit");

        private static readonly By SubmitButtonTopLocator = By.Id("btnTopSubmit");

        private static readonly By ContainerLocator = By.XPath("//form[@id='Form1']/table[2]");

        /// <summary>
        /// Checks whether report component initialized properly  
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsSpeccialPricingFrameDisplayed => DriverExtensions.IsDisplayed(FrameTitleLocator);

        /// <summary>
        /// Checks whether submit button is displayed on the bottom of a page
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsSubmitButtonBottomDisplayed => DriverExtensions.IsDisplayed(SubmitButtonBottomLocator);

        /// <summary>
        /// Checks whether submit button is displayed on the top of a page
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsSubmitButtonTopDisplayed => DriverExtensions.IsDisplayed(SubmitButtonTopLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks on submit button
        /// </summary>
        /// <returns><see cref="AnalyticsReportComponent"/></returns>
        public AnalyticsReportComponent ClickSubmitButton()
        {
            DriverExtensions.GetElement(SubmitButtonTopLocator).Click();
            DriverExtensions.WaitForElementNotDisplayed(120000, SubmitButtonTopLocator);
            return new AnalyticsReportComponent();
        }

        /// <summary>
        /// Gets frame title
        /// </summary>
        /// <returns>Frame title text</returns>
        public string GetFrameTitle() => DriverExtensions.WaitForElement(FrameTitleLocator).Text;
    }
}