namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Out of plan block dialog
    /// </summary>
    public class AiResearchOutOfPlanBlockDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='block-modal']");
        private static readonly By TitleLabelLocator = By.XPath(".//h2");
        private static readonly By OkButonLocator = By.XPath(".//button[@class='co_primaryBtn']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Ok button
        /// </summary>
        public IButton OkButton => new Button(ContainerLocator, OkButonLocator);
    }
}


