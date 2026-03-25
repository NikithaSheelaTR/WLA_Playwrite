namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.ItDepends
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Select Dialog (Jurisdiction and Multi-Factor test dialogs)
    /// </summary>
    public class ItDependsSelectDialog : BaseModuleRegressionDialog
    {
        private const string RadiobuttonLctMask = "//label[contains(.,'{0}')]/input";

        private static readonly By SaveJurisdictionButtonLocator = By.XPath("//*[@id='coid_itDependsConfirmButton']");

        /// <summary>
        /// Select radiobutton 
        /// </summary>
        /// <param name="itemValue">
        /// The item Value.
        /// </param>
        public void SelectItemByValue(string itemValue)
        {
            this.ClickElement(By.XPath(string.Format(RadiobuttonLctMask, itemValue)));
            this.ClickElement(SaveJurisdictionButtonLocator);
        }
    }
}
