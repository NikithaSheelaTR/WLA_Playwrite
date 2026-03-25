namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// KeyCite Warnings tab => The KeyCite Warnings feature facet component.
    /// </summary>
    public class KeyCiteWarningsFacetComponent : BaseModuleRegressionComponent
    {
        private const string CheckboxKeyCiteWarningOptionLctMask = "//*[text()='{0}']//preceding-sibling::input[@type='checkbox']";

        private static readonly By ContainerLocator = By.XPath("//div[@class='DA-KCWarningLeftColumn']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Set checkbox.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="warningTitle">
        /// The warning Title.
        /// </param>
        public void SetCheckbox(bool action, string warningTitle)
        {
            DriverExtensions.GetElement(By.XPath(string.Format(CheckboxKeyCiteWarningOptionLctMask, warningTitle))).SetCheckbox(action);
            DriverExtensions.WaitForJavaScript();
        }
    }
}