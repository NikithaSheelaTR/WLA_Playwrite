namespace Framework.Common.UI.Products.WestlawEdge.Components.ItDepends
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.WestlawEdge.Dialogs.ItDepends;

    /// <summary>
    /// Jurisdiction Component
    /// </summary>
    public class ItDependsJurisdictionComponent : BaseModuleRegressionComponent
    {
        private static readonly By SelectJurisdictionButtonLocator = By.XPath("//section[@id='co_jurusdictionsContent']//button");
        private static readonly By SelectedJurisdictionLocator = By.XPath("//section[contains(@id,'jurusdiction')]//div[@id='co_itDepends_selectedValue']");

        private static readonly By ContainerLocator = By.Id("co_jurusdictionsContent");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Jurisdiction button
        /// </summary>
        /// <returns>
        /// The <see cref="ItDependsSelectDialog"/>.
        /// </returns>
        public ItDependsSelectDialog ClickSelectJurisdictionButton()
        {
            DriverExtensions.Click(SelectJurisdictionButtonLocator);
            return new ItDependsSelectDialog();
        }

        /// <summary>
        /// Get Selected jurisdiction
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSelectedJurisdictionText() => DriverExtensions.GetText(SelectedJurisdictionLocator);
    }
}
