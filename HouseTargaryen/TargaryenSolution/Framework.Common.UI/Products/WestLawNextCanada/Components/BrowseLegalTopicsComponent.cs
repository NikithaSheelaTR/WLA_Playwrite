namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
   
    /// <summary>
    /// Contains Browse Legal Topics Component feature tests of Westlaw Next Canada
    /// </summary>
    public class BrowseLegalTopicsComponent : BaseModuleRegressionComponent
    {
        private const string CheckboxLocatorMsk = "//div[@class='co_selectable co_tocItem']/input[@id='{0}']";
        private static readonly By ContainerLocator = By.Id("co_contentColumn");
        private static readonly By SubchapterTitleLocator = By.Id("co_browsePageLabel");
        private static readonly By LegalTopicLinksLocator = By.XPath(".//a[contains(@class,'co_tocItemLink')]");
        private static readonly By TitleLinksLocator = By.XPath("//a[contains(@class,'co_tocItemLink')]/span[@class='crsw_tocTitleHeader'] | .//*[contains(@id,'co_tocItemLink')]//div[contains(@class,'co_tocTreeItemLinkIcon')]//a");
        private static readonly By SelectAllContentCheckboxLocator = By.XPath(".//input[@id='coid_browseSelectAllCheckboxInput']");
        private static readonly By LegalTopicPageHeadingLocator = By.XPath(".//*[@id='coid_website_searchAvailableFacets']/div/h1");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Legal Topic Links
        /// </summary>
        public IReadOnlyCollection<ILink> LegalTopicLinks => new ElementsCollection<Link>(this.ComponentLocator, LegalTopicLinksLocator);

        /// <summary>
        /// List of Favourites Links in favourites page
        /// </summary>
        public IReadOnlyCollection<ILink> TitleLinks => new ElementsCollection<Link>(TitleLinksLocator);

        /// <summary>
        ///  Select All Content Checkbox 
        /// </summary>
        public ICheckBox SelectAllContentCheckbox => new CheckBox(this.ComponentLocator, SelectAllContentCheckboxLocator);

        /// <summary>
        /// Legal Topic Page Heading
        /// </summary>
        public ILabel LegalTopicPageHeadingLabel => new Label(this.ComponentLocator, LegalTopicPageHeadingLocator);

        /// <summary>
        ///  Document Title Label
        /// </summary>
        public ILabel DocumentTitleLabel => new Label(this.ComponentLocator,SubchapterTitleLocator);

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<MasterTaxonomyLink, WebElementInfo> CheckBoxMap
            => this.legalTopicMap = this.legalTopicMap ?? EnumPropertyModelCache.GetMap<MasterTaxonomyLink, WebElementInfo>(
            string.Empty, @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Click Content Type Link by Content Type - View Pane
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="masterTaxonomy"> Master Taxonomy </param>
        /// <returns> New instance of the page </returns>
        public T ClickLegalTopicsLink<T>(MasterTaxonomyLink masterTaxonomy) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(CheckboxLocatorMsk, this.CheckBoxMap[masterTaxonomy].Text))).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        private EnumPropertyMapper<MasterTaxonomyLink, WebElementInfo> legalTopicMap;
    }
}
