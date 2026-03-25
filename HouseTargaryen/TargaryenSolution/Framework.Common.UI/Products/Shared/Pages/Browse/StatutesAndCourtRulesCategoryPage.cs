namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Statutes And Court Rules Category Page
    /// </summary>
    public class StatutesAndCourtRulesCategoryPage : CommonBrowsePage
    {
        private const string StatuteLinkLctMask = "//a[contains(@class, 'co_tocItem') and text()[contains(.,'{0}')]]";

        private const string DocumentImageLctMask = "//a[@class= 'co_tocImageLink' and @aria-label= 'Full Text Document for {0}']";

        private static readonly By CreateMultipleKeyciteAlertsButtonLocator = By.Id("co_docKeyCiteAlertAnchor");

        /// <summary>
        /// Clicks States Rule Link by text
        /// </summary>
        /// <param name="linkText"> Selected links text </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New page instance of the page </returns>
        public T ClickStateRuleLinkByText<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.XPath(string.Format(StatuteLinkLctMask, linkText))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks States Rule Document Image icon
        /// </summary>
        /// <param name="tocLink"> Document link text </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New  instance of the page </returns>
        public T ClickSuperBrowseIconByDocumentTitle<T>(string tocLink) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(DocumentImageLctMask, tocLink))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Create Multiple Keycite alerts button
        /// </summary>
        public IButton CreateMultipleKeyciteAlertsButton = new Button(CreateMultipleKeyciteAlertsButtonLocator);
    }
}