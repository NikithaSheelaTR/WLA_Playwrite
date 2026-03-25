namespace Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Report item
    /// </summary>
    public class ReportCreationComponent : BaseModuleRegressionComponent
    {
        private static readonly By FrameTitleLocator = By.Id("lblPageTitle");

        private static readonly By LeftPaneLabelLocator = By.Id("lblLeftTitle");

        private static readonly By SubmitButtonBottomLocator = By.Id("btnBottomSubmit");

        private static readonly By SubmitButtonTopLocator = By.Id("btnTopSubmit");

        private static readonly By ContainerLocator = By.XPath("//form[@id='Form1']/table[2]");

        /// <summary>
        /// Gets left pane label value
        /// </summary>
        /// <returns><see cref="string"/></returns>
        public string GetLeftLabelText => DriverExtensions.GetText(LeftPaneLabelLocator);

        /// <summary>
        /// Checks whether Left pane label is displayed
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsLeftPaneLabelDisplayed => DriverExtensions.IsDisplayed(LeftPaneLabelLocator);

        /// <summary>
        /// Checks whether report component initialized properly  
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsReportsFrameDisplayed => DriverExtensions.IsDisplayed(FrameTitleLocator);

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
        /// <returns><see cref="SpecialPricingReportComponent"/></returns>
        public SpecialPricingReportComponent ClickSubmitButton()
        {
            DriverExtensions.GetElement(SubmitButtonTopLocator).Click();
            return new SpecialPricingReportComponent();
        }

        /// <summary>
        /// Gets frame title
        /// </summary>
        /// <returns>Frame title text</returns>
        public string GetFrameTitle() => DriverExtensions.GetText(FrameTitleLocator);
    }
}