namespace Framework.Common.UI.Products.Shared.Dialogs.ResearchRecommendations
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Reset dialog.
    /// </summary>
    public class ResetDialog : BaseModuleRegressionDialog
    {
        private const string HeaderTitle = "Reset";

        private const string BodyText =
            "Research Recommendations will ignore all previous document interactions from this session "
            + "and start generating new recommendations based on the documents you interact with from here forward.";

        private static readonly By CancelButtonLocator = By.Id("co_ra_hideResetWarning");

        private static readonly By ResetButtonLocator = By.Id("co_ra_resetRecommendations");

        private static readonly By BodyLocator = By.XPath("//div[@id='coid_turnOffRXWarningLightBox']//div[@class='co_overlayBox_content']/div");

        private static readonly By HeaderLocator = By.XPath("//div[@id='coid_turnOffRXWarningLightBox']//h3 | //div[@id='coid_turnOffRXWarningLightBox']//h2");

        private static readonly By ResetDialogLocator = By.Id("coid_turnOffRXWarningLightBox");

        /// <summary>
        /// Initializes a new instance of the Reset dialog class. 
        /// </summary>
        public ResetDialog() => DriverExtensions.WaitForElementDisplayed(ResetDialogLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Reset button
        /// </summary>
        public IButton ResetButton => new Button(ResetButtonLocator);

        /// <summary>
        /// Header label
        /// </summary>
        public ILabel HeaderLabel => new Label(HeaderLocator);

        /// <summary>
        /// Body label
        /// </summary>
        public ILabel BodyLabel => new Label(BodyLocator);

        /// <summary>
        /// Verify header 'Reset' and body 
        /// 'Research Recommendations will ignore all previous document interactions from this session and start generating new recommendations based on the documents you interact with from here forward.'
        /// are displayed
        /// </summary>
        /// <returns>true if header and body are displayed with expected text, false otherwise</returns>
        public bool IsHeaderAndBodyDisplayed() =>
            this.HeaderLabel.Displayed 
            && this.BodyLabel.Displayed
            && HeaderLabel.Text == HeaderTitle
            && BodyLabel.Text == BodyText;
    }
}