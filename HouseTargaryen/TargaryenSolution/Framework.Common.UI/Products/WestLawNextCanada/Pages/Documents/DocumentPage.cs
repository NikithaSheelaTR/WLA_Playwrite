namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Documents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Document Page
    /// </summary>
    public class DocumentPage : EdgeCommonDocumentPage
    {
        private static readonly By FooterLocator = By.XPath("//*[@class='co_footnoteReference']");

        private static readonly By ClassificationLinkTextLocator = By.XPath("//div[@class='co_customDigestSearchResult_tree']");

        private static readonly By AbridgmentLocator = By.XPath("//*[@id='co_browsePageLabel']//span");

        private static readonly By SectionLocator = By.XPath("//*[@id='cobalt_result_can_direxpertwitness_title1']");

        private static readonly By DisplayLogoLocator = By.XPath("//*[@id='crsw_originalPdfUrlContainer']/a");

        private static readonly By DocumentBrowsePaneLocator = By.XPath("//*[@id='co_browsePageLabel']");

        private static readonly By CurrencyBlockFirstParaLabelLocator = By.XPath("//div[contains(@class,'co_includeCurrencyBlock')]/div/strong");

        private static readonly By CurrencyBlockContentLabelLocator = By.XPath("//div[contains(@class,'co_includeCurrencyBlock')]/div//li");

        private const string CurrentBlockLocator = "//div[contains(@class,'co_includeCurrencyBlock')]/strong[text()='{0}']";

        private const string InternalLinkLctMask = "//a[@class='co_internalLink' and text()=\"{0}\"]";

        /// <summary>
        /// Document Browse Pane Label
        /// </summary>
        public ILabel DocumentBrowsePaneLabel => new Label(DocumentBrowsePaneLocator);

        /// <summary>
        /// Display Logo Button
        /// </summary>
        /// <returns> Summary Text </returns>
        public IButton DisplayLogoButton => new Button(DisplayLogoLocator);

        /// <summary>
        /// Section Button
        /// </summary>
        public IButton SectionButton => new Button(SectionLocator);

        /// <summary>
        /// Classification link Label
        /// </summary>
        public ILabel ClassificationLabel => new Label(ClassificationLinkTextLocator);

        /// <summary>
        ///  Abridgment  Label
        /// </summary>
        public ILabel AbridgmentLabel => new Label(AbridgmentLocator);

        /// <summary>
        ///Display Footer Link
        /// </summary>
        /// <returns> Summary Text </returns>
        public ILink DisplayFooterLink => new Link(FooterLocator);

        /// <summary>
        /// Narrow Pane Component
        /// </summary>
        public DocumentNarrowPaneComponent NarrowPane { get; } = new DocumentNarrowPaneComponent();

        /// <summary>
        /// Navigation Component
        /// </summary>
        public DocumentNavigationComponent NavigationComponent { get; } = new DocumentNavigationComponent();

        /// <summary>
        /// Document Header Component
        /// </summary>
        public DocumentHeaderComponent DocumentHeader { get; } = new DocumentHeaderComponent();

        /// <summary>
        /// Document Body Component
        /// </summary>
        public DocumentBodyComponent Document { get; } = new DocumentBodyComponent();

        /// <summary>
        /// Currency First Para Label
        /// </summary>
        public ILabel CurrencyFirstParaLabel => new Label(CurrencyBlockFirstParaLabelLocator);

        /// <summary>
        /// Curenncy block content labels
        /// </summary>
        public IReadOnlyCollection<ILabel> CurrencyBlockContentLabels => new ElementsCollection<Label>(CurrencyBlockContentLabelLocator);

        /// <summary>
        /// The click document internal link.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        public void ClickDocumentInternalLink(string linkName) => 
            DriverExtensions.WaitForElement(By.XPath(string.Format(InternalLinkLctMask, linkName))).Click();

        /// <summary>
        /// Verify is link is displayed in document
        /// </summary>
        /// <param name="linkName">Link text</param>
        /// <returns>true if present</returns>
        public bool IsDocumentInternalLinkDisplayed(string linkName) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(InternalLinkLctMask, linkName))).IsDisplayed();

        /// <summary>
        /// Verifies is expected block in view
        /// </summary>
        /// <param name="blockText">Block text to verify</param>
        /// <returns>True if in view</returns>
        public bool IsCurrencyBlockInView(string blockText) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(CurrentBlockLocator, blockText))).IsElementInView();
    }
}
