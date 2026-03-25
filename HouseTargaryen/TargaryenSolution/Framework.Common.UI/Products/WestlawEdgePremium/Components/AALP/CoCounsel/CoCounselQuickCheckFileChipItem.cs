namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// CoCounselQuickCheckFileChipItem
    /// </summary>
    public class CoCounselQuickCheckFileChipItem : BaseItem
    {
        private static readonly By RemoveButtonLocator = By.XPath(".//saf-button[contains(@class, 'removeUpload')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="CoCounselQuickCheckFileChipItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public CoCounselQuickCheckFileChipItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Remove chip file button
        /// </summary>
        public IButton RemoveButton => new Button(this.Container, RemoveButtonLocator);
    }
}
