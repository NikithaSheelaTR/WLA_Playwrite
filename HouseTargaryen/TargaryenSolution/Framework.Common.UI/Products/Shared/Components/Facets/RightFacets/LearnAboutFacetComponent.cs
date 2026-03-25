namespace Framework.Common.UI.Products.Shared.Components.Facets.RightFacets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// "Learn About" section on medical litigator search results page
    /// </summary>
    public class LearnAboutFacetComponent : BaseModuleRegressionComponent
    {
        private const string ExpandButtonLctMask = "//a[contains(@childlistid,'facet_expandable') and text()='{0}']";

        private static readonly By LearnAboutItemLocator = By.XPath(".//a[@class='draggable_document_link']");

        private static readonly By LearnAboutTitleLocator = By.XPath(".//strong[contains(text(), 'Learn About')]");

        private static readonly By ContainerLocator = By.Id("coid_website_learnAbout");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Expands the search tree in the Learn About section
        /// </summary>
        /// <param name="treeRoot"> The tree Root. </param>
        public void ExpandSearhTree(string treeRoot) => DriverExtensions.Click(By.XPath(string.Format(ExpandButtonLctMask, treeRoot)));

        /// <summary>
        /// Verifies is Learn About Title displayed
        /// </summary>
        /// <returns> True if 'Learn About' is present, false otherwise</returns>
        public bool IsTitleDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, LearnAboutTitleLocator);

        /// <summary>
        /// Get a list of the elements listed on the "Learn About" section
        /// </summary>
        /// <returns> List of the elements from 'Learn About' component </returns>
        public List<string> GetLearnAboutList() => DriverExtensions.GetElements(this.ComponentLocator, LearnAboutItemLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Verify 'Learn About' is present
        /// </summary>
        /// <returns> True if 'Learn About' is present, false otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Open an element listed on the "Learns About" section
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="elementName"> Document to open </param>
        /// <returns> New instance of the page </returns>
        public T OpenSummaryDocument<T>(string elementName) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(this.ComponentLocator, LearnAboutItemLocator).FirstOrDefault(elem => elem.Text.Equals(elementName))?.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Return false if any item in ListToVerify is not listed under learn about
        /// </summary>
        /// <param name="listToVerify"> List to verify </param>
        /// <returns> True if lists are equal</returns>
        public bool VerifyLearnAboutList(List<string> listToVerify)
        {
            List<string> learnAboutList =
                DriverExtensions.GetElements(this.ComponentLocator, LearnAboutItemLocator).Select(elem => elem.Text).ToList();
            return !listToVerify.Select(elem => learnAboutList.Contains(elem)).ToList().Contains(false);
        }
    }
}