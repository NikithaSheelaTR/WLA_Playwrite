namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// The dialog that comes when you click the Select Alert Groups link 
    /// </summary>
    public class AlertGroupsSelectFilterDialog : BaseModuleRegressionDialog
    {
        private const string GroupCheckBoxLctMask = "//input[@data-path ={0}]";

        private static readonly By FilterButtonLocator = By.Id("co_alerts_groupsFilter");

        /// <summary>
        /// Filter button
        /// </summary>
        public IButton FilterButton => new Button(FilterButtonLocator);

        /// <summary>
        /// Select All Content Checkbox
        /// </summary>
        public ICheckBox GroupCheckbox(string groupName) => new CheckBox(SafeXpath.BySafeXpath(GroupCheckBoxLctMask, groupName));
    }
}