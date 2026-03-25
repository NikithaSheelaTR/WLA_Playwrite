namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for all Hierarchy Facet with Select link(Key Number, Topic, Contacts facets)
    /// </summary>
    public class EdgeBaseFacetWithAppearingDialogComponent : EdgeBaseFacetComponent
    {
        private static readonly By AppliedFacetItemListLocator = By.XPath(".//*[@class='co_facet_applied'] | .//fieldset[@class = 'SearchFacet-fieldset']");

        private static readonly By AppliedFacetItemLocator = By.XPath(".//li | .//div[@class='SearchFacet-listItem' or @role='listitem']");

        private static readonly By BreadcrumbLocator = By.XPath(".//span[@class='SearchFacet-breadcrumbText']");

        private static readonly By FacetOptionsLocator = By.CssSelector("span.SearchFacet-labelText");

        private static readonly By MoreInfoLocator = By.CssSelector("button.co_moreInfoBlock.co_moreInfo");

        private static readonly By ToolTipLocator = By.XPath("//div[@aria-hidden='false']/div[contains(@class,'Tooltip-pointer')]/following-sibling::div");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeBaseFacetWithAppearingDialogComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        /// <param name="additionalInfo">
        /// The additional Info.
        /// </param>
        public EdgeBaseFacetWithAppearingDialogComponent(By componentLocator, string additionalInfo = "")
        {
            this.componentLocator = componentLocator;
            this.AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// More info icon
        /// </summary>
        public IButton MoreInfoIcon => new Button(this.ComponentLocator, MoreInfoLocator);
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        /// <summary>
        /// Additional info
        /// </summary>
        private string AdditionalInfo { get; }

        /// <summary>
        /// Sets checkbox by name for single mode
        /// </summary>
        /// <typeparam name="T"> T page  </typeparam>
        /// <param name="state"> state  </param>
        /// <param name="itemName"> Item Name </param>
        /// <returns> page  </returns>
        public T SetCheckboxForAppliedFacetByName<T>(bool state, string itemName) where T : ICreatablePageObject
        {
            this.GetAppliedItemByName(itemName).SetCheckbox(state);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="itemName">The name of item </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected(string itemName)
            => this.GetAllAppliedFacetItems().First(item => item.Title.Equals(itemName)).IsSelected;

        /// <summary>
        /// Is Expand facet arrow exists
        /// </summary>
        public bool DoesExpandFacetArrowExist() => this.ExpandButton.GetAttribute("class").Contains("icon_rightCaret");

        /// <summary>
        /// Click on facet to open a dialog
        /// </summary>
        /// <typeparam name="T">
        /// Dialog to be returned
        /// </typeparam>
        /// <returns>
        /// Some dialog 
        /// </returns>
        public T ExpandFacetToOpenDialog<T>() where T : BaseModuleRegressionDialog
        {
            this.ExpandFacet();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that the applied section is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the select link is displayed. </returns>
        public bool IsAppliedSectionDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, AppliedFacetItemListLocator);

        /// <summary>
        /// Get all applied items titles
        /// </summary>
        /// <returns>Applied items titles</returns>
        public IEnumerable<string> GetAllAppliedItemsTitles() => this.GetAllAppliedFacetItems().Select(item => item.Title);

        /// <summary>
        /// Get breadcrumbs titles
        /// </summary>
        /// <returns>Breadcrumbs title</returns>
        public IEnumerable<string> GetBreadcrumbsTitles() => DriverExtensions.GetElements(this.ComponentLocator, BreadcrumbLocator).Select(el => el.Text);

        /// <summary>
        /// Gets list of options in facet
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<string> GetOptionsInFacet() =>
            DriverExtensions.GetElements(FacetOptionsLocator).Select(t => t.Text).ToList();

        /// <summary>
        /// Get count for item in facet by name
        /// </summary>
        /// <param name="itemName"> Item Name </param>
        /// <returns> page  </returns>
        public int GetAppliedItemCountByName(string itemName) =>
            this.GetAppliedItemByName(itemName).Count;

        /// <summary>
        /// 
        /// </summary>
        public ILabel FacetTooltip => new Label(this.ComponentLocator, ToolTipLocator);

        /// <summary>
        /// Get applied item by name
        /// </summary>
        /// <param name="itemName">The name of item to return</param>
        /// <returns>Facet option item</returns>
        protected FacetOptionItem GetAppliedItemByName(string itemName) =>
            this.GetAllAppliedFacetItems().First(item => item.Title.Equals(itemName));

        /// <summary>
        /// Get all applied facet items 
        /// </summary>
        /// <returns>List of jurisdiction items</returns>
        protected List<FacetOptionItem> GetAllAppliedFacetItems() =>
         DriverExtensions.GetElements(this.ComponentLocator, AppliedFacetItemLocator)
                .ToList().Select(el => new FacetOptionItem(el, this.AdditionalInfo)).ToList();
    }
}
