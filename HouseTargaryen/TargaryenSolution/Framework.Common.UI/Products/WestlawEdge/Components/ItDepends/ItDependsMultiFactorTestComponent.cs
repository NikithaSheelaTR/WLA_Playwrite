namespace Framework.Common.UI.Products.WestlawEdge.Components.ItDepends
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using Framework.Common.UI.Products.WestlawEdge.Dialogs.ItDepends;

    /// <summary>
    /// 'Multi-Factor Test' Component
    /// </summary>
    public class ItDependsMultiFactorTestComponent : BaseModuleRegressionDialog
    {
        private static readonly By MultiFactorTestButtonLocator = By.XPath("//section[@id='co_issuesContent']//button");
        private static readonly By MultiFactorTestNameLocator = By.Id("co_itDepends_selectedValue");

        /// <summary>
        /// Click 'Multi-Factor Test' button
        /// </summary>
        /// <returns>
        /// The <see cref="ItDependsSelectDialog"/>.
        /// </returns>
        public ItDependsSelectDialog ClickMultiFactorTestButton()
        {
            DriverExtensions.Click(MultiFactorTestButtonLocator);
            return new ItDependsSelectDialog();
        }

        /// <summary>
        /// Get 'Multi-Factor Test' name
        /// </summary>
        /// <returns>Test Name</returns>
        public string GetMultiFactorTestText() => DriverExtensions.GetText(MultiFactorTestNameLocator);
    }
}
