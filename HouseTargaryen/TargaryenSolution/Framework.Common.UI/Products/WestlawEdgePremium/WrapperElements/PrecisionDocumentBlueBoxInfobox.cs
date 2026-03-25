namespace Framework.Common.UI.Products.WestlawEdgePremium.WrapperElements
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using OpenQA.Selenium;

    /// <summary>
    /// Blue box infobox on Document page
    /// </summary>
    public class PrecisionDocumentBlueBoxInfobox: InfoBox, IPrecisionDocumentBlueBoxInfobox
    {
        private static readonly By MoreLikeThisButtonLocator = By.XPath(".//a[@class='Athens-cited-link Athens-moreLikeThisHeadnoteLink']");

        /// <summary>
        /// currentContainer - InfoBox container
        /// </summary>
        public PrecisionDocumentBlueBoxInfobox(By currentContainer) : base(currentContainer)
        {
        }
        
        /// <summary>
        /// More like this button
        /// </summary>
        public IButton MoreLikeThisButton => new Button(this.GetContainer(), MoreLikeThisButtonLocator);
    }
}
