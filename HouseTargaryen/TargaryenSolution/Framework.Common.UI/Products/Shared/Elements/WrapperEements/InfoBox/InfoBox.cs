namespace Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The InfoBox.
    /// </summary>
    public class InfoBox : BaseWebElement, IInfoBox
    {
        /// <summary>
        /// Close button locator
        /// </summary>
        protected static By CloseButtonLocator = By.XPath(".//*[contains(@class,'closeButton')] | .//*[contains(@id,'closeButton')]");
        private By MessageLocator = null;   

        /// <summary>
        /// currentContainer - InfoBox container, closeLocator - X button locator
        /// </summary>
        public InfoBox(IWebElement currentContainer, By closeLocator) : base(currentContainer) => CloseButtonLocator = closeLocator;
        
        /// <summary>
        /// currentContainer - InfoBox container
        /// </summary>
        public InfoBox(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <summary>
        /// currentContainer - InfoBox container
        /// </summary>
        public InfoBox(By currentContainer) : base(currentContainer)
        {
        }

        /// <summary>
        /// currentContainer - InfoBox container, messageLocator - info text locator, closeLocator - X button locator
        /// </summary>        
        public InfoBox(By currentContainer, By messageLocator, By closeLocator) : base(currentContainer) => (CloseButtonLocator, MessageLocator)
            = (closeLocator, messageLocator);
       

        /// <summary>
        /// X button on the info box
        /// </summary>
        public IButton CloseButton => new Button(this.GetContainer(), CloseButtonLocator);

        /// <summary>
        /// Get Info text
        /// </summary>
        public override string Text =>
           MessageLocator == null
                    ? base.Text
                    : DriverExtensions.GetElement(this.GetContainer(), MessageLocator).GetText() ?? string.Empty;    
    }
}

    

