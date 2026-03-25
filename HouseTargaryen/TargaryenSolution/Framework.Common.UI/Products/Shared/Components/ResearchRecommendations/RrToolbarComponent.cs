namespace Framework.Common.UI.Products.Shared.Components.ResearchRecommendations
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Research Recommendations toolbar component
    /// </summary>
    public class RrToolbarComponent : BaseModuleRegressionComponent
    {
        private static readonly By DetailDropdownLocator = By.XPath(".//div[contains(@id, 'DetailSliderTab')]");

        private static readonly By DeliveryDropdownLocator = By.XPath(".//*[contains(@id, 'deliveryWidget')]");

        private readonly By componentLocator;

        private EnumPropertyMapper<ToolbarElements, WebElementInfo> researchRecommendationsToolbarMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="RrToolbarComponent"/> class. 
        /// </summary>
        /// <param name="componentElement"> container </param>
        public RrToolbarComponent(By componentElement)
        {
            this.componentLocator = componentElement;
        }
   
        /// <summary>
        /// The delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(new ByChained(this.ComponentLocator, DeliveryDropdownLocator));

        /// <summary>
        /// The detail dropdown
        /// </summary>
        public DetailDropdown DetailDropdown => new DetailDropdown(DriverExtensions.GetElement(this.ComponentLocator, DetailDropdownLocator).GetCssLocator());

        /// <summary>
        /// Research Recommendations Toolbar Options Map
        /// </summary>
        protected EnumPropertyMapper<ToolbarElements, WebElementInfo> RrToolbarMap
            => this.researchRecommendationsToolbarMap = this.researchRecommendationsToolbarMap ?? EnumPropertyModelCache.GetMap<ToolbarElements, WebElementInfo>("Rr");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.componentLocator;

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <typeparam name="T">Page returned from clicking the button.</typeparam>
        /// <param name="toolbarElement">Element to click on the toolbar.</param>
        /// <returns>The expected page.</returns>
        public T ClickToolbarElement<T>(ToolbarElements toolbarElement) where T : ICreatablePageObject
        {
            this.ClickToolbarElement(toolbarElement);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the toolbar button.
        /// </summary>
        /// <param name="toolbarElement"> Toolbar option to click. </param>
        public void ClickToolbarElement(ToolbarElements toolbarElement)
        {
            IWebElement elementToClick = DriverExtensions.GetElements(this.ComponentLocator, By.XPath(this.RrToolbarMap[toolbarElement].LocatorString)).FirstOrDefault(i => i.IsDisplayed());        
            elementToClick.CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify that toolbar option is displayed
        /// </summary>
        /// <param name="toolbarElement"> Option to verify </param>
        /// <returns> True if the toolbar option is visible, false otherwise </returns>
        public bool IsToolbarElementDisplayed(ToolbarElements toolbarElement)
            => DriverExtensions.IsDisplayed(this.ComponentLocator, By.XPath(this.RrToolbarMap[toolbarElement].LocatorString));
    }
}
