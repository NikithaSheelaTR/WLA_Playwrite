namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Corporate commercial Page
    /// </summary>
    public class CorporateCommercialPage : EdgeCommonDocumentPage
    {
        private const string DocumentCheckboxLctMsk = "//li//a[contains(text(),'{0}')]/ancestor::div[1]/input";
        private const string DocumentLinkLctMsk = "//a[@class='co_tocItemLink' and contains(text(),'{0}')]";
        private const string ExpandIconLctMsk = "//span[contains(text(),'{0}')]/parent::div/a[@class='co_genericExpand']";

        private static readonly By BreadcrumbLinkLocator = By.XPath("//*[@id='coid_website_breadCrumbTrail']");
        private static readonly By DocumentsLinkLocator = By.XPath("//li[@role='treeitem']");
        private static readonly By TitleLocator = By.XPath("//h1[@id='co_browsePageLabel']");
       // private static readonly By DocumentLinkLctMsk = By.XPath(".//*[@class='co_selectable co_tocItem']//descendant::a");

        /// <summary>
        /// Taxnet Pro Filter Component
        /// </summary>
        public TaxnetProSearchFacetsFilterComponent Filter { get; } = new TaxnetProSearchFacetsFilterComponent();

        /// <summary>
        /// PersonalInjuryContentType Component
        /// </summary>
        public PersonalInjuryContentTypeComponent PersonalInjuryContentType { get; } = new PersonalInjuryContentTypeComponent();

        /// <summary>
        /// Favorites Components elements
        /// </summary>
        public FavoritesComponent Favorites => new FavoritesComponent();

        /// <summary>
        /// Breadcrumb link
        /// </summary>
        public ILink BreadcrumbLink => new Link(BreadcrumbLinkLocator);

        /// <summary>
        /// Duty link based on name.
        /// </summary>
        /// <param name="dutyName">The flag.</param>
        public ILink DutyLink(string dutyName) => new Link(DriverExtensions.GetElement(
                By.XPath(string.Format(ExpandIconLctMsk, dutyName))));

        /// <summary>
        /// Document chekcbox element by document name
        /// </summary>
        /// <param name="documentName">Document Name</param>
        public ICheckBox DocumentCheckBox(string documentName) =>
            new CheckBox(DriverExtensions.GetElement(By.XPath(string.Format(DocumentCheckboxLctMsk, documentName))));

        /// <summary>
        /// Expand duty based on name.
        /// </summary>
        /// <param name="dutyName">The flag.</param>
       public IButton DutyExpandButton(string dutyName) => new Button(DriverExtensions.GetElement(
                By.XPath(string.Format(DocumentLinkLctMsk, dutyName))));


        /// <summary>
        /// Page title label
        /// </summary>
        public ILabel PageTitleLable => new Label(TitleLocator);

        /// <summary>
        /// Document links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentLinks => new ElementsCollection<Link>(DocumentsLinkLocator);
    }
}
