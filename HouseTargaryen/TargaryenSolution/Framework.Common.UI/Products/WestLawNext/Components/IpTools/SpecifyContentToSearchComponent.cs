namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using System.ComponentModel;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Select Content For Search Component (Intellectual property pages)
    /// </summary>
    public class SpecifyContentToSearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = 
            By.CssSelector("#coid_browseShowHideCheckboxesContainer, .co_browseShowHideCheckboxesContainer");

        private static readonly By SpecifyContentToSearchCheckboxLocator =
            By.CssSelector("input.co_browseShowCheckboxes, input#coid_browseShowCheckboxes");

        private static readonly By SelectAllContentCheckboxLocator = 
            By.CssSelector("input.co_browseSelectAllCheckboxInput, input#coid_browseSelectAllCheckboxInput");

        private static readonly By ItemsSelectedCountLocator = By.Id("coid_selectedCount");

        private static readonly By ItemsSelectedLabelLocator = By.Id("coid_selectedCountLabel");

        private static readonly By ClearSelectionLinkLocator =
            By.CssSelector("a#co_itemsClearAnchor, a.co_itemsClearAnchor");

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Element container
        /// </summary>
        protected IWebElement Container { get; set; }

        /// <summary>
        /// Select Content For Search Component constructor
        /// </summary>
        public SpecifyContentToSearchComponent()
        {
            this.Container = DriverExtensions.GetElements(ContainerLocator).FirstOrDefault(x => x.Displayed);
        }

        /// <summary>
        /// Set Specify Content To Search Checkbox
        /// </summary>
        /// <param name="setTo">Required checkbox state</param>
        public SpecifyContentToSearchComponent SetSpecifyContentToSearchCheckbox(bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, this.Container, SpecifyContentToSearchCheckboxLocator);
            return this;
        }

        /// <summary>
        /// Set Select All Content Checkbox
        /// </summary>
        /// <param name="setTo">Required checkbox state</param>
        public SpecifyContentToSearchComponent SetSelectAllContentCheckbox(bool setTo)
        {
            DriverExtensions.SetCheckbox(setTo, this.Container, SelectAllContentCheckboxLocator);
            return this;
        }

        /// <summary>
        /// Get Count Of Selected Items
        /// </summary>
        /// <returns>items selected count</returns>
        public int GetCountOfSelectedItems() =>
            DriverExtensions.GetText(ItemsSelectedCountLocator, this.Container, 30).ConvertCountToInt();

        /// <summary>
        /// Get Count Of Selected Items Label
        /// </summary>
        /// <returns>items selected count label text</returns>
        public string GetCountOfSelectedItemsLabel() => DriverExtensions.GetText(ItemsSelectedLabelLocator, this.Container, 30);

        /// <summary>
        /// Click Clear Selection Link
        /// </summary>
        /// <returns></returns>
        public SpecifyContentToSearchComponent ClickClearSelectionLink() 
        {
            DriverExtensions.Click(this.Container, ClearSelectionLinkLocator);
            return this;
        }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public override bool IsDisplayed() => this.Container.Displayed;
    }
}
