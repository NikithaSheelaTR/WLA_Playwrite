namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Suggested titles dialog
    /// </summary>
    public class SuggestedTitlesDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath(".//a[@id = 'co_suggestedTitlesCancelButton']");
        private static readonly By TopicItemLinkLocator = By.XPath(".//ul[@class = 'co_SuggestedTitle-topicItems']//a");
        private static readonly By ContentTopicTitleLocator = By.XPath(".//ul[@class = 'co_SuggestedTitle-topic']//h3");
        private static readonly By DialogHeaderTitleLocator = By.XPath(".//div[@class = 'co_overlayBox_headline']//h3");

        private IWebElement Container => DriverExtensions.WaitForElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.SuggestedTitles].LocatorString));

        /// <summary>
        /// Get dialog title
        /// </summary>
        /// <returns>Dialog title.</returns>
        public string GetTitle() => DriverExtensions.GetElement(this.Container, DialogHeaderTitleLocator).Text.Trim();

        /// <summary>
        /// Get topics titles
        /// </summary>
        /// <returns>List of topics titles.</returns>
        public List<string> GetTopicsTitles() => DriverExtensions.GetElements(this.Container, ContentTopicTitleLocator)
                                                                 .Select(el => el.Text).ToList();

        /// <summary>
        /// Click 'Close button'
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <returns>New instance of T</returns>
        public T ClickCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(this.Container, CloseButtonLocator);
    }
}