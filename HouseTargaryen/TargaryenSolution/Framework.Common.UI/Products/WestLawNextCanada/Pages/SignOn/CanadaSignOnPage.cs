namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.SignOn
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Sign On Page
    /// </summary>
    public class CanadaSignOnPage : CommonSignOnPage
    {
        private static readonly By SignOnTitleLabelLocator = By.CssSelector(".CustomFont.Cosi-header");

        /// <summary>
        /// Sign On Title Label
        /// </summary>
        public ILabel SignOnTitleLabel => new Label(SignOnTitleLabelLocator);
    }
}