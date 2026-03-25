namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Document  Features
    /// </summary>
    public class DocumentBodyComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentBodyContainerLocator = By.XPath("//*[@id='co_document']");

        private static readonly By DocumentBodyLocator = By.XPath(".//*[@id='co_link_GTY.II.3']");

        private static readonly By LabourRepLocator = By.CssSelector("div.co_imageBlock a img.co_image");

        private static readonly By ImageLocator = By.CssSelector("div.co_imageBlock a.co_imageLink img.co_image");

        private static readonly By KeyCiteLocator = By.CssSelector("div.co_contentBlock a.crsw_KeyCiteImgLink");

        private static readonly By RelatedContentLocator = By.PartialLinkText("Related Content");

        private static readonly By WebsiteLinkLocator = By.PartialLinkText("AABS-Specific Forms page of the AABS website");

        private static readonly By CoRelatedContentLocator = By.XPath(".//div[@id='co_relatedContent']");

        private static readonly By TocLinksLocator = By.XPath(".//a[starts-with(@href, '#tocid')]");

        private static readonly By FirstCitationLocator = By.CssSelector("div.co_title first-citation");

        private static readonly By LastCitationLocator = By.CssSelector("div.co_title last-citation");

        private static readonly By SectionLocator = By.XPath(".//*[contains(text(),'Address for service')]");

        private static readonly By RecevierLocator = By.XPath(".//strong[contains(text(), 'Receivers')]");

        private static readonly By FootSectionLocator = By.XPath(".//*[@class='co_footnoteSectionTitle co_printHeading']");

        private static readonly By BottomCompositeIconLocator = By.XPath(".//*[@id='co_prelimGoldenLeaf']/a[@class='co_excludeAnnotations icon25 icon_superbrowse']");

        private static readonly By MiddleCompositeIconLocator = By.XPath(".//div[@id='co_prelimContainer']/div/div/div/a[1]");

        private static readonly By AboutThisDocumentLocator = By.XPath(".//*[contains(text(),'About this Document')]");

        private static readonly By DocumentHeaderLocator = By.XPath(".//*[@id='co_docHeaderContainer']/h1");

        private static readonly By DocumentContentLocator = By.XPath(".//*[@id='co_contentColumn']");

        private static readonly By DocumentEodLocator = By.XPath(".//*[@id='co_endOfDocument']");

        private static readonly By DocumentTitleLocator = By.ClassName("crsw_shortTitle co_title");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocumentBodyContainerLocator;

        /// <summary>
        /// FootNotes Component
        /// </summary>
        public FootNotesComponent FootNote { get; } = new FootNotesComponent();

        /// <summary>
        /// Related Abridjment Copmonent
        /// </summary>
        public RelatedAbridjmentComponent RelatedAbridjment { get; } = new RelatedAbridjmentComponent();

        /// <summary>
        /// Labour Rep Logo 
        /// </summary>
        /// <returns> Summary Text </returns>
        public IButton LabourRepButton => new Button(this.ComponentLocator, LabourRepLocator);

        /// <summary>
        /// Document Content  Label
        /// </summary>
        public ILabel DocumentContentLabel => new Label(this.ComponentLocator, DocumentContentLocator);

        /// <summary>
        /// Document EOD  Label
        /// </summary>
        public ILabel DocumentEodLabel => new Label(this.ComponentLocator, DocumentEodLocator);

        /// <summary>
        ///  Document Header 
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel DocumentLabel => new Label(this.ComponentLocator, DocumentHeaderLocator);

        /// <summary>
        /// About This Document
        /// </summary>
        /// <returns> Summary Text </returns>
        public IButton AboutThisDocumentButton => new Button(this.ComponentLocator, AboutThisDocumentLocator);

        /// <summary>
        /// Middle Composite Document Icon
        /// </summary>
        public IButton MiddleCompositeDocumentButton => new Button(this.ComponentLocator, MiddleCompositeIconLocator);

        /// <summary>
        /// Bottom composite Document Icon
        /// </summary>
        public IButton BottomCompositeDocumentButton => new Button(this.ComponentLocator, BottomCompositeIconLocator);

        /// <summary>
        /// Gets FootNote Section Tittle Text from Document 
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel SectionLabel => new Label(this.ComponentLocator, FootSectionLocator);

        /// <summary>
        /// First Citation Label
        /// </summary>
        public ILabel FirstCitationLabel => new Label(this.ComponentLocator, FirstCitationLocator);

        /// <summary>
        /// Last Citation Label
        /// </summary>
        public ILabel LastCitationLabel => new Label(this.ComponentLocator, LastCitationLocator);

        /// <summary>
        /// Gets  Text from Section Header
        /// </summary>
        public ILabel DocumentSectionLabel => new Label(this.ComponentLocator, SectionLocator);

        /// <summary>
        /// Gets  Text from Recevier Header
        /// </summary>
        public ILabel RecevierLabel => new Label(this.ComponentLocator, RecevierLocator);

        /// <summary>
        /// Website Link 
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILink WebsiteLink => new Link(this.ComponentLocator, WebsiteLinkLocator);

        /// <summary>
        /// CoRelated Content
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILink CoRelatedContentLink => new Link(this.ComponentLocator, CoRelatedContentLocator);

        /// <summary>
        /// Gets Image Block from Document 
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILabel ImageGraphicLabel => new Label(this.ComponentLocator, ImageLocator);

        /// <summary>
        ///Related Content Link
        /// </summary>
        public ILink RelatedContentLink => new Link(this.ComponentLocator, RelatedContentLocator);

        /// <summary>
        /// bottom composite doc icon
        /// </summary>
        public ILink TopBodylink => new Link(this.ComponentLocator, DocumentBodyLocator);

        /// <summary>
        /// Get the document title from document body
        /// </summary>
        public ILabel DocumentTitle => new Label(this.ComponentLocator, DocumentTitleLocator);

        /// <summary>
        /// Key Cite Images from Document
        /// </summary>
        /// <returns> Summary Text </returns>
        public IReadOnlyCollection<IWebElement> KeyCiteImages => DriverExtensions.GetElements(KeyCiteLocator);

        /// <summary>
        /// TOC Links from Document are stored
        /// </summary>
        /// <returns> Summary Text </returns>
        public IReadOnlyCollection<ILink> TocLink => new ElementsCollection<Link>(ComponentLocator, TocLinksLocator);
    }
}
