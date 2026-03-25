namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;

    /// <summary>
    /// CoCounsel Complaint Analysis Result item
    /// </summary>
    public class CoCounselComplaintAnalysisResultItem : BaseItem
    {
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UserQuestionItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public CoCounselComplaintAnalysisResultItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Get the Complaint Analyzer Result Tab Panel 
        /// </summary>
        public ComplaintAnalyzerResultTabPanel ComplaintAnalyzerResultTabPanel => new ComplaintAnalyzerResultTabPanel(this.Container);
    }
}
