namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.CustomPages.ManagePage;
    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Add tool to custom page dialog
    /// </summary>
    public class AddToolCustomPageDialog : BaseManagePageDialog
    {
        private const string ToolCheckboxLctMask =
            "id('cp_customPageSelectTools')//label[text()[contains(., '{0}')]]/input[@type='checkbox']";

        private EnumPropertyMapper<CustomPageTools, WebElementInfo> toolsMap;

        /// <summary>
        /// Gets the CustomPageTools enumeration to Framework.Common.UI.Products.Shared.Models.EnumProperties.WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<CustomPageTools, WebElementInfo> ToolsMap
            => this.toolsMap = this.toolsMap ?? EnumPropertyModelCache.GetMap<CustomPageTools, WebElementInfo>();

        /// <summary>
        /// This common method adds/removes the widgets to the custom page 
        /// </summary>
        /// <param name="selected"> Check or uncheck tool. </param>
        /// <param name="tools"> List of widgets for adding/removing on Custom Page  </param>
        /// <returns> The <see cref="CustomPage"/>.  </returns>
        public CustomPage SetUpTools(bool selected, params CustomPageTools[] tools)
        {
            tools.ToList().ForEach(tool => DriverExtensions.SetCheckbox(By.XPath(string.Format(ToolCheckboxLctMask, this.ToolsMap[tool].Text)), selected));
            return this.ClickSaveButton<CustomPage>();
        }

        /// <summary>
        /// Verify that certain tool is displayed in the Tools Section
        /// </summary>
        /// <param name="tool">Certain tool</param>
        /// <returns>True if tool is displayed, false otherwise</returns>
        public bool IsToolDisplayed(CustomPageTools tool) =>
            DriverExtensions.IsDisplayed(By.XPath(this.ToolsMap[tool].LocatorString));
    }
}