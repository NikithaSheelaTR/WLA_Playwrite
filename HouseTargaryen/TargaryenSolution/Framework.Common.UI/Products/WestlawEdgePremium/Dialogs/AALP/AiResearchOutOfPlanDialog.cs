namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Out of plan dialog
    /// </summary>
    public class AiResearchOutOfPlanDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='ancillary-warning-modal']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2");
        private static readonly By GetAnswerlButonLocator = By.XPath(".//button[@class='co_primaryBtn']");
        private static readonly By CancelButonLocator = By.XPath(".//button[@class='co_secondaryBtn']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Get answer button
        /// </summary>
        public IButton GetAnswerButton => new Button(ContainerLocator, GetAnswerlButonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(ContainerLocator, CancelButonLocator);
    }
}

