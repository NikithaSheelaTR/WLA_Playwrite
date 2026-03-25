namespace Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck.DocumentResults
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Arguments And Counterarguments Tab Item
    /// </summary>
    public class ArgumentCounterargumentItem : QuickCheckBaseItem
    {
        private static readonly By ArgumentsCounterArgumentsAndSummaryLocator = By.XPath(".//h5");
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//div[contains(@id, 'document')]//a");
        private static readonly By TitleLabelLocator = By.XPath(".//h4");
        private static readonly By KeyCiteTitleLabelLocator = By.XPath(".//div[@class='co_search_keyciteFlag']/following-sibling::a");

        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentCounterargumentItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public ArgumentCounterargumentItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(this.Container, TitleLabelLocator);

        /// <summary>
        /// Arguments, CounterArguments And CounterArguments Summary Labels
        /// </summary>
        public IReadOnlyCollection<ILabel> ArgumentsCounterArgumentsAndSummaryLabels => new ElementsCollection<Label>(this.Container, ArgumentsCounterArgumentsAndSummaryLocator);

        /// <summary>
        /// Document Title Links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentTitleLinks => new ElementsCollection<Link>(this.Container, DocumentTitleLinkLocator);

        /// <summary> 
        /// KeyCite Title Label
        /// </summary>
        public ILabel KeyCiteTitleLabel => new Label(this.Container, KeyCiteTitleLabelLocator);
    }
}
