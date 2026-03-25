namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    
    using OpenQA.Selenium;

    /// <summary>
    /// Dockets Category Page
    /// </summary>
    public class DocketsCategoryPage : CommonBrowsePage
    {
        private static readonly By SearchDocketsPdfsAndCourtFilingsCheckboxLocator = By.XPath("//input[contains(@id, '_SEARCHABLEDOCKETPDFS')]");
        private static readonly By SearchDocketsPdfsAndCourtFilingsInfoboxLocator = By.XPath("//div[contains(@class, 'SPDF-onboardingInfobox')]");
        private static readonly By SearchDocketsPdfsAndCourtFilingsInfoIconLocator = By.XPath("//button[@class = 'SPDF-informationButton']");
        private static readonly By DocketsByTopicCheckboxesLocator = By.XPath("//fieldset[legend/*[@id='co_browseItemHeading_Dockets by Topic']]//input");
        private static readonly By SearchSeparatelyLocator = By.XPath("//h2[@id='co_browseItemHeading_Excluded: Search Separately']/parent::legend/following-sibling::div//li");

        /// <summary>
        /// 'Search dockets, available PDFs, and court filings' checkbox
        /// </summary>
        public ICheckBox SearchDocketsPdfsAndCourtFilingsCheckbox => new CheckBox(SearchDocketsPdfsAndCourtFilingsCheckboxLocator);

        /// <summary>
        /// 'Search dockets, available PDFs, and court filings' info box
        /// </summary>
        public IInfoBox SearchDocketsPdfsAndCourtFilingsInfoBox => new InfoBox(SearchDocketsPdfsAndCourtFilingsInfoboxLocator);

        /// <summary>
        /// 'Search dockets, available PDFs, and court filings' info icon
        /// </summary>
        public IButton SearchDocketsPdfsAndCourtFilingsInfoIcon => new Button(SearchDocketsPdfsAndCourtFilingsInfoIconLocator);

        /// <summary>
        /// Dockets by topic checkboxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> DocketsByTopicCheckboxes => new ElementsCollection<CheckBox>(DocketsByTopicCheckboxesLocator);

        /// <summary>
        /// Excluded: Search Separately links
        /// </summary>
        public IReadOnlyCollection<ILink> SearchSeparatelylinks => new ElementsCollection<Link>(SearchSeparatelyLocator);
    }
}