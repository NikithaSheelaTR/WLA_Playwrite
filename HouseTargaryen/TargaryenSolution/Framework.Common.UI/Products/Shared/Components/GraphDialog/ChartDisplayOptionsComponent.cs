
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.Shared.Components.GraphDialog
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Graphs Dialog's' display options component
    /// </summary>
    public class ChartDisplayOptionsComponent : BaseModuleRegressionComponent
    {
        private const string ContainerLctMask = "//div[@class='graphicalSideBox' and .//label[text()='{0}']]";

        private const string ButtonsOnOffLctMask = ".//a[text() = '{0}']";

        private static readonly By OptionNameLocator = By.XPath("./h3");

        private readonly By containerLocator;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.containerLocator;

        /// <summary>
        /// Graphs Dialog's' display options component
        /// </summary>
        public ChartDisplayOptionsComponent(string option)
        {
            this.containerLocator = By.XPath(string.Format(ContainerLctMask, option));
        }

        /// <summary>
        /// Verify if option is selected
        /// </summary>
        public bool IsDisabled() =>
            DriverExtensions.GetElement(this.containerLocator, OptionNameLocator)
                             .GetAttribute("class").Contains("co_disabled");

        /// <summary>
        /// Verify if option is selected
        /// </summary>
        public bool IsSelected(string option) =>
            !DriverExtensions.GetElement(this.containerLocator, By.XPath(string.Format(ButtonsOnOffLctMask, option)))
                             .GetAttribute("class").Contains("inactive");

        /// <summary>
        /// Sets option
        /// </summary>
        public void Select(string option) =>
            DriverExtensions.GetElement(this.containerLocator, By.XPath(string.Format(ButtonsOnOffLctMask, option)))
                            .Click();
    }
}
