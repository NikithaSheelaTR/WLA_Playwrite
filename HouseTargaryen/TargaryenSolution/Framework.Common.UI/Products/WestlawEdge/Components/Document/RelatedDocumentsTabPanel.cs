namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Components.TabPanel;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Related documents tab panel
    /// </summary>
    public class RelatedDocumentsTabPanel : TabPanel<RelatedDocumentsTabs>
    {
        private static readonly By CurrentActiveTab = By.XPath("//ul[contains(@class,'Tab-list')]/li[contains(@class,'Tab--active')]");
        private static readonly By RelatedDocumentsComponentLocator = By.XPath("//div[@id='coid_website_relatedContentWidget']/div");
        private static readonly By RelatedDocumentsComponentTabLocator = By.XPath("//div[@class = 'Tab-container ']/ul[@aria-label='Related documents']");
        private static readonly By RelatedDocumentComponentNameLocator = By.XPath("//div[@id='coid_website_relatedContentWidget']//h3");

        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedDocumentsTabPanel"/> class. 
        /// </summary>
        public RelatedDocumentsTabPanel()
        {
            this.AllPossibleTabOptions = new Dictionary<RelatedDocumentsTabs, Type>
                                             {
                                                 {
                                                     RelatedDocumentsTabs.SelectedTopics,
                                                     typeof(SelectedTopicsTabComponent)
                                                 },
                                                 {
                                                     RelatedDocumentsTabs.FormFamilies,
                                                     typeof(FormFamiliesTabComponent)
                                                 },
                                                 {
                                                     RelatedDocumentsTabs.SecondarySources,
                                                     typeof(SecondarySourcesTabComponent)
                                                 },
                                                 {
                                                     RelatedDocumentsTabs.Briefs,
                                                     typeof(BriefsTabComponent)
                                                 },
                                                 {
                                                     RelatedDocumentsTabs.TrialCourtDocuments,
                                                     typeof(TrialCourtDocumentsTabComponent)
                                                 }
                                             };
        }

        /// <summary>
        /// Checks if tab is active
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if expected tab is active</returns>
        public override bool IsActive(RelatedDocumentsTabs tab)
        {
            string activeTabText = DriverExtensions.WaitForElement(CurrentActiveTab).Text;
            string tabToCheckText = this.TabsMap[tab].Text;
            return activeTabText.Equals(tabToCheckText);
        }

        /// <summary>
        /// Checks if tab is displayed
        /// </summary>
        /// <param name="tab">tab</param>
        /// <returns>true if displayed</returns>
        public override bool IsDisplayed(RelatedDocumentsTabs tab) => 
            DriverExtensions.IsDisplayed(By.XPath(this.TabsMap[tab].LocatorString));

        /// <summary>
        /// Verifies that the Related Documents component is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Related Documents component is displayed. </returns>
        public bool IsRelatedDocumentsComponentDisplayed()
            => DriverExtensions.IsDisplayed(RelatedDocumentsComponentLocator);

        /// <summary>
        /// Verifies that the Related Documents component is in view.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the Related Documents component is in view. </returns>
        public bool IsRelatedDocumentsComponentInView()
            => DriverExtensions.GetElement(RelatedDocumentsComponentLocator).IsElementInView();

        /// <summary>
        /// Verifies that the related documents widget is displayed in tabular.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the related documents widget displayed in tabular. </returns>
        public bool IsRelatedDocumentsComponentDisplayedInTabular() => DriverExtensions.IsDisplayed(RelatedDocumentsComponentTabLocator);

        /// <summary>
        /// Verifies that the related documents widget name is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the related documents widget name is displayed. </returns>
        public bool IsRelatedDocumentsComponentNameDisplayed() => DriverExtensions.IsDisplayed(
            RelatedDocumentComponentNameLocator);

        /// <summary>
        /// Gets related documents widget name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>. Related documents widget name. </returns>
        public string GetRelatedDocumentsComponentName() => DriverExtensions.GetText(
            RelatedDocumentComponentNameLocator);

        /// <summary>
        /// Click Tab
        /// </summary>
        /// <param name="tab">tab option</param>
        /// <returns>ITAB object</returns>
        protected override TTab ClickTab<TTab>(RelatedDocumentsTabs tab)
        {
            By relatedDocTab = By.XPath(this.TabsMap[tab].LocatorString);
            DriverExtensions.ScrollTo(relatedDocTab);
            DriverExtensions.Click(relatedDocTab);

            return DriverExtensions.CreatePageInstance<TTab>();
        }
    }
}
