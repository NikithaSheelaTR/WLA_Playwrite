namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Labels.IpLabel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Patent result list item
    /// </summary>
    public class PatentResultsItem : ResultListItem
    {
        private static readonly By OwnerLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Owner:']/parent::*");

        private static readonly By FiledDateLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Filed Date:']/parent::*");

        private static readonly By GrantedDateLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Granted Date:']/parent::*");

        private static readonly By PriorityDateLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Priority Date:']/parent::*");

        private static readonly By PublicationDateLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Publication Date:']/parent::*");

        private static readonly By InventorLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Inventor:']/parent::*");

        private static readonly By ClaimLocator = By.XPath(".//div[contains(@class,'co_search_detailLevel')]/b[text()='Claim 1:']/parent::*");

        private static readonly By FirstLineIpCitationBy = By.XPath(".//div[contains(@class,'co_searchResults_citation')]/span[1]");

        private static readonly By CheckOriginalPdfLabelLocator = By.XPath(".//span[text()='Check the original PDF for drawings.']");

        /// <inheritdoc />
        public PatentResultsItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// The Drawing section
        /// </summary>
        public DrawingSectionComponent DrawingSection => new DrawingSectionComponent(this.Container);

        /// <summary>
        /// The first line IP citation label
        /// </summary>
        public ILabel FirstLineIpCitationLabel => new Label(this.Container, FirstLineIpCitationBy);

        /// <summary>
        /// Owner
        /// </summary>
        public ILabel Owner => new IpLabel("Owner: ", this.Container, OwnerLocator);

        /// <summary>
		/// Filed Date
		/// </summary>
		public ILabel FiledDate => new IpLabel("Filed Date: ", this.Container, FiledDateLocator);

        /// <summary>
        /// Granted Date
        /// </summary>
        public ILabel GrantedDate => new IpLabel("Granted Date: ", this.Container, GrantedDateLocator);

        /// <summary>
        /// Priority Date
        /// </summary>
        public ILabel PriorityDate => new IpLabel("Priority Date: ", this.Container, PriorityDateLocator);

        /// <summary>
		/// Publication Date
		/// </summary>
		public ILabel PublicationDate => new IpLabel("Publication Date: ", this.Container, PublicationDateLocator);

        /// <summary>
		/// Inventor
		/// </summary>
		public ILabel Inventor => new IpLabel("Inventor: ", this.Container, InventorLocator);

        /// <summary>
		/// Claim
		/// </summary>
		public ILabel Claim => new IpLabel("Claim 1:", this.Container, ClaimLocator);

        /// <summary>
        /// No drawings label
        /// </summary>
        public ILabel CheckOriginalPdf => new Label(this.Container, CheckOriginalPdfLabelLocator);
    }
}
