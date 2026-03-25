namespace Framework.Common.UI.Products.Shared.Dialogs.Document.SearchWithin
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Within Document Error Dialog
    /// </summary>
    public class SearchWithinDocumentErrorDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id = 'co_searchWithinErrorMessageBox']");

        private static readonly By IgnoreSpellCheckLinkLocator =
            By.XPath(".//div[@id='co_docSearchWithinIgnoreSpellCheck']/a");

        private static readonly By SuggestionTermLinkLocator =
            By.XPath(".//div[@id='co_docSearchWithinTermSuggestion']/span/a/em");

        /// <summary>
        /// Ignore spell check link
        /// </summary>
        public ILink IgnoreSpellCheckLink => new Link(ContainerLocator, IgnoreSpellCheckLinkLocator);

        /// <summary>
        /// Ignore spell check link
        /// </summary>
        public ILink SuggestionTermLink => new Link(ContainerLocator, SuggestionTermLinkLocator);
    }
}