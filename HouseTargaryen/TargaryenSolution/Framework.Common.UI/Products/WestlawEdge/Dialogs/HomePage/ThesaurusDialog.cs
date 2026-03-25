namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Components.Thesaurus;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Thesaurus dialog in typeAhead
    /// </summary>
    public class ThesaurusDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_thesaurus_lightbox']");
        private static readonly By TabContainerLocator = By.XPath(".//div[contains(@class,'Tab-container')]");
        private static readonly By InfoButtonLocator = By.Id("coid_thesaurusInfoIcon");
        private static readonly By HoverMessageLocator = By.XPath("//div[@id='coid_thesaurusInfoMessage']");
        private static readonly By HoverMessageTextLocator = By.XPath("//*[@class='co_overlayBox_subtitle']");
        private static readonly By HoverMessageBoldTextLocator = By.XPath("//*[@class='co_thesaurus_query']//b");
        private static readonly By CancelButtonLocator = By.XPath("//button[@class='co_overlayBox_buttonCancel']");
        private static readonly By CloseButtonLocator = By.Id("coid_thesaurus_cancel");
        private static readonly By SearchButtonLocator = By.XPath("//button[@class='co_primaryBtn' and text()='Search']");
        private static readonly By EditQueryButtonLocator = By.XPath("//button[@class='co_tbButton QueryEditButton ']");
        private static readonly By QueryPreviewTitleLocator = By.XPath(".//span[@class = 'QueryPreview']");
        private static readonly By QueryPreviewTextBoxLocator = By.XPath("//textarea[contains(@id,'coid_thesaurus_queryPreviewText')]");
        private static readonly By QueryPreviewBoldTextLocator = By.XPath(".//div[contains(@class,'co_thesaurus_querypreview')]//b");
        private static readonly By JurisdictionFieldLocator = By.XPath(".//span[@class = 'QueryJurisdiction']");
        private static readonly By ContentTypeFieldLocator = By.XPath(".//label[@for = 'coid_thesaurus_queryPreviewText']");
        private static readonly By ApplyButtonLocator = By.XPath(".//button[contains(@class,'QueryPreviewEditButton') and text()='Apply']");
        private static readonly By QueryPreviewCancelButtonLocator = By.XPath("//button[contains(@class,'QueryPreviewEditButton') and contains(text(),'Cancel')]");
        private static readonly By NoSuggestionsMessageLocator = By.XPath(".//div[@class='co_infoBox NoSuggestions']");
        private static readonly By DidYouMeanInfoMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By DidYouMeanSuggestionButtonLocator = By.XPath(".//*[@class='BtnDidYouMean ']");
        private static readonly By QueryLimitingMessageLocator = By.XPath(".//div[not (@class ='co_hideState')]/span[@class='QueryRemaining']");
        private static readonly By ErrorHandlingMessageLocator = By.XPath(".//div[@class='co_overlayBox_content']//div[@class='co_infoBox_message']/parent::div[not(contains(@class,'hide'))]");
        private static readonly By CloseErrorHandlingButtonLocator = By.XPath(".//div[@class='co_ThesaurusErrorMessage co_infoBox']/*[@class='co_infoBox_closeButton']");
        private static readonly By RelatedconceptsTitleLocator = By.XPath("//li[@id='tab_relatedConceptsTabId']");

        /// <summary>
        /// Gets or sets the tab panel
        /// </summary>
        public ThesaurusTabPanel TabPanel { get; set; } = new ThesaurusTabPanel();

        /// <summary>
        /// Info button
        /// </summary>
        public IButton InfoButton => new Button(this.Container, InfoButtonLocator);

        /// <summary>
        /// RelatedConcepts
        /// </summary>
        public IButton RelatedConceptsButton => new Button(this.Container, RelatedconceptsTitleLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);

        /// <summary>
        /// Search button
        /// </summary>
        public IButton SearchButton => new Button(this.Container, SearchButtonLocator);

        /// <summary>
        /// Edit query button
        /// </summary>
        public IButton EditQueryButton => new Button(this.Container, EditQueryButtonLocator);

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new Button(this.Container, ApplyButtonLocator);

        /// <summary>
        /// Did you mean suggestion button
        /// </summary>
        public IButton DidYouMeanSuggestionButton => new Button(this.Container, DidYouMeanSuggestionButtonLocator);

        /// <summary>
        /// Did you mean info message label
        /// </summary>
        public ILabel DidYouMeanInfoMessageLabel => new Label(this.Container, DidYouMeanInfoMessageLocator);

        /// <summary>
        /// Close error handling button
        /// </summary>
        public IButton CloseErrorHandlingButton => new Button(this.Container, CloseErrorHandlingButtonLocator);

        /// <summary>
        /// Query preview cancel button
        /// </summary>
        public IButton QueryPreviewCancelButton => new Button(this.Container, QueryPreviewCancelButtonLocator);

        /// <summary>
        /// Query preview title label
        /// </summary>
        public ILabel QueryPreviewTitleLabel => new Label(this.Container, QueryPreviewTitleLocator);

        /// <summary>
        /// Query limiting message label
        /// </summary>
        public ILabel QueryLimitingMessageLabel => new Label(this.Container, QueryLimitingMessageLocator);

        /// <summary>
        /// Error handling message label
        /// </summary>
        public ILabel ErrorHandlingMessageLabel => new Label(this.Container, ErrorHandlingMessageLocator);

        /// <summary>
        /// Hover message label
        /// </summary>
        public ILabel HoverMessageLabel => new Label(this.Container, HoverMessageLocator);

        /// <summary>
        /// Hover message Text label
        /// </summary>
        public ILabel HoverMessageTextLabel => new Label(this.Container, HoverMessageTextLocator);

        /// <summary>
        /// No suggestions message label
        /// </summary>
        public ILabel NoSuggestionsMessageLabel => new Label(this.Container, NoSuggestionsMessageLocator);

        /// <summary>
        /// Jurisdiction label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.Container, JurisdictionFieldLocator);

        /// <summary>
        /// Content type label
        /// </summary>
        public ILabel ContentTypeLabel => new Label(this.Container, ContentTypeFieldLocator);

        /// <summary>
        /// Query preview bold label
        /// </summary>
        public IReadOnlyCollection<ILabel> QueryPreviewBoldLabels =>
            new ElementsCollection<Label>(this.Container, QueryPreviewBoldTextLocator);

        /// <summary>
        /// Hover message bold label
        /// </summary>
        public IReadOnlyCollection<ILabel> HoverMessageBoldLabels => 
            new ElementsCollection<Label>(this.Container, HoverMessageBoldTextLocator);

        /// <summary>
        /// Query preview textbox
        /// </summary>
        public ITextbox QueryPreviewTextBox => new Textbox(this.Container, QueryPreviewTextBoxLocator);

        /// <summary>
        /// Container
        /// </summary>
        private IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Check is top of the dialog is enabled
        /// </summary>
        /// <returns> True - if top of the dialog is enabled </returns>
        public bool IsTabContainerEnabled() => 
            DriverExtensions.GetAttribute("class", DriverExtensions.WaitForElement(this.Container, TabContainerLocator)).Equals("false");


        /// <summary>
        /// Click on the SearchButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the CancelButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, CancelButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the QueryPreviewCancelButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public ThesaurusDialog ClickQueryPreviewCancelButton() 
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, QueryPreviewCancelButtonLocator);
            return new ThesaurusDialog(); ;
        }

        /// <summary>
        /// Click on the CloseErrorHandlingButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public ThesaurusDialog ClickCloseErrorHandlingCancelButton()
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, CloseErrorHandlingButtonLocator);
            return new ThesaurusDialog(); ;
        }

        /// <summary>
        /// Click on the EditQueryButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public ThesaurusDialog ClickEditQueryButton()
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, EditQueryButtonLocator);
            return new ThesaurusDialog(); ;
        }

        /// <summary>
        /// Click on the EditQueryButton
        /// </summary>
        /// <returns>
        /// The <see cref="ThesaurusDialog"/>.
        /// </returns>
        public ThesaurusDialog ClickRelatedConceptsButton()
        {
            DriverExtensions.WaitForElementDisplayed(this.Container);
            DriverExtensions.JavascriptClick(ContainerLocator, RelatedconceptsTitleLocator);
            return new ThesaurusDialog(); ;
        }
    }
}
