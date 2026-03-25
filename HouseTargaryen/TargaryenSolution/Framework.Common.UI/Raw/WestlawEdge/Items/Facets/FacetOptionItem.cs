namespace Framework.Common.UI.Raw.WestlawEdge.Items.Facets
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Common facet item
    /// </summary>
    public class FacetOptionItem : BaseItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FacetOptionItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        /// <param name="additionalInfo">
        /// The additional Info.
        /// </param>
        public FacetOptionItem(IWebElement containerElement, string additionalInfo = "")
            : base(containerElement)
        {
            this.AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Title of item
        /// </summary>
        public string Title => DriverExtensions.GetElement(
                                                   this.Container,
                                                   By.XPath(this.FieldsMap[FacetOptionFields.Title].LocatorString)).GetText();

        /// <summary>
        /// Count of item
        /// </summary>
        public int Count =>
            DriverExtensions.SafeGetElement(
                this.Container,
                By.XPath(this.FieldsMap[FacetOptionFields.Count].LocatorString)) == null
                ? -1
                : DriverExtensions.SafeGetElement(
                    this.Container,
                    By.XPath(this.FieldsMap[FacetOptionFields.Count].LocatorString)).GetText().ConvertCountToInt();


        /// <summary>
        /// Is checkbox displayed
        /// </summary>
        public bool IsDisplayed => DriverExtensions.IsDisplayed(this.Container, By.XPath(this.FieldsMap[FacetOptionFields.Selector].LocatorString));

        /// <summary>
        /// The is enabled.
        /// </summary>
        public bool IsEnabled => DriverExtensions.GetElement(
                                                     this.Container,
                                                     By.XPath(this.FieldsMap[FacetOptionFields.Selector].LocatorString)).Enabled;

        /// <summary>
        /// Is checkbox selected
        /// </summary>
        public bool IsSelected => DriverExtensions.IsCheckboxSelected(
            DriverExtensions.GetElement(
                this.Container,
                By.XPath(this.FieldsMap[FacetOptionFields.Selector].LocatorString)));

        /// <summary>
        /// The fields mapper initialization.
        /// </summary>
        protected EnumPropertyMapper<FacetOptionFields, WebElementInfo> FieldsMap =>
            EnumPropertyModelCache.GetMap<FacetOptionFields, WebElementInfo>(this.AdditionalInfo, "Resources/EnumPropertyMaps/WestlawEdge/Facets/Items");

        /// <summary>
        /// Gets the additional info.
        /// </summary>
        private string AdditionalInfo { get; }

        /// <summary>
        /// The set checkbox
        /// </summary>
        /// <param name="action"> The action. </param>
        public void SetCheckbox(bool action) => DriverExtensions
            .GetElement(this.Container, By.XPath(this.FieldsMap[FacetOptionFields.Selector].LocatorString))
            .SetCheckbox(action);

        /// <summary>
        /// Gets the document tooltip.
        /// </summary>
        /// <returns>tooltip message</returns>
        public virtual string GetTooltip() => DriverExtensions.GetElement(
            this.Container,
            By.XPath(this.FieldsMap[FacetOptionFields.Title].LocatorString)).GetAttribute("title");
    }
}