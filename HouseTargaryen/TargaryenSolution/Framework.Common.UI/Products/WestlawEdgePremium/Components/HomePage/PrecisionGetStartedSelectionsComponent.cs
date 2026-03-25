namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Selections Component
    /// </summary>
    public class PrecisionGetStartedSelectionsComponent : BaseModuleRegressionComponent
    {
        private const string UnselectOptionLctMask = ".//button[@aria-label='Remove {0}']";
        private const string UpArrowLctMask = ".//div[contains(@class, 'PrecisionSearch-selection') and contains(@aria-label, '{0}')]//button[@class='PrecisionSearch-moveUp']";
        private const string DownArrowLctMask = ".//div[contains(@class, 'PrecisionSearch-selection') and contains(@aria-label, '{0}')]//button[@class='PrecisionSearch-moveDown']";

        private static readonly By ContainerLocator = By.XPath("//div[@class='PrecisionSearchModal-selections']");
        private static readonly By ClearAllButtonLocator = By.XPath(".//button[contains(@class, 'clearAll')]");
        private static readonly By SelectionsHeaderTextLocator = By.XPath(".//div[contains(@class, 'PrecisionSearchModal-selectionsHeading')]");
        private static readonly By SelectedItemLocator = By.XPath(".//span[contains(@class,'PrecisionSearch-selection-hasPath')]/span[1]");
        private static readonly By SelectedItemSubTitleLocator = By.XPath("./following-sibling::dd");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Started widget Selections header text label
        /// </summary>
        public ILabel SelectionsHeaderTextLabel => new Label(this.ComponentLocator, SelectionsHeaderTextLocator);

        /// <summary>
        /// Get Started widget ClearAll button
        /// </summary>
        public IButton ClearAllButton => new Button(this.ComponentLocator, ClearAllButtonLocator);

        /// <summary>
        /// Up arrow
        /// </summary>
        public IButton UpArrowButton(string itemToUp) => 
            new Button(this.ComponentLocator, By.XPath(string.Format(UpArrowLctMask, itemToUp)));

        /// <summary>
        /// Down arrow
        /// </summary>
        public IButton DownArrowButton(string itemToDown) => 
            new Button(this.ComponentLocator, By.XPath(string.Format(DownArrowLctMask, itemToDown)));

        /// <summary>
        /// Select Get Started tab's option
        /// </summary>
        /// <param name="option">the checkbox to select</param>
        public void UnselectOptionByName(string option) =>
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(string.Format(UnselectOptionLctMask, option))).Click();

        /// <summary>
        /// Get all selected items 
        /// </summary>
        /// <returns>List of selected items</returns>
        public List<string> GetAllSelectedItems() =>
            DriverExtensions.GetElements(this.ComponentLocator, SelectedItemLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get all selected items sub-titles
        /// </summary>
        /// <returns>List of selected items</returns>
        public List<string> GetAllSelectedItemsSubTitles() =>
            DriverExtensions.GetElements(this.ComponentLocator, SelectedItemLocator, SelectedItemSubTitleLocator).Select(elem => elem.Text).ToList();
    }
}
