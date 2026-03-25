namespace Framework.Common.UI.Products.Shared.Dialogs.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// Dialog that pops up when you have the welcome screen preference on
    /// </summary>
    public class WelcomeDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//input[@value='Close']");

        private static readonly By DoNotShowCheckboxLocator = By.Id("welcomePrefCheckbox");

        private static readonly By GuideLinkLocator = By.ClassName("co_welcomeGuide");

        private static readonly By HelpLinkLocator = By.ClassName("co_welcomeHelpCenter");

        /// <summary>
        /// Do not show checkbox
        /// </summary>
        public ICheckBox DoNotShowCheckbox => new CheckBox(DoNotShowCheckboxLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Guide link
        /// </summary>
        public ILink GuideLink => new Link(GuideLinkLocator);

        /// <summary>
        /// Help link
        /// </summary>
        public ILink HelpLink => new Link(HelpLinkLocator);
    }
}