namespace Framework.Common.UI.Products.WestLawNext.Components.BrowsePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Browse Checkbox Component for Intellectual property page
    /// Used for All Intellectual Property page
    /// </summary>
    public sealed class IntellectualPropertyResourcesBrowseCheckboxComponent : BaseResourcesCheckboxComponent
    {
        private static readonly By TitleLocator = By.CssSelector(".co_browsePageSectionWidget>h1");
        private static readonly By ContainerLocator = By.ClassName("co_browsePageSectionWidget");
        private static readonly By CategoryLinksWithCheckboxesLocator = By.CssSelector(".co_browseContent .co_column li");
        
        /// <summary>
        /// Container locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Element container
        /// </summary>
        private IWebElement Container { get; }

        /// <summary>
        /// IntellectualPropertyBrowseCheckboxComponent constructor
        /// </summary>
        public IntellectualPropertyResourcesBrowseCheckboxComponent()
        {
            this.Container = DriverExtensions.GetElements(this.ComponentLocator).FirstOrDefault(x => x.Displayed);
        }

        /// <summary>
        /// Get browse component title
        /// </summary>
        /// <returns>title as a string</returns>
        public string GetTitleText() => DriverExtensions.GetText(TitleLocator, this.Container, 30);

        internal override IList<IWebElement> GetLinksElements() =>
            DriverExtensions.GetElements(this.Container, CategoryLinksWithCheckboxesLocator);
    }
}
