namespace Framework.Common.UI.Products.WestlawNextCorrectional.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Correctional Help Guide page
    /// </summary>
    public class CorrectionalHelpGuidePage : BaseModuleRegressionPage
    {
        private static readonly By HeaderTitleLocator = By.XPath("//div[@id='co_subHeader']/h1");

        private static readonly By BackLinkLocator = By.XPath("//a[contains(.,'Back')]");

        /// <summary>
        /// HeaderTitle
        /// </summary>
        public ILabel HeaderTitleLabel => new Label(HeaderTitleLocator);

        /// <summary>
        /// Back link to home page
        /// </summary>
        public ILink BackButtonLabel => new Link(BackLinkLocator);
    }
}
