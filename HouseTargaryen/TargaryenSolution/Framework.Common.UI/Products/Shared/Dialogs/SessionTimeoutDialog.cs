namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when the user is not active for few minutes
    /// </summary>
    public class SessionTimeOutDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContinueResearchButtonLocator = By.XPath("//*[@name='ContinueResearch']");

        /// <summary>
        /// Continue Research button  
        /// </summary>
        public IButton ContinueResearchButton => new Button(ContinueResearchButtonLocator);        
    }
}