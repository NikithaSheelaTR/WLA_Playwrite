namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.BuildAWillContract
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    /// <summary>
    /// Represents a page for building contacts.
    /// </summary>
    public class BuildContractExpressPage : CommonAdvancedSearchPage
    {
        private static readonly By ShowWelcomeCheckBoxInContractExpressWillLocator = By.XPath("//*[@formcontrolname='showWelcomeScreen']//input[@type='checkbox']");

        private static readonly By WelcomeCheckboxLocator = By.ClassName("Welcome-option");

        private static readonly By MyAccountLocator = By.XPath("//button[@data-role='MyAccountBtn']");

        /// <summary>
        /// Click on the MyccountDetailsLink
        /// </summary>
        public ILink MyAccountDetailsLink => new Link(MyAccountLocator);

        /// <summary>
        /// Select WelcomeCheckBox
        /// </summary>
        public ICheckBox WelcomeCheckBox => new CheckBox(WelcomeCheckboxLocator);

        /// <summary>
        /// Header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// Header Dailog
        /// </summary>
        public PreferencesDialog Dialog { get; } = new PreferencesDialog();

        /// <summary>
        /// Check if the checkbox is selected
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsCheckboxSelected() => DriverExtensions.GetElement(ShowWelcomeCheckBoxInContractExpressWillLocator).Selected;

        /// <summary>
        /// Check if the checkbox is not selected.
        /// </summary>
        /// <returns>True if the checkbox is selected; otherwise, false.</returns>
        public bool Clickcheckbox()
        {
            DriverExtensions.WaitForElement(ShowWelcomeCheckBoxInContractExpressWillLocator);
            IWebElement checkboxElement = DriverExtensions.GetElement(ShowWelcomeCheckBoxInContractExpressWillLocator);
            DriverExtensions.GetElement(checkboxElement).Click();
            return checkboxElement.Selected;
        }
    }
}