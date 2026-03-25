namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Document Miscellaneous component on the Westlaw Canada
    /// </summary>
    public class DocumentMiscellaneousComponent : BaseModuleRegressionComponent
    {
        private static readonly By DocumentContainerLocator = By.XPath(".//*[@id='co_contentColumn']//div");
        private static readonly By CaseLawDocLinkLocator = By.XPath(".//a[@id='cobalt_result_can_casesWithoutDecisions_title1']");
        private static readonly By DocumentTitleLinkLocator = By.XPath(".//span[@id='titleInfo']//a");
        private static readonly By DateDropdownLocator = By.XPath(".//span[@id='co_dateWidget_1_dropdown_span']");
        private static readonly By DateDropdownValueLocator = By.XPath(".//span[@id='co_dateWidgetSelectRangeLink_1_-1']//a");
        private static readonly By HistoryResultListLocator = By.XPath(".//a[contains(@id,'cobalt_foldering_ro_item_name')]");
        private static readonly By DocumentViewLabelLocator = By.XPath(".//span[@id='cobalt_foldering_ro_item_event_0']");
        private static readonly By StatutesAndRegDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_statutesAndRegs_title')]");
        private static readonly By RulesDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_rules_title')]");
        private static readonly By SecurityAndRegDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_securitiesAndBulletins_title')]");
        private static readonly By PolicyDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_policyDocsAndGuidance_title')]");
        private static readonly By CEDDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_ced_title')]");
        private static readonly By LegalMemoDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_legalmemo_title')]");
        private static readonly By TextsAndAnnotDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_textsAndAnnotations_title')]");
        private static readonly By ArticlesAndNewsDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_journals_title')]");
        private static readonly By InsolvencyCourtDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_insolvency_title')]");
        private static readonly By PleadingDocLinkLocator = By.XPath(".//a[contains(@id,'cobalt_result_can_pleadings_title')]");
        private static readonly By TreatmentLabelLocator = By.CssSelector("div.crsw_mostNegativeTreatment strong");
        private static readonly By NegativeTreatmentLabelLocator = By.XPath(".//div[@class='crsw_mostNegativeTreatment']");
        private static readonly By RecentTreatmentLabelLocator = By.CssSelector("div.crsw_mostRecentTreatment strong");
        private static readonly By RecentLinkTLocator = By.XPath(".//div[@class='crsw_mostRecentTreatment']//a");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DocumentContainerLocator;

        /// <summary>
        /// CaseLawDoc Link 
        /// </summary>
        public ILink CaseLawDocLink => new Link(this.ComponentLocator, CaseLawDocLinkLocator);

        /// <summary>
        /// Document Title Link 
        /// </summary>
        public ILink DocumentTitleLink => new Link(this.ComponentLocator, DocumentTitleLinkLocator);

        /// <summary>
        /// Date Dropdown 
        /// </summary>
        public IButton DateDropdown => new Button(DateDropdownLocator);

        /// <summary>
        /// Select Date Dropdown Value
        /// </summary>
        public IButton DateDropdownValue => new Button(DateDropdownValueLocator);

        /// <summary>
        /// Select All in Date DropDown button type
        /// </summary>
        public void SelectDateDropdownValue()
        {
            if (!DateDropdown.Text.Equals("All"))
            {
                DateDropdown.Click();
                DateDropdownValue.Click();
                DriverExtensions.WaitForPageLoad();
            }
        }

        /// <summary>
        /// History ResultList
        /// </summary>
        public IReadOnlyCollection<ILink> GetHistoryResultList => new ElementsCollection<Link>(this.ComponentLocator, HistoryResultListLocator);

        /// <summary>
        /// Document View Label 
        /// </summary>
        public ILabel DocumentViewLabel => new Label(this.ComponentLocator, DocumentViewLabelLocator);

        /// <summary>
        /// StatutesAndRegulations Link 
        /// </summary>
        public ILink StatutesAndRegDocLink => new Link(this.ComponentLocator, StatutesAndRegDocLinkLocator);

        /// <summary>
        /// Rules Link 
        /// </summary>
        public ILink RulesDocLink => new Link(this.ComponentLocator, RulesDocLinkLocator);

        /// <summary>
        /// SecurityAnd Regulatory Link 
        /// </summary>
        public ILink SecurityAndRegDocLink => new Link(this.ComponentLocator, SecurityAndRegDocLinkLocator);

        /// <summary>
        /// Policy Link 
        /// </summary>
        public ILink PolicyDocLink => new Link(this.ComponentLocator, PolicyDocLinkLocator);

        /// <summary>
        /// Canadian Encyclopedic Digest Link 
        /// </summary>
        public ILink CEDDocLink => new Link(this.ComponentLocator, CEDDocLinkLocator);

        /// <summary>
        /// LegalMemo Link 
        /// </summary>
        public ILink LegalMemoDocLink => new Link(this.ComponentLocator, LegalMemoDocLinkLocator);

        /// <summary>
        /// TextsAndAnnotDoc Link 
        /// </summary>
        public ILink TextsAndAnnotDocLink => new Link(this.ComponentLocator, TextsAndAnnotDocLinkLocator);

        /// <summary>
        /// ArticlesAndNews Link 
        /// </summary>
        public ILink ArticlesAndNewsDocLink => new Link(this.ComponentLocator, ArticlesAndNewsDocLinkLocator);

        /// <summary>
        /// InsolvencyCourt Link 
        /// </summary>
        public ILink InsolvencyCourtDocLink => new Link(this.ComponentLocator, InsolvencyCourtDocLinkLocator);

        /// <summary>
        /// Pleading Link 
        /// </summary>
        public ILink PleadingDocLink => new Link(this.ComponentLocator, PleadingDocLinkLocator);

        /// <summary>
        /// Treatment Label 
        /// </summary>
        public ILabel TreatmentLabel => new Label(this.ComponentLocator, TreatmentLabelLocator);

        /// <summary>
        /// NegativeTreatment Label 
        /// </summary>
        public ILabel NegativeTreatmentLabel => new Label(this.ComponentLocator, NegativeTreatmentLabelLocator);

        /// <summary>
        /// RecentTreatment Label 
        /// </summary>
        public ILabel RecentTreatmentLabel => new Label(this.ComponentLocator, RecentTreatmentLabelLocator);

        /// <summary>
        /// Recent Link 
        /// </summary>
        public ILink RecentLink => new Link(this.ComponentLocator, RecentLinkTLocator);
    }
}
