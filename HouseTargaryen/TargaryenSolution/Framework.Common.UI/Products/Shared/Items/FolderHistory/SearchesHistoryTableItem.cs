namespace Framework.Common.UI.Products.Shared.Items.FolderHistory
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// History Event Component for Searches
    /// </summary>
    public class SearchesHistoryTableItem : BaseGridItem
    {
        private static readonly By ContentTypeLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and contains(., 'Content: ')]");

        private static readonly By AppliedFilterLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and not(contains(., 'Content: ')) and not(contains(., 'Search Type'))]");

        private static readonly By DateEntryLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and contains(., 'Date: ')]");

        private static readonly By KeyNumberLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and contains(., 'Key Number: ')]");

        private static readonly By JurisdictionLocator = By.XPath(".//div[@class='cobalt_ro_searchJurisdiction']");

        private static readonly By PracticeAreaLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and contains(., 'Practice Area: ')]");

        private static readonly By SearchResultNumberLocator = By.XPath(".//span[@class='co_smallText']");

        private static readonly By SearchTypeLocator = By.XPath(".//div[@class='cobalt_ro_searchContent']");

        private static readonly By SearchQueryLocator = By.XPath(".//div[@class='co_keyCite_treatment']");

        private static readonly By SearchQueryEnableCheckLocator = By.XPath(".//*[contains(@class,'Title') or contains(@class,'ink')]");

        private static readonly By InfoIconLocator = By.XPath(".//span[contains(@class,'help')]");

        private static readonly By ProceedingsContentTypeLocator = By.XPath(".//div[@class='cobalt_ro_searchContent' and contains(., 'Proceedings: ')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchesHistoryTableItem"/> class. 
        /// </summary>
        /// <param name="tableContainer"> The table container </param>
        public SearchesHistoryTableItem(IWebElement tableContainer) : base(tableContainer)
        {
        }

        /// <summary>
        /// Search content labels
        /// </summary>
        public IReadOnlyCollection<ILabel> SearchContentLabels => new ElementsCollection<Label>(this.Description, SearchTypeLocator);

        /// <summary>
        /// Gets or sets the entry content.
        /// </summary>
        public string EntryContent => DriverExtensions.SafeGetElement(this.Description, ContentTypeLocator)?.Text.Trim().Replace("Content: ", string.Empty);

        /// <summary>
        /// Gets the applied filter.
        /// </summary>
        public string AppliedFilter => DriverExtensions.SafeGetElement(this.Description, AppliedFilterLocator)?.Text;

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        public string EntryDate => DriverExtensions.SafeGetElement(this.Description, DateEntryLocator)?.Text.Trim().Replace("Date: ", string.Empty);

        /// <summary>
        /// Gets or sets the entry key number.
        /// </summary>
        public string KeyNumber => DriverExtensions.SafeGetElement(this.Description, KeyNumberLocator)?.Text.Trim().Replace("Key Number: ", string.Empty);

        /// <summary>
        /// Gets or sets the jurisdiction text
        /// </summary>
        public string Jurisdiction => DriverExtensions.SafeGetElement(this.Container, JurisdictionLocator)?.Text.Trim().Replace("Jurisdiction: ", string.Empty);

        /// <summary>
        /// Gets or sets the entry practice area type.
        /// </summary>
        public string PracticeArea => DriverExtensions.SafeGetElement(this.Description, PracticeAreaLocator)?.Text.Trim().Replace("Practice Area: ", string.Empty);

        /// <summary>
        /// Gets the proceedings content.
        /// </summary>
        public string ProceedingsContent => DriverExtensions.SafeGetElement(this.Description, ProceedingsContentTypeLocator)?.Text.Trim().Replace("Proceedings: ", string.Empty);

        /// <summary>
        /// Gets or sets the search query.
        /// </summary>
        public string SearchQuery()
        {
            string queryString = DriverExtensions.SafeGetElement(this.Description, SearchQueryLocator).GetAttribute("innerText");
            return !queryString.Contains("\r\n") ? queryString : Regex.Replace(queryString.Substring(0, queryString.IndexOf("\r\n", StringComparison.Ordinal)).Trim(), Constants.NumberWithParenthesesRegex, String.Empty).Trim();
        }

        /// <summary>
        /// Gets or sets the search result number.
        /// </summary>
        public int SearchResultNumber => DriverExtensions.IsDisplayed(this.Description, SearchResultNumberLocator)
                                             ? DriverExtensions.WaitForElement(this.Description, SearchResultNumberLocator).Text.ConvertCountToInt()
                                             : 0;

        /// <summary>
        /// Gets or sets the search type.
        /// </summary>
        public string SearchType => DriverExtensions.SafeGetElement(this.Description, SearchTypeLocator)?.Text.Trim().Replace("Search Type: ", string.Empty);

        /// <summary>
        /// Is Title (Search Query) enabled
        /// </summary>
        /// <returns>
        /// Return True if the search query is enabled 
        /// </returns>
        public bool IsSearchQueryEnabled => !DriverExtensions.SafeGetElement(this.Description, SearchQueryEnableCheckLocator)?.GetAttribute("class").Contains("disabled") ??
                                              false;
        
        /// <summary>
        /// Is Info icon (?) displayed 
        /// </summary>
        /// <returns>
        /// Return True if info icon is displayed
        /// </returns>
        public bool IsInfoIconDisplayed => DriverExtensions.IsDisplayed(this.Container, InfoIconLocator);

        /// <summary>
        /// Heading element
        /// </summary>
        private IWebElement Description => DriverExtensions.GetElement(this.Container, By.XPath(this.ColumnsMap[Columns.Description].LocatorString));
    }
}
