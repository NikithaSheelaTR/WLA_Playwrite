namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Dialog that pops up when clicking Open preview
    /// </summary>
    public class GraphicalHistorySearchPreviewDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Panel-right GH-Panel']");               
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='Panel-close']");
        private static readonly By SearchTypeLocator = By.XPath(".//li");
        private static readonly By EventTitleLocator = By.Id("co_titleLink");
        private static readonly By SummaryLabelLocator = By.ClassName("GH-Panel-summary");

        /// <summary>
        /// Search content labels
        /// </summary>
        public IReadOnlyCollection<ILabel> SearchContentLabels => new ElementsCollection<Label>(ContainerLocator, SummaryLabelLocator, SearchTypeLocator);

        /// <summary>
        /// Summary label
        /// </summary>
        public ILabel SummaryLabel => new Label(ContainerLocator, SummaryLabelLocator);

        /// <summary>
        /// Close button search preview panel
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Event title link
        /// </summary>
        public ILink EventTitleLink => new Link(ContainerLocator, EventTitleLocator);
    }
}