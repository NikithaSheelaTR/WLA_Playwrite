namespace Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document upload page.
    /// </summary>
    public class QuickCheckUploadPage : QuickCheckBasePage
    {
        private static readonly By ChooseFileLocator = By.XPath("//input[@name='file']");
        private static readonly By EnterPlainTextLinkLocator = By.XPath("//button[@class='co_linkBlue'] | //button[text()='Edit full view']");
        private static readonly By PageHeadingLocator = By.XPath("//div[@class = 'DA-Title']/h1");
        private static readonly By PageDescriptionLocator = By.XPath("//div[@class = 'DA-Title']/span");
        private static readonly By SecurityTextLocator = By.XPath("//button[@class = 'DA-SecurityContainer']/p");
        private static readonly By LegalIssueLocator = By.XPath("//*[@id='legalIssueInput_general']");
        private static readonly By TextWithCitedAuthorityLocator = By.XPath("//*[@id='plainTextInput_general']");
        private static readonly By ErrorMessageLocator = By.ClassName("co_errorMessage");
        private static readonly By StartAnalysisButtonLocator = By.XPath("//*[text()='Start analysis']");
        private static readonly By JurisdictionDialogLabelLocator = By.Id("co_jurisdictionSelectorDialog");
        
        /// <summary>
        /// Gets the enter plain text link.
        /// </summary>
        public ILink EnterPlainTextLink { get; } = new Link(EnterPlainTextLinkLocator);

        /// <summary>
        /// Gets the select file button.
        /// </summary>
        public IButton SelectFileButton { get; } = new Button(ChooseFileLocator);

        /// <summary>
        /// Gets the page heading label.
        /// </summary>
        public ILabel PageHeadingLabel { get; } = new Label(PageHeadingLocator);

        /// <summary>
        /// Gets the error messge label.
        /// </summary>
        public ILabel ErrorMessageLabel { get; } = new Label(ErrorMessageLocator);

        /// <summary>
        /// Gets the page description label.
        /// </summary>
        public ILabel PageDescriptionLabel { get; } = new Label(PageDescriptionLocator);

        /// <summary>
        /// Gets the jurisdiction dialog label.
        /// </summary>
        public ILabel JurisdictionDialogLabel => new Label(JurisdictionDialogLabelLocator);

        /// <summary>
        /// Gets the security text label.
        /// </summary>
        public IButton SecurityTextButton { get; } = new Button(SecurityTextLocator);

        /// <summary>
        /// Gets the heading textbox.
        /// </summary>
        public ITextbox HeadingTextbox { get; } = new Textbox(LegalIssueLocator);

        /// <summary>
        /// Gets the plain text textbox.
        /// </summary>
        public ITextbox PlainTextTextbox { get; } = new SearchWithinTextbox(TextWithCitedAuthorityLocator);

        /// <summary>
        /// Gets the start analysis button.
        /// </summary>
        public IButton StartAnalysisButton { get; } = new Button(StartAnalysisButtonLocator);

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <typeparam name="T">
        /// The uploading progress dialog
        /// </typeparam>
        /// <param name="path">
        /// The path to file.
        /// </param>
        /// <returns>
        /// The document analyzer recommendations page.
        /// </returns>
        public T UploadFile<T>(string path) where T : BaseModuleRegressionDialog
        {
            this.SelectFileButton.SendKeys(path);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}