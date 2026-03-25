namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document.RI;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// NegativeTreatmentPage
    /// </summary>
    public class EdgeNegativeTreatmentPage : NegativeTreatmentPage
    {
        private const string ReferenceGridLocator = "//div[@id='coid_relatedinfo_detailsContainer']//table[@id='co_relatedInfo_table']";
        private const string OverrulingRiskGridLocator = "//div[@id='coid_relatedinfo_io_detailsContainer']//table[@id='co_relatedInfo_table']";
        private static readonly By SummarizeNegativeTreatmentButtonLocator = By.XPath("//li[@id='co_docDocumentAnalyzer']/saf-button");
        
        /// <summary>
        /// Summarize negative treatment button
        /// </summary>
        public IButton SummarizeNegativeTreatmentButton => new Button(SummarizeNegativeTreatmentButtonLocator);

        /// <summary>
        /// WestlawNext Header
        /// </summary>
        public new EdgeDocumentFixedHeaderComponent FixedHeader { get; } = new EdgeDocumentFixedHeaderComponent();

        /// <summary>
        /// Reference grid
        /// </summary>
        new public EdgeReferenceGridComponent NegativeTreatementReferenceGrid { get; private set; }
            = new EdgeReferenceGridComponent(ReferenceGridLocator);

        /// <summary>
        /// Overruling Risk Grid
        /// </summary>
        public EdgeReferenceGridComponent OverrulingRiskGrid { get; private set; }
            = new EdgeReferenceGridComponent(OverrulingRiskGridLocator);
        
        /// <summary> 
        /// Negative Treatment summary component  
        /// </summary>
        public NegativeTreatmentSummaryComponent NegativeTreatmentSummary { get; protected set; } = new NegativeTreatmentSummaryComponent();

        /// <summary>
        /// Click Negative Treatment button
        /// </summary>
        public void ClickNegativeTreatmentButton()
        {
            IWebElement searchAreaElement = DriverExtensions.GetElement(SummarizeNegativeTreatmentButtonLocator);  
            DriverExtensions.ExecuteScript("arguments[0].click()", searchAreaElement);           
        }
    }
}
