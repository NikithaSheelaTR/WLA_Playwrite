namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// HeadnoteTopicsFacet
    /// </summary>
    public class HeadnoteTopicsFacetComponent : BaseFacetCheckboxComponent
    {
        private const string CheckboxNameLctMask = ".//a[contains(text(),'{0}') and not (@class='co_headnoteTopicSpecify')]";

        private const string SpecifyLctMask = ".//a[@class='co_headnoteTopicSpecify' and (contains(text(),'{0}'))]";

        private static readonly By ListItemLocator = By.XPath(".//a[contains(text(),'') and not (@class='co_headnoteTopicSpecify')]");

        private static readonly By ContainerLocator = By.Id("facet_div_HeadnoteTopics");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify that the given facet is of the checkbox type
        /// </summary>
        /// <param name="checkboxName">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckbox(string checkboxName)
            => this.IsCheckbox(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxNameLctMask, checkboxName))));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkboxName">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(string checkboxName, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxNameLctMask, checkboxName))), setTo);

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="checkboxName">The locator.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public new int GetCheckboxCount(string checkboxName)
            => this.GetCheckboxText(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxNameLctMask, checkboxName))))
                   .RetrieveCountFromBrackets();

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="checkboxName">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(string checkboxName)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(CheckboxNameLctMask, checkboxName))));

        /// <summary>
        /// Click Specify link of the specific checkbox
        /// </summary>
        /// <param name="checkboxName">The checkbox.</param>
        /// <returns>The <see cref="HeadnoteTopicsDialog"/>.</returns>
        public HeadnoteTopicsDialog ClickSpecifyLink(string checkboxName)
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), By.XPath(string.Format(SpecifyLctMask, checkboxName))).Click();
            return new HeadnoteTopicsDialog();
        }

        /// <summary>
        /// Get the list of all checkboxes
        /// </summary>
        /// <returns>The list of strings</returns>
        public List<string> GetItemsList()
            => DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator, ListItemLocator))
                               .Select(checkbox => this.GetCheckboxText(checkbox).RetainText()).ToList();

        /// <summary>
        /// IsSpecifyLinkDisplayed
        /// </summary>
        /// <param name="checkboxName">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSpecifyLinkDisplayed(string checkboxName)
            => DriverExtensions.IsDisplayed(this.ComponentLocator, By.XPath(string.Format(SpecifyLctMask, checkboxName)));
    }
}