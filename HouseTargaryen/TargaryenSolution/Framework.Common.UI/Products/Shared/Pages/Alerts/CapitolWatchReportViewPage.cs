namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Capitol Watch Report View Page
    /// </summary>
    public class CapitolWatchReportViewPage : BaseModuleRegressionPage
    {
        private static readonly By ReturnToReportCenterButtonLocator = By.Id("return_to_alertCenter");
        
        private static readonly By ViewFormButtonLocator =
            By.XPath("//div[@id='co_reportFormToggleContainer']/a[@class='co_tbButton']");

        /// <summary>
        /// Gets or sets Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; set; } = new Toolbar();

        /// <summary>
        /// Click Return To Report Center Button
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <returns>new instance of specified type</returns>
        public T ClickReturnToReportCenterButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(ReturnToReportCenterButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is return to report center button displayed.
        /// </summary>
        /// <returns>
        /// True if button displayed,false otherwise.
        /// </returns>
        public bool IsReturnToReportCenterButtonDisplayed() => DriverExtensions.IsDisplayed(ReturnToReportCenterButtonLocator, 5);

        /// <summary>
        /// Select the specified form
        /// </summary>
        /// <param name="longForm">form to select, long is true, short if false</param>
        public void SelectForm(bool longForm = true)
        {
            if (this.IsLongFormSelected() != longForm)
            {
                DriverExtensions.WaitForElement(ViewFormButtonLocator).Click();
            }
        }

        private bool IsLongFormSelected() => DriverExtensions.WaitForElement(ViewFormButtonLocator).Text.Equals("View Long Form");
    }
}