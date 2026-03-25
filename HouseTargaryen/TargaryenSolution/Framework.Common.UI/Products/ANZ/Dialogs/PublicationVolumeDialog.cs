namespace Framework.Common.UI.Products.ANZ.Dialogs
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Publication Volume Dialog for Publication Volume Facet
    /// </summary>
    public class PublicationVolumeDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string AvailableItemLctMask = ".//span[contains(text(), {0})]";

        private static readonly By AllAvailableOptions = By.XPath(".//ul[@id='co_facet_publicationVolume_availableOptions']//a/span[1]");     

        private static readonly By ContainerLocator = By.CssSelector(
            "#co_facet_PublicationName_popup, #co_facet_publicationVolume_popup, #co_facet_aunzFacetViewSetPublicationNameFacet_popup");

        /// <summary>
        /// Publication Volume Options List
        /// </summary>
        public IReadOnlyCollection<ILabel> PublicationVolumeAllAvailableOptions => new ElementsCollection<Label>(this.Container, AllAvailableOptions);

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container =>
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Selects an option on the popup
        /// </summary>
        /// <param name="item"> item to select </param>
        public void SelectOption(string item)
            => DriverExtensions.GetElement(this.Container, SafeXpath.BySafeXpath(AvailableItemLctMask, item)).Click();
    }
}
