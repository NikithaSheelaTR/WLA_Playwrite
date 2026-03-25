namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial
{
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Items.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Pages.Judicial;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The assign document to party component
    /// </summary>
    public class AssignDocumentsToPartiesComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentItemLocator = By.XPath("./li");
        private static readonly By FirstPartyTextboxLocator = By.Id("coid_qc_firstParty");
        private static readonly By SecondPartyTextboxLocator = By.Id("coid_qc_secondParty");
        private static readonly By ReportNameLocator = By.Id("coid_qc_reportName");
        private static readonly By StartCheckButtonLocator = By.Id("coid_qc_startCheckButton");
        private static readonly By RemoveFocusLocator = By.XPath("//h1");

        /// <summary>
        /// First party name
        /// </summary>
        public ITextbox FirstPartyNameTextBox => new JudicialUploadPageTextbox(FirstPartyTextboxLocator);

        /// <summary>
        /// SecondPartyName
        /// </summary>
        public ITextbox SecondPartyNameTextBox => new JudicialUploadPageTextbox(SecondPartyTextboxLocator);

        /// <summary>
        /// Report name text box
        /// </summary>
        public ITextbox ReportNameTextBox => new JudicialUploadPageTextbox(ReportNameLocator);

        /// <summary>
        /// Start check button
        /// </summary>
        public IButton StartCheckButton => new Button(StartCheckButtonLocator);

        /// <summary>
        /// Judicial Parties list
        /// </summary>
        protected EnumPropertyMapper<JudicialParties, WebElementInfo> JudicialParties =>
            EnumPropertyModelCache.GetMap<JudicialParties, WebElementInfo>(
                "",
                @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// This component locator
        /// </summary>
        protected override By ComponentLocator => By.ClassName("DA-AssignToPartiesSection");

        /// <summary>
        /// The Remove Focus From Item
        /// </summary>
        public void RemoveFocusFromItem() => DriverExtensions.Click(RemoveFocusLocator);

        /// <summary>
        /// Get free cell to drop element
        /// </summary>
        /// <param name="party">
        /// The party.
        /// </param>
        /// <returns>
        /// Element to drop
        /// </returns>
        public IItemsCollection<JudicialUploadedDocumentItem> GetAssignmentSlotsForParty(JudicialParties party) =>
            new ItemsCollection<JudicialUploadedDocumentItem>(
                By.XPath(this.JudicialParties[party].Id),
                DocumentItemLocator);

        /// <summary>
        /// Gets the judicial report
        /// todo remove take the tour dialog when Judicial preferences will be checked in
        /// </summary>
        /// <returns>Judicial recommendations page</returns>
        public JudicialRecommendationsPage GetReport()
        {
            DriverExtensions.ScrollPageToBottom();
            var filesUploadDialog = this.StartCheckButton.Click<QuickCheckFileUploadDialog>();
            filesUploadDialog.WaitUntilFileUpload();
            
            return new JudicialRecommendationsPage();
        }

        /// <summary>
        /// Gets the judicial Advantage report
        /// todo remove take the tour dialog when Judicial preferences will be checked in
        /// </summary>
        /// <returns>Judicial recommendations page</returns>
        public AdvantageJudicialRecommendationsPage GetAdvantageReport()
        {
            DriverExtensions.ScrollPageToBottom();
            var filesUploadDialog = this.StartCheckButton.Click<QuickCheckFileUploadDialog>();
            filesUploadDialog.WaitUntilFileUpload();

            return new AdvantageJudicialRecommendationsPage();
        }
    }
}