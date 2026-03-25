namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Header
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// Canada Tools dialog
    /// </summary>
    public class CanadaToolsDialog : BaseModuleRegressionDialog
    {
        private static readonly string ToolImageLocator = "//span[text()='{0}']/preceding-sibling::div/span";

        private EnumPropertyMapper<HomePageTools, WebElementInfo> toolIconMap;

        /// <summary>
        /// ContentTypes Mapper
        /// </summary>
        protected EnumPropertyMapper<HomePageTools, WebElementInfo> CanadaToolMap =>
            this.toolIconMap = this.toolIconMap ?? EnumPropertyModelCache.GetMap<HomePageTools, WebElementInfo>(
                                            string.Empty,
                                            @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Gets the Tool Icon from Tools Dialog
        /// </summary>
        /// <returns> The Tool icon from Dialog </returns>
        public HomePageTools GetHomePageTool(HomePageTools flag)
        {
            if (DriverExtensions.IsDisplayed(By.XPath(string.Format(ToolImageLocator, CanadaToolMap[flag].Text))))
            {
                string flagClass = DriverExtensions.GetElement(By.XPath(string.Format(ToolImageLocator, CanadaToolMap[flag].Text))).GetAttribute("class");

                return CanadaToolMap.FirstOrDefault(
                    map => !string.IsNullOrEmpty(map.Value.LocatorString)
                           && map.Value.LocatorString.Contains(flagClass)).Key;
            }

            return HomePageTools.NoTool;
        }

        /// <summary>
        /// Click on a Flag in the doc.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public T ClickToolIcon<T>(HomePageTools flag) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(this.CanadaToolMap[flag].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}