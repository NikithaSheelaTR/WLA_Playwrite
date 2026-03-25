namespace Framework.Common.UI.Products.Shared.Dialogs.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.Items.Facets;
    using Framework.Common.UI.Products.Shared.Models;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Publication Name Dialog for Publication Name Facet
    /// </summary>
    public class PublicationNameDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string AvailableItemLctMask = ".//label[contains(normalize-space(.), {0})]";

        private static readonly By AvailableOptionsLocator = By.XPath(".//ul[contains(@id,'availableOptions')]");

        private static readonly By ContainerLocator = By.CssSelector(
            "#co_facet_PublicationName_popup, #co_facet_publication_popup, #co_facet_aunzFacetViewSetPublicationNameFacet_popup, #co_facet_publicationName_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container =>
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Get all the options in the dialog
        /// </summary>
        /// <returns> Dialog items list </returns>
        public List<DialogOptionItemModel> GetAllDialogOptions() =>
            this.GetDialogOptions().Select(el => new DialogOptionItem(el).ToModel<DialogOptionItemModel>()).ToList();

        /// <summary>
        /// Click on the dialog option link
        /// </summary>
        /// <param name="option">The option.</param>
        public void ClickDialogOption(string option)
        {
            this.GetDialogOptions().FirstOrDefault(el => new DialogOptionItem(el).ToModel<DialogOptionItemModel>().Name.Equals(option)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Gets count for all displayed options
        /// </summary>
        /// <returns>count</returns>
        public int GetCountForAllDisplayedOptions()
            => this.GetDialogOptions().ToList().Count;

        /// <summary>
        /// Selects an option on the popup
        /// </summary>
        /// <param name="item"> item to select </param>
        public void SelectOption(string item) 
            => DriverExtensions.GetElement(this.Container, SafeXpath.BySafeXpath(AvailableItemLctMask, item)).Click();

        /// <summary>
        /// Check whether the option is selected or not 
        /// </summary>
        /// <param name="item"> item to select </param>
        public bool IsOptionSelected(string item)
           => bool.Parse(DriverExtensions.GetElement(this.Container, SafeXpath.BySafeXpath(AvailableItemLctMask, item)).GetAttribute("aria-selected"));

        private IEnumerable<IWebElement> GetDialogOptions()
            => DriverExtensions.GetElements(this.Container, AvailableOptionsLocator, By.XPath(".//li"));
    }
}