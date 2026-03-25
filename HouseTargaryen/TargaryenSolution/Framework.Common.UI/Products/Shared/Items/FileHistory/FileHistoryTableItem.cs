
namespace Framework.Common.UI.Products.Shared.Items.FileHistory
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// File History table item
    /// </summary>
    public class FileHistoryTableItem : BaseItem
    {
        private static readonly By PdfIconLocator = By.XPath(".//a");

        private static readonly By PdfCheckBoxLocator = By.XPath(".//input");

        private static readonly By DateLabelLocator = By.XPath(".//td[3]");

        private static readonly By DocumentTitleLabelLocator = By.XPath(".//td[4]");

        private static readonly By SortByDateButtonLocator = By.XPath(".//th[3]/button");

        private static readonly By SortByDocTitleButtonLocator = By.XPath(".//th[4]/button");

        /// <summary>
        /// File History Pdf Table Item
        /// </summary>
        public FileHistoryTableItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// The Date label
        /// </summary>
        public ILabel Date => new Label(this.Container,DateLabelLocator);


        /// <summary>
        /// The Title label
        /// </summary>
        public ILabel Title => new Label(this.Container, DocumentTitleLabelLocator);

        /// <summary>
        /// The Pdf Icon link
        /// </summary>
        public ILink PdfIconLink => new Link(this.Container, PdfIconLocator);

        /// <summary>
        /// The Pdf checkbox
        /// </summary>
        public ICheckBox PdfCheckBox => new CheckBox(this.Container, PdfCheckBoxLocator);

        /// <summary>
        /// The Sort by date button
        /// </summary>
        public IButton SortByTitleButton => new Button(this.Container, SortByDocTitleButtonLocator);

        /// <summary>
        /// The Sort by date button
        /// </summary>
        public IButton SortByDateButton => new Button(this.Container, SortByDateButtonLocator);
    }
}
