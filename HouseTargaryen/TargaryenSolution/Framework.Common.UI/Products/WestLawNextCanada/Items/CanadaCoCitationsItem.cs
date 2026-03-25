namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Canada Co citations item
    /// </summary>
    public class CanadaCoCitationsItem : BaseItem
    {
        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");
        private static readonly By ItemCountLocator = By.XPath(".//div[contains(@id,'CitedWithResultItem')]");
        private static readonly By TitleLinkLocator = By.XPath(".//h3[@class='ResultItem-title']/a");
        private static readonly By CitationLocator = By.XPath(".//span[@class='ResultItem-metadataItem']");
        private static readonly By CoCitingCasesButtonLocator = By.XPath(".//button[@class='ResultItem-button']");

        /// <summary>
        /// Initializes a new instance of the CoCitationsItem class.
        /// Search result item constructor
        /// </summary>
        /// <param name="containerElement"> The container Element. </param>
        public CanadaCoCitationsItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckBoxLocator);

        /// <summary>
        /// Get item count
        /// </summary>
        /// <returns>the count of item</returns>
        public int GetItemCount() =>
            DriverExtensions.GetElement(this.Container, ItemCountLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Title
        /// </summary>
        public ILink Title => new Link(this.Container, TitleLinkLocator);

        /// <summary>
        /// Citations list
        /// </summary>
        public IReadOnlyCollection<ILabel> Citations =>
            new ElementsCollection<Label>(this.Container, CitationLocator);

        /// <summary>
        /// [N] Co-citing cases button
        /// </summary>
        public IButton CoCitingCasesButton => new Button(this.Container, CoCitingCasesButtonLocator);
    }
}