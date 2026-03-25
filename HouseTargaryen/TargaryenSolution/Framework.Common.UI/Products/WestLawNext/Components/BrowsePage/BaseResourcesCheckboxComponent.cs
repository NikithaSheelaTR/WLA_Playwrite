namespace Framework.Common.UI.Products.WestLawNext.Components.BrowsePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base component for Resources Checkbox
    /// Used for Intelectual property, Patent, Trademark, Copyright browse pages
    /// </summary>
    public abstract class BaseResourcesCheckboxComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// Get List Of Category Links as a text
        /// </summary>
        /// <returns>list of category page links</returns>
        public List<string> GetListOfCategoryLinks() => this.GetCategoryLinksWithCheckboxes().Select(k => k.Key.Text).ToList();

        /// <summary>
        /// Select Category Checkbox By Category Name
        /// </summary>
        /// <param name="categoryLinkName">Category link name</param>
        /// <param name="setTo">required state for checkbox</param>
        /// <returns></returns>
        protected void SetCategoryCheckboxByName(string categoryLinkName, bool setTo) => DriverExtensions.SetCheckbox(
            setTo,
            this.GetCategoryLinksWithCheckboxes().First(k => k.Key.Text.Equals(categoryLinkName)).Value);

        internal abstract IList<IWebElement> GetLinksElements();

        /// <summary>
        /// Get List Of Category Links
        /// </summary>
        /// <returns>Dictionary with pair Link and Checkbox</returns>
        private Dictionary<IWebElement, IWebElement> GetCategoryLinksWithCheckboxes()
        {
            IList<IWebElement> links = this.GetLinksElements();
            return links.ToDictionary(
                link => DriverExtensions.GetElement(link, By.TagName("a")),
                link => DriverExtensions.GetElement(link, By.TagName("input")));
        }
    }
}
