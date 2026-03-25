namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Industry type items dialog
    /// </summary>
    public class PrecisionIndustryTypeItemsDialog : BaseModuleRegressionDialog
    {
        private const string IndustryTypeCheckBoxLctMask = ".//input[@value='{0}']";

        private static readonly By DialogContainerLocator = By.XPath("//button[@id='coid_IndustryTypeDropdown']//following-sibling::ul");        

        /// <summary>
        /// Industry type checkbox
        /// </summary>
        public ICheckBox IndustryTypeCheckBox(string itemName) => new CheckBox(DialogContainerLocator, By.XPath(string.Format(IndustryTypeCheckBoxLctMask, itemName)));
    }
}
