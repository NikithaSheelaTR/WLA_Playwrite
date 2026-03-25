namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.ProceduralPosture
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// Procedural Posture on-boarding dialog
    /// </summary>
    public class ProceduralPostureOnBoardingDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//div[@id='proceduralPosture_InfoBox_container']//a[@class='co_infoBox_closeButton']");

        private static readonly By OnBoardingMessageLocator = By.XPath("//div[@id='proceduralPosture_InfoBox_container']//div[@class='co_infoBox_message']");

        /// <summary>
        /// Onboarding message label
        /// </summary>
        public ILabel OnboardingMessageLabel => new Label(OnBoardingMessageLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);
    }
}