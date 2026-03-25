namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Cited Authority Item
    /// </summary>
    public class CanadaCitedAuthorityItem : CitedAuthorityItem
    {
        private static readonly By CollpaseExpandButtonLocator = By.XPath("//div[contains(@class,'DA-KCWarningSnippet')]//button");
        private static readonly By TreatmentLabelLocator = By.ClassName("DA-NegativeTreatmentTitle");
        private static readonly string TreatmentLinkLocator = "//ul[@class='DA-LinkList']//a[contains(text(),'{0}')]";
       

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaCitedAuthorityItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public CanadaCitedAuthorityItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Collpase/Expand button for the warnings for cited authority tab.
        /// </summary>
        public IButton CollpaseExpandButton => new Button(CollpaseExpandButtonLocator);

        /// <summary>
        /// Treatment Label
        /// </summary>
        public ILabel TreatmentLabel => new Label(TreatmentLabelLocator);

        /// <summary>
        /// Creates a link to the treatment of the cited authority item.
        /// </summary>
        /// <param name="linkText">Link Text</param>
        /// <returns></returns>
        public ILink TreatmentLink(string linkText) => new Link(By.XPath(string.Format(TreatmentLinkLocator, linkText)));
    }
}