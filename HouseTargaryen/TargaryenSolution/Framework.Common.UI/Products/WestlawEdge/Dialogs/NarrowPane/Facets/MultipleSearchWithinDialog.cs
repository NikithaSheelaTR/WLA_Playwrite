namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Multiple Search Within dialog
    /// </summary>
    public class MultipleSearchWithinDialog : BaseModuleRegressionDialog
    {
        private static readonly By AlertInfoBoxLocator = By.XPath(".//*[@class='co_infoBox_message']");
        private static readonly By AppliedSearchesLocator = By.XPath(".//ul[@class='searchWithin-list']//li");
        private static readonly By CancelButtonLocator = By.XPath(".//a[@class='co_overlayBox_buttonCancel']");
        private static readonly By CloseAlertInfoboxButtonLocator = By.XPath(".//div[@class='co_infoBox informational']/a");
        private static readonly By CrossedOutTermLocator = By.XPath(".//ul[@class='searchWithin-list']//p[contains(@class, 'strikethrough')]");
        private static readonly By ConnectorsAndExpandersInfoboxLocator = By.XPath(".//div[contains(@class, 'SearchFacetSearchWithinHelp-body')]");
        private static readonly By DocumentsRadioButtonLocator = By.XPath(".//div[@class='SearchFacet-optionItem']//*[contains(@id, 'documentsSearch')]");
        private static readonly By EditPreviousSearchesButtonLocator = By.XPath(".//button[contains(@class, 'co_primaryBtn') and text()='Edit previous']");
        private static readonly By ErrorMessageLocator = By.Id("co_searchWithinWidget_error");
        private static readonly By ExpandConnectorsButtonLocator = By.XPath(".//*[contains(@class, 'SearchFacet-buttonLink')]");
        private static readonly By HeaderInfoLabelLocator = By.XPath(".//div[@class='SearchWithinLightBox-header']//span");
        private static readonly By MaterialFactsRadioButtonLocator = By.XPath(".//div[@class='SearchFacet-optionItem']//*[contains(@id, 'materialFactsSearch')]");
        private static readonly By RemoveAllButtonLocator = By.XPath(".//button[contains(@class, 'co_secondaryBtn') and text()='Remove all']");
        private static readonly By RecentSearchesQueryLocator = By.XPath(".//ul[@class='a11yDropdown-menu SearchFacetSearchResults-list']/li");
        private static readonly By RecentSearchesButtonLocator = By.XPath(".//button[contains(@class,'SearchFacet-button SearchFacet-buttonSearch')]");
        private static readonly By SearchTextboxLocator = By.XPath(".//input[contains(@name,'SearchFacetSearchWithin-')]");
        // depending from single or multiple filter mode button has text 'Search' or 'Continue' respectively
        private static readonly By SearchButtonLocator = By.XPath(".//button[@class='co_primaryBtn' and text()='Search' or text()='Continue'] | .//span[@class='SearchFacet-buttonText' and text()='Search']");   
        private static readonly By WarningMessageLabelLocator = By.XPath(".//div[@class='co_infoBox warning']");
        
        private IWebElement Container => DriverExtensions.WaitForElement(
            By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.MultipleSearchWithin].LocatorString));

        /// <summary>
        ///  Material facts radiobutton
        /// </summary>
        public IRadiobutton MaterialFactsRadiobutton => new Radiobutton(this.Container, MaterialFactsRadioButtonLocator);

        /// <summary>
        /// Documents radiobutton
        /// </summary>
        public IRadiobutton DocumentsRadiobutton => new Radiobutton(this.Container, DocumentsRadioButtonLocator);

        /// <summary>
        /// Search or Continue button 
        /// </summary>
        public IButton SearchButton => new Button(this.Container, SearchButtonLocator);

        /// <summary>
        /// Search textbox
        /// </summary>
        public ITextbox SearchTextbox => new SearchWithinTextbox(this.Container, SearchTextboxLocator);

        /// <summary>
        /// Cancel button 
        /// </summary>
        public IButton CancelButton => new Button(this.Container, CancelButtonLocator);

        /// <summary>
        /// Alert infobox
        /// </summary>
        public IInfoBox AlertInfoBox => new InfoBox(new ByChained(By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.MultipleSearchWithin].LocatorString), AlertInfoBoxLocator));

        /// <summary>
        /// Close alert infobox button
        /// </summary>
        public IButton CloseAlertInfoboxButton => new Button(this.Container, CloseAlertInfoboxButtonLocator);

        /// <summary>
        /// Edit previous searches button
        /// </summary>
        public IButton EditPreviousSearchesButton => new Button(this.Container, EditPreviousSearchesButtonLocator);

        /// <summary>
        /// Remove All button 
        /// </summary>
        public IButton RemoveAllButton => new Button(this.Container, RemoveAllButtonLocator);

        /// <summary>
        /// Header info label
        /// </summary>
        public ILabel HeaderInfoLabel => new Label(this.Container, HeaderInfoLabelLocator);

        /// <summary>
        /// Error message label
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(this.Container, ErrorMessageLocator);

        /// <summary>
        /// Warning message label
        /// </summary>
        public ILabel WarningMessageLabel => new Label(this.Container, WarningMessageLabelLocator);

        /// <summary>
        ///  Expand connectors button
        /// </summary>
        public IButton ExpandConnectorsButton => new Button(this.Container, ExpandConnectorsButtonLocator);

        /// <summary>
        /// Connectors and expanders infobox
        /// </summary>
        public IInfoBox ConnectorsAndExpandersInfobox => new InfoBox(new ByChained(By.XPath(EnumPropertyModelCache.GetMap<Dialogs, WebElementInfo>()[Dialogs.MultipleSearchWithin].LocatorString), ConnectorsAndExpandersInfoboxLocator));

        /// <summary>
        /// Crossed out label
        /// </summary>
        public ILabel CrossedOutTermLabel => new Label(Container, CrossedOutTermLocator);

        /// <summary>
        /// Recent searches button 
        /// </summary>
        public IButton RecentSearchesButton => new Button(this.Container, RecentSearchesButtonLocator);

        /// <summary>
        /// Applied searches
        /// </summary>
        /// <returns>A list of applied searches.</returns>
        public ItemsCollection<PreviousSearchesItem> AppliedSearchesItems => new ItemsCollection<PreviousSearchesItem>(this.Container, AppliedSearchesLocator);

        /// <summary>
        /// Recent searches labels
        /// </summary>
        public IReadOnlyCollection<ILabel> RecentSearchesLabels => new ElementsCollection<Label>(this.Container, RecentSearchesQueryLocator);

        /// <summary>
        /// Click Recent Searches Query
        /// </summary>
        /// <param name="index"> link's index </param>
        /// <returns> The <see cref="MultipleSearchWithinDialog"/>. </returns>
        public MultipleSearchWithinDialog ClickRecentSearchesSuggestion(int index)
        {
            this.ClickElement(DriverExtensions.GetElements(this.Container, RecentSearchesQueryLocator).ElementAt(index));
            return this;
        }
    }
}