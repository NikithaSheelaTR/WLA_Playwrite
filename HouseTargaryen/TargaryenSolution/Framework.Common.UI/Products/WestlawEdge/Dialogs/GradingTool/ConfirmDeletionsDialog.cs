namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.GradingTool
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// The confirm deletions dialog.
    /// </summary>
    public class ConfirmDeletionsDialog : BaseModuleRegressionDialog
    {
        private static readonly By OkButtonLocator = By.Id("coid_createExperiment");

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton OkButton => new Button(OkButtonLocator);        
    }
}
