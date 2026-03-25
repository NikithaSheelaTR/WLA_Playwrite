using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components.Document.RI;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Products.Shared.Elements.Links;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.Shared.Pages
{
    /// <summary>
    /// Error page without spinner
    /// </summary>
    public class ErrorPageWithoutSpinner : BaseModuleRegressionPage
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'co_infoBox failure') and @style='display: block;']");
        private static readonly By FailureMessageLocator = By.XPath(".//*[@class='co_website_errorsSummaryList']//li");
        private static readonly By MoreInfoLinkLocator = By.Id("co_website_errorSummaryToggleLink");

        /// <summary>
        /// Failure label
        /// </summary>
        public ILabel FailureMessageLabel => new Label(ContainerLocator, FailureMessageLocator);

        /// <summary>
        /// More info link
        /// </summary>
        public ILink MoreInfoLink => new Link(ContainerLocator, MoreInfoLinkLocator);

        /// <summary> Related info tabs </summary>
        public RelatedInfoTabComponent RiTabs { get; } = new RelatedInfoTabComponent();
    }
}
