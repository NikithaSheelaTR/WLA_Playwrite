namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Select Default Tab Dialog
    /// </summary>
    public class SelectDefaultTabDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.XPath("//button[.='Cancel']");

        private static readonly By CloseXButtonLocator = By.XPath("//button[.='Close']");

        private static readonly By SaveButtonLocator = By.XPath("//button[.='Save']");

        /// <summary>
        /// Default options map
        /// </summary>
        protected EnumPropertyMapper<SelectDefaultTabOptions, WebElementInfo> SelectTabMap =>
            EnumPropertyModelCache.GetMap<SelectDefaultTabOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/HomePage");

        /// <summary>
        /// Cancel Button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Close X Button
        /// </summary>
        public IButton CloseXButton => new Button(CloseXButtonLocator);

        /// <summary>
        /// Save Button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Select Default tab
        /// </summary>
        /// <param name="option"></param>
        public void SelectDefaultTab(SelectDefaultTabOptions option) =>
            DriverExtensions.WaitForElement(By.Id(this.SelectTabMap[option].Id)).Click();
    }
}
