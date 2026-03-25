namespace Framework.Common.UI.Products.WestLawNext.Components.IpTools
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Grid Component on ClaimHistoryResultPage
    /// </summary>
    public class ClaimHistoryGridComponent : IpToolsBaseGridComponent
    {
        private const string EnhancedViewPatternAdded = "greenUnderline\\sco_claimsHistory_addedMaterial\">(?<AddedParts>[^<]+)</";

        private const string EnhancedTextViewPatternAdded = "addedMaterial[^[]+(\\[\\+\\+)</span>(?<AddedParts>[^<]+)<[^>]+>(\\+\\+\\])";

        private const string EnhancedViewPatternDeleted = "deletedMaterial\">(?<DeletedParts>[^<]+)</";

        private const string EnhancedTextViewPatternDeleted = "deletedMaterial[^[]+(\\[\\-\\-)</span>(?<AddedParts>[^<]+)<[^>]+>(\\-\\-\\])";

        private static readonly By ContainerLocator = By.XPath("//table[@id='co_relatedInfo_table_claimsHistory']");

        private static readonly By ClaimStatusColumnItemsLocator = By.XPath("//td[@class='co_detailsTable_cell'][2]");

        private static readonly By WhatIsClaimedColumnItemsLocator = By.XPath("//td[@class='co_detailsTable_cell'][3]/span");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets text of all ClaimStatus columns items
        /// </summary>
        /// <returns>The list of all column items text</returns>
        public List<string> GetClaimStatusColumnItemsText()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(this.ComponentLocator), ClaimStatusColumnItemsLocator).Select(u => u.Text).ToList();

        /// <summary>
        /// Gets added text from the cell
        /// </summary>
        /// <returns>String collection</returns>
        public List<List<string>> GetAddedParts()
            => this.GetWhatIsClaimedColumnItems()
                .Select(u => this.GetAddedOrDeletedParts(u, "AddedParts", EnhancedViewPatternAdded, EnhancedTextViewPatternAdded)).ToList();

        /// <summary>
        /// Gets deleted text from the cell
        /// </summary>
        /// <returns>String collection</returns>
        public List<List<string>> GetDeletedParts()
            => this.GetWhatIsClaimedColumnItems()
                .Select(u => this.GetAddedOrDeletedParts(u, "DeletedParts", EnhancedViewPatternDeleted, EnhancedTextViewPatternDeleted)).ToList();

        /// <summary>
        /// Gets added or deleted text
        /// </summary>
        /// <param name="cell">IWebElement object</param>
        /// <param name="parts">Added or deleted to search</param>
        /// <param name="enhancedViewPattern">Pattern for added or deleted text</param>
        /// <param name="enhancedTextViewPattern">Pattern for added or deleted text</param>
        /// <returns>The list of added or deleted text</returns>
        private List<string> GetAddedOrDeletedParts(IWebElement cell, string parts, string enhancedViewPattern, string enhancedTextViewPattern)
        {
            MatchCollection enhancedViewMatches = Regex.Matches(cell.GetAttribute("innerHTML"), enhancedViewPattern);
            return enhancedViewMatches.Count != 0
                       ? enhancedViewMatches.OfType<Match>().Select(m => m.Groups[parts].Value).ToList()
                       : Regex.Matches(cell.GetAttribute("innerHTML"), enhancedTextViewPattern)
                              .OfType<Match>()
                              .Select(m => m.Groups["AddedParts"].Value)
                              .ToList();
        }

        /// <summary>
        /// Gets all cells of What Is Claimed column
        /// </summary>
        /// <returns>The list of IWebElements</returns>
        private List<IWebElement> GetWhatIsClaimedColumnItems() => DriverExtensions.GetElements(WhatIsClaimedColumnItemsLocator).ToList();
    }
}