namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using System.Collections.Generic;
	using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Document;
	using Framework.Common.UI.Products.Shared.Models.EnumProperties;
	using Framework.Common.UI.Products.Shared.Pages.Document;
	using Framework.Common.UI.Products.WestLawNext.Models.BusinessObjects;
	using Framework.Common.UI.Utils.Mapper;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
	using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

	/// <summary>
	/// Common Patent Document Page
	/// </summary>
	public class PatentDocumentPage : CommonDocumentPage
    {
        private static readonly By PdfLinkLocator = By.XPath("//a[@class='co_blobLink']");
        private EnumPropertyMapper<PatentDocumentSection, WebElementInfo> patentDocumentSectionMap;

		/// <summary>
		/// Gets the DocumentConstituent enumeration to WebElementInfo map.
		/// </summary>
		private EnumPropertyMapper<PatentDocumentSection, WebElementInfo> PatentDocumentSectionMap
            => this.patentDocumentSectionMap = this.patentDocumentSectionMap ?? EnumPropertyModelCache.GetMap<PatentDocumentSection, WebElementInfo>();

		/// <summary>
		/// Get piece of document text by document paragraph index
		/// </summary>
		/// <param name="docConstituent">Constituent</param>
		/// <returns> Text from document constituent </returns>
		public List<string> GetPatentDocumentSectionTextList(PatentDocumentSection docConstituent) => DriverExtensions
			.GetElements(By.XPath(this.PatentDocumentSectionMap[docConstituent].LocatorString))?.Select(el => el?.Text).ToList();

		/// <summary>
		/// To Patent Document Model
		/// </summary>
		/// <returns><see cref="PatentDocumentModel"/>patent document model</returns>
		public PatentDocumentModel ToModel() => MapperService.Map<PatentDocumentModel>(this);

		/// <summary>
		/// Link to the original patent PDF file
		/// </summary>
		public ILink OriginalPdfLink => new Link(PdfLinkLocator);

        /// <summary>
        /// Drawings component
        /// </summary>
        public DrawingsComponent DrawingsSection => new DrawingsComponent();

        /// <summary>
        /// Select a specific document section
        /// </summary>
        /// <param name="docConstituent">DocSection</param>
        /// <param name="paragraphIndex">The index of a paragraph</param>
        /// <returns> The <see cref="HighlightMenuDialog"/>. </returns>
        public HighlightMenuDialog SelectPatentDocumentSection(PatentDocumentSection docConstituent, int paragraphIndex = 0) =>
            this.SelectSnippet(DriverExtensions.GetElements(By.XPath(this.PatentDocumentSectionMap[docConstituent].LocatorString)).ElementAt(paragraphIndex));
    }
}