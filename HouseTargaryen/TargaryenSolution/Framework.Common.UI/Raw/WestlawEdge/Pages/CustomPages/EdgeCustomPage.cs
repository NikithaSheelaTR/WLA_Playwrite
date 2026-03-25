namespace Framework.Common.UI.Raw.WestlawEdge.Pages.CustomPages
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Custom page
    /// </summary>
    public class EdgeCustomPage : CustomPage
    {
        private const string ToolCheckboxLctMask =
            "id('cp_customPageSelectTools')//label[text()[contains(., '{0}')]]/input[@type='checkbox']";

        private static readonly By ManagePageDropdownButtonLocator = By.Id("cp_dropdown_button");

        private static readonly By ToolsSectionButtonLocator = By.Id("cp_addToolsSection_button");

        private static readonly By ToolsLightboxSaveButtonLocator = By.Id("co_CustomPages_SaveTools");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// This common method adds the widgets to the custom page 
        /// </summary>
        /// <param name="tools"> List of widgets for adding on Custom Page </param>
        /// <returns> The <see cref="EdgeCustomPage"/>. </returns>
        public EdgeCustomPage AddTools(params CustomPageTools[] tools) => this.SetUpTools(true, tools);

        /// <summary>
        /// This common method adds/removes the widgets to the custom page 
        /// </summary>
        /// <param name="selected"> Check or uncheck tool. </param>
        /// <param name="tools"> List of widgets for adding/removing on Custom Page  </param>
        /// <returns> The <see cref="EdgeCustomPage"/>.  </returns>
        public EdgeCustomPage SetUpTools(bool selected, params CustomPageTools[] tools)
        {
            DriverExtensions.WaitForElement(ManagePageDropdownButtonLocator).Click();
            DriverExtensions.WaitForElement(ToolsSectionButtonLocator).Click();
            tools.ToList().ForEach(tool => DriverExtensions.SetCheckbox(By.XPath(string.Format(ToolCheckboxLctMask, this.ToolsMap[tool].Text)), selected));
            DriverExtensions.WaitForElement(ToolsLightboxSaveButtonLocator).Click();
            DriverExtensions.WaitForElementNotPresent(ToolsLightboxSaveButtonLocator);
            return new EdgeCustomPage();
        }
    }
}