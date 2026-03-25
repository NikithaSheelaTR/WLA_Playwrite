namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.MultipleSearchWithin
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium; 

    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Class models the fly-out that is displayed to contain multiple Search Within terms present in
    /// the Term Navigation widget
    /// </summary>
    public class SearchWithinFlyoutDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContinueButtonLocator = By.Id("co_mswFlyoutContinueButton");

        private static readonly By SelectAllQueriesCheckboxLocator = By.Id("co_mswSelectAllQueries");

        private static readonly By QueryCheckboxLocator = By.XPath($"//*[@id='mswSearchTerm-{'0'}']");

        /// <summary>
        /// Continue button
        /// </summary>
        public IButton ContinueButton => new Button(this.Container, ContinueButtonLocator);

        /// <summary>
        /// Select All Queries checkbox
        /// </summary>
        public ICheckBox SelectAllCheckBox => new CheckBox(this.Container, SelectAllQueriesCheckboxLocator);

        private IWebElement Container => DriverExtensions.WaitForElement(By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.SearchWithinFlyoutDialog].LocatorString));

        /// <summary>
        /// Toggle checkbox for individual Search Within query in the fly-out
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ICheckBox QueryCheckBox(string index) => new CheckBox(this.Container, QueryCheckboxLocator);
 
        /// <summary>
        /// Selects a checkbox for specified query in the fly-out
        /// </summary>
        /// <param name="query"></param>
        public void SelectCheckbox(string query) => DriverExtensions.SetCheckbox(true, query);
    }
}
