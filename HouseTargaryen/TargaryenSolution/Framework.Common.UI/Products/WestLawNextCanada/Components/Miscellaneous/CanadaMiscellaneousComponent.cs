namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Miscellaneous component on the Westlaw Edge Canada HomePage
    /// </summary>
    public class CanadaMiscellaneousComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//*[@id='co_mainContainer']");
        private static readonly By SpecifyContentCheckboxLocator = By.XPath(".//input[@id='coid_browseShowCheckboxes']");
        private static readonly By SelectAllContentCheckboxLocator = By.XPath(".//input[@id='coid_browseSelectAllCheckboxInput']");
        private static readonly By ViewHeadnotesButtonLocator = By.XPath(".//input[@id='co_viewHeadnotesButton']");
        private static readonly By BreadCrumbTrailLabelLocator = By.XPath(".//*[@id='coid_website_breadCrumbTrail']");
        private static readonly By RightPaneLinkLocator = By.XPath(".//a[@id='crsw_rightPaneAbridgmentClassificationsLink_1']");
        private static readonly By LegalMemoLinkLocator = By.XPath(".//a[@id='cobalt_result_can_legalmemo_title4']");
        private static readonly By LegalTopicsHeaderLabelLocator = By.XPath(".//*[@id='crsw_canadianAbridgment']//strong");
        private static readonly By RightFrameLinksLocator = By.XPath(".//div[@class='co_genericBoxContent']");
        private static readonly By TocHeaderLabelLocator = By.CssSelector(".crw_tocSelectedNode");
        private static readonly By ImmigCitizenLinkLocator = By.XPath(".//*[contains(@class,'co_link co_drag')]");
        private static readonly By PageHeaderLabelLocator = By.XPath(".//*[@id='co_browsePageLabel']");
        private static readonly By LeafNodeLinkLocator = By.XPath(".//*[@class='co_browseTocHeading ']");
        private static readonly By CaseLawLMIconButtonLocator = By.XPath(".//*[@class='co_legalMemo']");
        private static readonly By CaseLawLinksLocator = By.XPath(".//*[contains(@id,'cobalt_result_can_cases_title')]");
        private static readonly By LegalMemoIconButtonLocator = By.XPath(".//*[@id='crsw_legalMemoIcon']");
        private static readonly By LegislationLinksLocator = By.XPath(".//*[contains(@id,'cobalt_result_can_statutesAndRegs_title')]");
        private static readonly By TextAndAnnotationsDocHeaderLinkLocator = By.XPath(".//*[@id='titleInfo']//a");
        private static readonly By TextAndAnnotationsDocBodyLabelLocator = By.XPath(".//*[@class='co_title']//div");
        private static readonly By PublicationDocHeaderLabelLocator = By.XPath(".//*[contains(@id,'headerInfo')]");
        private static readonly By PublicationDocBodyLabelLocator = By.XPath(".//*[contains(@class,'co_contentBlock co_propBlock')]//div");
        private static readonly By DeliveryListLocator = By.XPath(".//*[@class='crsw_forceTableColumnDeliveryWidth']");
        private static readonly By CompanyNameLabelLocator = By.XPath(".//*[@id='titleInfo']//a");
        private static readonly By HeaderNamesListLocator = By.XPath(".//*[contains(@id,'headerInfo_')]");
        private static readonly By PrelimCiteLabelLocator = By.XPath(".//div[@class='co_cites']");
        private static readonly By AllResultsLinkLocator = By.XPath(".//a[@id='cobalt_result_can_abridgment_cases_title1']");
        private static readonly By LegalMemoTitleLinkLocator = By.XPath(".//a[@id='cobalt_result_can_ced_title1']");
        private static readonly By DocumentTitleLabelLocator = By.XPath(".//span[@class='co_search_titleCount']/..");

        /// <summary>
        /// Miscellaneous Tab Panel
        /// </summary>
        public CanadaMiscellaneousTabPanel MiscellaneousTabPanel { get; } = new CanadaMiscellaneousTabPanel();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///SpecifyContent Checkbox 
        /// </summary>
        public ICheckBox SpecifyContentCheckbox => new CheckBox(this.ComponentLocator, SpecifyContentCheckboxLocator);

        /// <summary>
        /// SelectAll Content Checkbox
        /// </summary>
        public ICheckBox SelectAllContentCheckbox => new CheckBox(this.ComponentLocator, SelectAllContentCheckboxLocator);

        /// <summary>
        /// ViewHeadnotes Button
        /// </summary>
        public IButton ViewHeadnotesButton => new Button(this.ComponentLocator, ViewHeadnotesButtonLocator);

        /// <summary>
        /// BreadCrumbTrail Label
        /// </summary>
        public ILabel BreadCrumbTrailLabel => new Label(this.ComponentLocator, BreadCrumbTrailLabelLocator);

        /// <summary>
        /// RightPane Link
        /// </summary>
        public ILink RightPaneLink => new Link(this.ComponentLocator, RightPaneLinkLocator);

        /// <summary>
        /// LegalMemoLink Locator
        /// </summary>
        public ILink LegalMemoLink => new Link(this.ComponentLocator, LegalMemoLinkLocator);

        /// <summary>
        /// LegalTopicsHeader Label
        /// </summary>
        public ILabel LegalTopicsHeaderLabel => new Label(this.ComponentLocator, LegalTopicsHeaderLabelLocator);

        /// <summary>
        /// RightFrame Links
        /// </summary>
        public IReadOnlyCollection<ILink> RightFrameLinks => new ElementsCollection<Link>(this.ComponentLocator, RightFrameLinksLocator);

        /// <summary>
        /// TocHeader Locator
        /// </summary>
        public ILabel TocHeaderLabel => new Label(this.ComponentLocator, TocHeaderLabelLocator);

        /// <summary>
        /// Immigration Citizenship Links
        /// </summary>
        public IReadOnlyCollection<ILink> ImmigCitizenLink => new ElementsCollection<Link>(this.ComponentLocator, ImmigCitizenLinkLocator);

        /// <summary>
        /// PageHeader Label 
        /// </summary>
        public ILabel PageHeaderLabel => new Label(this.ComponentLocator, PageHeaderLabelLocator);

        /// <summary>
        ///  DocumentTitle Label
        /// </summary>
        public ILabel DocumentTitleLabel => new Label(this.ComponentLocator, DocumentTitleLabelLocator);

        /// <summary>
        /// LeafNode Link
        /// </summary>
        public IReadOnlyCollection<ILink> LeafNodeLink => new ElementsCollection<Link>(this.ComponentLocator, LeafNodeLinkLocator);

        /// <summary>
        /// CaseLawLM Icon 
        /// </summary>
        public IReadOnlyCollection<IButton> CaseLawLMIcon => new ElementsCollection<Button>(this.ComponentLocator, CaseLawLMIconButtonLocator);

        /// <summary>
        /// CaseLaw Links 
        /// </summary>
        public IReadOnlyCollection<ILink> CaseLawLinks => new ElementsCollection<Link>(this.ComponentLocator, CaseLawLinksLocator);

        /// <summary>
        /// LegalMemoIcon Button
        /// </summary>
        public IButton LegalMemoIcon => new Button(this.ComponentLocator, LegalMemoIconButtonLocator);

        /// <summary>
        /// Legislation Links 
        /// </summary>
        public IReadOnlyCollection<ILink> LegislationLinks => new ElementsCollection<Link>(this.ComponentLocator, LegislationLinksLocator);

        /// <summary>
        /// TextAndAnnotations DocHeader Link
        /// </summary>
        public ILink TextAndAnnotationsDocHeader => new Link(this.ComponentLocator, TextAndAnnotationsDocHeaderLinkLocator);

        /// <summary>
        /// TextAndAnnotations DocBody Link
        /// </summary>
        public ILabel TextAndAnnotationsDocBody => new Label(this.ComponentLocator, TextAndAnnotationsDocBodyLabelLocator);

        /// <summary>
        /// Publication DocHeader  
        /// </summary>
        public IReadOnlyCollection<ILabel> PublicationDocHeader => new ElementsCollection<Label>(this.ComponentLocator, PublicationDocHeaderLabelLocator);

        /// <summary>
        /// Publication DocBody  
        /// </summary>
        public IReadOnlyCollection<ILabel> PublicationDocBody => new ElementsCollection<Label>(this.ComponentLocator, PublicationDocBodyLabelLocator);

        /// <summary>
        /// ColumnDelivery List  
        /// </summary>
        public IReadOnlyCollection<ILabel> DeliveryList => new ElementsCollection<Label>(this.ComponentLocator, DeliveryListLocator);

        /// <summary>
        /// CompanyName Label 
        /// </summary>
        public ILabel CompanyNameLabel => new Label(this.ComponentLocator, CompanyNameLabelLocator);

        /// <summary>
        /// Header List  
        /// </summary>
        public IReadOnlyCollection<ILabel> HeaderNamesList => new ElementsCollection<Label>(this.ComponentLocator, HeaderNamesListLocator);

        /// <summary>
        /// PrelimCite Label 
        /// </summary>
        public ILabel PrelimCiteLabel => new Label(this.ComponentLocator, PrelimCiteLabelLocator);

        /// <summary>
        /// ALResultLink 
        /// </summary>
        public ILink AllResultLink => new Link(this.ComponentLocator, AllResultsLinkLocator);

        /// <summary>
        /// LegalMemoLink 
        /// </summary>
        public ILink LegalMemoTitleLink => new Link(this.ComponentLocator, LegalMemoTitleLinkLocator);
    }
}
