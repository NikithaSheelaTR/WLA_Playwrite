
using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Dialogs;
using Framework.Common.UI.Products.Shared.Elements;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs
{
    /// <summary>
    /// Advantage History Dialog
    /// </summary>
    public class AdvantageHistoryDialog : BaseModuleRegressionDialog
    {
        private static readonly By HistoryLabelLocator = By.XPath("//h2[contains(text(), 'History')]");
        private static readonly By ClientNameButtonLocator = By.XPath(".//saf-select[@label='Client' and @current-value='TEST DEMO']");
        private static readonly By EventNameLocator = By.XPath(".//saf-select[@label='Event']//saf-listbox-option[normalize-space(text())='All']");
        private static readonly By OpenFullHistoryButtonLocator = By.XPath("//saf-button[@aria-label='Open all history in new tab']");
        private static readonly By HistoryElementLocator = By.XPath("//li");
        private static readonly By ContentTypeContainerLocator = By.XPath("//div[contains(@class,'__panelContent')]");
        private static readonly By AllHistoryLabelLocator = By.XPath(".//h1[contains(text(), 'History: All History')]");
        private static readonly By SearchResultsContainerLocator = By.XPath("//ul[contains(@class, '__content')]");
        private static readonly By SearchResultDateTimeStampLabelLocator = By.XPath("//div[contains(@class,'Meta')]/time");
        private static readonly By SearchResultAJSQueryLocator = By.XPath("//a[contains(@id, 'fiftyStates')]");
        
        /// <summary>
        /// History Label
        /// </summary>
        public ILabel HistoryLabel => new Label(HistoryLabelLocator);

        /// <summary>
        /// Client Button
        /// </summary>
        public IButton ClientNameButton => new Button(ContentTypeContainerLocator,ClientNameButtonLocator);

        /// <summary>
        /// Event
        /// </summary>
        public IButton EventName => new Button(ContentTypeContainerLocator,EventNameLocator);

        /// <summary>
        /// Open Full History Button
        /// </summary>
        public IButton OpenFullHistoryButton => new Button(OpenFullHistoryButtonLocator);

        /// <summary>
        /// History Label
        /// </summary>
        public ILabel AllHistoryLabel => new Label(AllHistoryLabelLocator);

        /// <summary>
        ///  All History questions
        /// </summary>
        public IReadOnlyCollection<IButton> HistoryButton => new ElementsCollection<Button>(ContentTypeContainerLocator, HistoryElementLocator);

        /// <summary>
        /// search results
        /// </summary>
        public IReadOnlyCollection<ILink> AJSSearchResultsQuery => new ElementsCollection<Link>(SearchResultsContainerLocator, SearchResultAJSQueryLocator);

        /// <summary>
        ///  Search result date time stamp label
        /// </summary>
        public IButton SearchResultDateTimeStampLabel => new Button(SearchResultDateTimeStampLabelLocator);
    }
}
