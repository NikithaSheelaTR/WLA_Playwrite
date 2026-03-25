namespace Framework.Common.UI.Products.WestlawAdvantage.Dialogs.HomePageLeftToolsBar
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Select Default Tab Dialog
    /// </summary>
    public class AdvantageSelectDefaultTabDialog : SelectDefaultTabDialog
    {
        /// <summary>
        /// Default options map
        /// </summary>
        protected EnumPropertyMapper<AdvantageSelectDefaultTabOptions, WebElementInfo> SelectDefaultTabMap =>
            EnumPropertyModelCache.GetMap<AdvantageSelectDefaultTabOptions, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawAdvantage");

        /// <summary>
        /// Select Default tab
        /// </summary>
        /// <param name="option"></param>
        public void SelectDefaultTab(AdvantageSelectDefaultTabOptions option) =>
            DriverExtensions.WaitForElement(By.Id(this.SelectDefaultTabMap[option].Id)).Click();
    }
}
