namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// PersonalInjuryContentType from Canada
    /// </summary>
    public class PersonalInjuryContentTypeComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContentTypeContainerLocator = By.XPath(".//div[@id='co_contentWrapper']");
        private static readonly By SelectAllCheckBoxLocator = By.XPath(".//input[@id='co_searchHeader_selectAll']");
        private static readonly By DetailLevelLabelLocator = By.XPath(".//*[@class='co_search_detailLevel_1']");
        private static readonly By ResultListLinksLocator = By.XPath(".//*[contains(@id,'cobalt_result_can_personal_injury_title')]");
        private static readonly By ProductNameLabelLocator = By.XPath(".//div[@class='co_productname']");
        private static readonly By RelatedContentLinkLocator = By.XPath(".//*[@class='co_relatedContentLink']");
        private static readonly By RelatedContentLabelLocator = By.XPath(".//*[@id='co_relatedContent']//h2");
        private static readonly By DocumentLinkLocator = By.XPath(".//div[@class='co_inline']");
        private static readonly By BrowseContentLinksLocator = By.XPath("//*[@class='co_browseContent_iconItem']//a");
        private const string ExpanderButtonLctMask = ".//*[contains(text(), '{0}') and @class='co_expanderLink']";
        private const string TocItemLctMask = "//*[contains(text(), '{0}') and @class='co_tocItemLink']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContentTypeContainerLocator;

        /// <summary>
        /// SelectAll CheckBox
        /// </summary>
        public ICheckBox SelectAllCheckBox => new CheckBox(this.ComponentLocator, SelectAllCheckBoxLocator);

        /// <summary>
        /// DetailLevel Label
        /// </summary>
        public ILabel DetailLevelLabel => new Label(this.ComponentLocator, DetailLevelLabelLocator);

        /// <summary>
        /// ResultList Links
        /// </summary>
        public IReadOnlyCollection<ILink> ResultListLinks => new ElementsCollection<Link>(this.ComponentLocator, ResultListLinksLocator);

        /// <summary>
        /// ProductName Label
        /// </summary>
        public ILabel ProductNameLabel => new Label(this.ComponentLocator, ProductNameLabelLocator);

        /// <summary>
        /// RelatedContent Link
        /// </summary>
        public ILink RelatedContentLink => new Link(this.ComponentLocator, RelatedContentLinkLocator);

        /// <summary>
        /// RelatedContent Label
        /// </summary>
        public ILabel RelatedContentLabel => new Label(this.ComponentLocator, RelatedContentLabelLocator);

        /// <summary>
        /// Document Link
        /// </summary>
        public ILink DocumentLink => new Link(this.ComponentLocator, DocumentLinkLocator);

        /// <summary>
        /// BrowseContent Links
        /// </summary>
        public IReadOnlyCollection<ILink> BrowseContentLinks => new ElementsCollection<Link>(this.ComponentLocator, BrowseContentLinksLocator);

        /// <summary>
        /// Expands toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public void ExpandNodeByName(string docTitle) =>
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(ExpanderButtonLctMask, docTitle))).Click();

        /// <summary>
        /// Click toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public T ClickLinkByName<T>(string docTitle) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(TocItemLctMask, docTitle))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
