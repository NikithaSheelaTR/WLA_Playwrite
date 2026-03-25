namespace Framework.Common.UI.Products.WestLawNext.Components.BrowsePage
{
    using System.Collections.Generic;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Resources Checkbox 
    /// Used for Intelectual property Patent, Trademark, Copyright
    /// </summary>
    public sealed class ResourcesCheckboxSectionComponent : BaseResourcesCheckboxComponent
    {
        private const string ContainerLctMask =
            "//div[contains(@class,'co_tabShow')]//div[@class='co_browsePageSectionWidget']//ul/li[.//label[@id='co_columnCheckboxLabel' and contains(.,'{0}')]]";

        private static readonly By CategoryLinksWithCheckboxesLocator = By.ClassName("co_columnSectionItem");

        /// <summary>
        /// IntellectualPropertyBrowseCheckboxComponent constructor
        /// </summary>
        public ResourcesCheckboxSectionComponent(string name)
        {
            this.Name = name;
        }

        /// <summary>
        /// Container locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(string.Format(ContainerLctMask, this.Name));

        private string Name { get; }


        internal override IList<IWebElement> GetLinksElements() =>
            DriverExtensions.GetElements(this.ComponentLocator, CategoryLinksWithCheckboxesLocator);
    }
}
