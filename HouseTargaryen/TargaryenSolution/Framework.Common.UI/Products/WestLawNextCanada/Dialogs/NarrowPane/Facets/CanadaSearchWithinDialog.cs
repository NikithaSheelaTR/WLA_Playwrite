namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Search Within Dialog
    /// </summary>
    public class CanadaSearchWithinDialog : EdgeSearchWithinDialog
    {
        private static readonly By SearchDialogTitleLabelLocator = By.XPath("//div[@id='coid_SearchWithinLightbox']//h2");
        private static readonly By SearchInputTextboxLocator = By.ClassName("SearchFacet-inputText");
        private static readonly By SearchWithinButtonLocator = By.XPath("//button[contains(@class,'SearchFacet-buttonSubmit')]");
        private static readonly By SearchWithinHelpButtonLocator = By.XPath("//div[@class='SearchFacetSearchWithinHelp']/button");
        private static readonly By ConnectorsAndExpandersInfoLabelLocator = By.ClassName("SearchFacetSearchWithinHelp-body");
        private static readonly By ContainerLocator = By.Id("coid_SearchWithinLightbox");

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeSearchWithinDialog"/> class. 
        ///  </summary>
        /// <param name="additionalInfo"> additional Info </param>
        public CanadaSearchWithinDialog(string additionalInfo) : base(additionalInfo)
        {
        }

        /// <summary>
        /// Dialg Title Label
        /// </summary>
        public ILabel SearchDialogTitleLabel => new Label(ContainerLocator,SearchDialogTitleLabelLocator);

        /// <summary>
        /// Search within Input Textbox
        /// </summary>
        public ITextbox SearchWithinInputTextbox => new Textbox(ContainerLocator, SearchInputTextboxLocator);

        /// <summary>
        /// Search within Button
        /// </summary>
        public IButton SearchWithinButton => new Button(ContainerLocator, SearchWithinButtonLocator);

        /// <summary>
        /// Search within Help Button
        /// </summary>
        public IButton SearchWithinHelpButton => new Button(ContainerLocator, SearchWithinHelpButtonLocator);

        /// <summary>
        /// Connectors and expanders Info Label
        /// </summary>
        public ILabel ConnectorsAndExpandersInfoLabel => new Label(ContainerLocator, ConnectorsAndExpandersInfoLabelLocator);
    }
}