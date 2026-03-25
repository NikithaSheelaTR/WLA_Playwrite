namespace Framework.Common.UI.Products.Shared.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that pops up when user reaches document limit and tries to view another document
    /// </summary>
    public class DocumentLimitReachedDialog : BaseModuleRegressionDialog
    {
        private static readonly By GoBackToPreviousPageLinkLocator = By.Id("coid_website_blockedBackLinkHistoryBack");

        /// <summary>
        /// Go Back To the Previous Page Link
        /// </summary>
        public ILink GoBackToPreviousPageLink => new Link(GoBackToPreviousPageLinkLocator);
    }
}
