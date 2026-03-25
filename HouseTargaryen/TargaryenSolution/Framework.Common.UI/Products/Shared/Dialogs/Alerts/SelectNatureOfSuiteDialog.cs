namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Select Nature Of Suite Dialog in Docket and Court Wire alerts
    /// </summary>
    public class SelectNatureOfSuiteDialog : BaseModuleRegressionDialog
    {
        private const string AddButtonLocator = "//div[contains(@class,'co_nos_wizardStep_left_tab') and contains(@class,'co_3Column')]//ul/li//i[contains(@title, {0})]";
        private const string NosCodeLocator = "//div[contains(@class,'co_nos_wizardStep_left_tab') and contains(@class,'co_3Column')]//ul/li[contains(@title, {0})]";
        private const string SelectedNatureOfSuiteLocator = "(//div[contains(@class,'co_nos_wizardStep_left_tab') and contains(@class,'co_3Column')]//ul/li//i[@class = 'co_checked'])[{0}]";

        private static readonly By SaveButtonLocator = By.Id("co_search_alertSmartTermsSave");

        /// <summary>
        /// Save button
        /// </summary>
        public IButton SaveButton => new Button(SaveButtonLocator);

        /// <summary>
        /// Add button
        /// </summary>
        public IButton AddButton(string code) => new Button(SafeXpath.BySafeXpath(AddButtonLocator, code));

        /// <summary>
        /// Nos code label
        /// </summary>
        public ILabel NosCodeLabel(string code) => new Label(SafeXpath.BySafeXpath(NosCodeLocator, code));

        /// <summary>
        /// Selected nature of suite label
        /// </summary>
        public ILabel SelectedNatureOfSuiteLabel(string code) => new Label(SafeXpath.BySafeXpath(SelectedNatureOfSuiteLocator, code));
    }
}
