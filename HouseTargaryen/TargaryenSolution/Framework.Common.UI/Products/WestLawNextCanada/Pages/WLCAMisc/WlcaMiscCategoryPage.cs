namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.WLCAMisc
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Elements;

    /// <summary>
    /// Represents a page CategoryPages of WLCA
    /// </summary>
    public class WlcaMiscCategoryPage : EdgeCommonSearchResultPage
    {
        private static readonly By AbridgmentLocator = By.XPath(".//*[@id='co_facet_selectLink_wlncMetaDocAbridgmentClassification']");

        private static readonly By FindDocumentLinksLocator = By.XPath(".//*[@class='co_selectable co_tocItem']//a");

        private static readonly By FindLetterLinksLocator = By.XPath(".//*[@id='coid_website_indexCharacterLinks']/li/a");

        private static readonly By OverlayBoxContainerLocator = By.XPath(".//*[@class='co_overlayBox_container']");

        private static readonly By OverlayBoxContentsLocator = By.XPath(".//*[@id='coid_scrollContainer']/div/h3");

        private static readonly By OverlayBoxTitlesLocator = By.XPath(".//*[@id='coid_scrollContainer']/div/h3");

        private static readonly By LawLinkLocator = By.XPath("//*[@id='tocItem_AILAboriginalandIndigenouslaw']/span[contains(text(), 'Aboriginal and Indigenous law')]");

        private static readonly By IconLocator = By.XPath(".//*[@id='co_scopeIcon_AIL']");

        private static readonly By CloseButtonLocator = By.XPath(".//*[@id='co_closeScopeWindowTop']");

        private static readonly By MiscellaneousLocator = By.XPath(".//span[contains(text(),' Miscellaneous')]/parent::div/preceding-sibling::a");

        private static readonly By ApplyButtonLocator = By.XPath(".//*[contains(@id,'co_facet_keynumber_') and  @class='co_primaryBtn']");

        private static readonly By MyStartpageLocator = By.XPath(".//*[@id='coid_setAsHomePageElement']");

        private static readonly By LawSourceLocator = By.XPath(".//*[@id='crsw_startPageLink']");

        private static readonly By DirectHistoryLocator = By.XPath(".//*[@class='co_relatedInfo_history_subheading']");

        private static readonly By CitingReferenceLocator = By.XPath(".//*[@id='co_docToolbarMenuLeft']");

        private static readonly By IsCourtImgIconLinkLocator = By.XPath(".//*[@id='crsw_courtDocIcon']/a/img");

        private static readonly By IsCourtIconLinkLocator = By.XPath(".//*[@id='crsw_courtDocIcon']/a");

        private static readonly By IsLMIconLinkLocator = By.XPath(".//*[@id='crsw_legalMemoIcon']/a");

        private static readonly By SynopsisLinkLocator = By.XPath(".//*[@id='co_searchResults_citation_1']");

        private static readonly By FirstTitleLocator = By.XPath(".//*[@id='cobalt_result_can_cases_title1'] | //*[@id='cobalt_result_can_casesWithoutDecisions_title1'] ");

        private static readonly By SecondTitleLocator = By.XPath(".//*[@id='cobalt_result_can_cases_title2']");

        private static readonly By SearchWithinTermLocator = By.XPath(".//*[@class='co_searchWithinTerm']");

        private static readonly By DocumentLinkLocator = By.XPath(".//*[@id='co_docHeaderTitle']//a");

        /// <summary>
        /// Header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// First abridgement Link
        /// </summary>
        public ILink AbridgementLink => new Link(AbridgmentLocator);

        /// <summary>
        /// Icon locator
        /// </summary>
        public ILink IconByLink => new Link(IconLocator);

        // Define a property for the icon locator
        /// <summary>
        /// ElementExtensions Section component
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// MiscellaneousByLink
        /// </summary>
        public ILink MiscellaneousByLink => new Link(MiscellaneousLocator);

        /// <summary>
        /// ApplyButton
        /// </summary>
        public IButton ApplyButton => new Button(ApplyButtonLocator);

        /// <summary>
        /// LawLinklocator
        /// </summary>
        public ILink LawLink => new Link(LawLinkLocator);

        /// <summary>
        /// OverlayBoxByLabel
        /// </summary>
        public ILabel OverlayBoxByLabel => new Label(OverlayBoxContainerLocator);

        /// <summary>
        /// MyStartPagebutton
        /// </summary>
        public IButton MyStartPageButton => new Button(MyStartpageLocator);

        /// <summary>
        /// OverlayBoxContentsByLabel
        /// </summary>IButton
        public ILabel OverlayBoxContentsByLabel => new Label(OverlayBoxContentsLocator);

        /// <summary>
        /// OverlayBoxTitles
        /// </summary>
        public ILabel OverlayBoxTitlesByLabel => new Label(OverlayBoxTitlesLocator);

        /// <summary>
        /// LawSourceBYlink
        /// </summary>
        public ILink LawSourceByLink => new Link(LawSourceLocator);

        /// <summary>
        ///DirectHistoryByLabel
        /// </summary>
        public ILabel DirectHistoryByLabel => new Label(DirectHistoryLocator);

        /// <summary>
        /// BreadcrumbRangeLabel
        /// </summary>
        public ILabel CitingReferenceByLabel => new Label(CitingReferenceLocator);

        /// <summary>
        /// IsIconlink
        /// </summary>
        public ILink IsCourtImgIconByLink => new Link(IsCourtImgIconLinkLocator);

        /// <summary>
        /// IsIconlink
        /// </summary>
        public ILink IsCourtIconByLink => new Link(IsCourtIconLinkLocator);

        /// <summary>
        /// IsIconlink
        /// </summary>
        public ILink IsLMIconByLink => new Link(IsLMIconLinkLocator);

        /// <summary>
        /// Header
        /// </summary> 
        public ILink SynopsisByLink => new Link(SynopsisLinkLocator);

        /// <summary>
        /// Header
        /// </summary> 
        public ILink FirstTitleLink => new Link(FirstTitleLocator);

        /// <summary>
        /// Header
        /// </summary> 
        public ILink SecondTitleLink => new Link(SecondTitleLocator);

        /// <summary>
        /// Header
        /// </summary> 
        public ILabel SearchWithinTermLabel => new Label(SearchWithinTermLocator);

        /// <summary>
        /// BreadcrumbRangeLabel
        /// </summary>
        public ILink DocumentLink => new Link(DocumentLinkLocator);

        /// <summary>
        /// List of DocumentLinkslist
        /// </summary>
        public IReadOnlyCollection<ILink> FindDocumentLinks => new ElementsCollection<Link>(FindDocumentLinksLocator);

        /// <summary>
        /// Get the first letter of each document link
        /// </summary>
        /// <returns>List of first letters of document links</returns>
        public List<char> GetFirstLettersOfDocumentLinks()
        {
            List<char> firstLetters = new List<char>();

            foreach (var link in FindDocumentLinks)
            {
                char firstLetter = link.Text.Trim()[0];
                firstLetters.Add(firstLetter);
            }

            return firstLetters;
        }

        /// <summary>
        /// List of DocumentLinkslist
        /// </summary>
        public IReadOnlyCollection<ILink> FindLetterLinks => new ElementsCollection<Link>(FindLetterLinksLocator);

        /// <summary>
        /// Check if the letter links correspond to the first letters of document links
        /// </summary>
        /// <param name="firstLetters">List of first letters of document links</param>
        /// <returns>True if the letter links match the first letters of document links, otherwise false</returns>
        public bool AreLetterLinksMatchedToDocumentLinks(List<char> firstLetters)
        {
            List<char> allLetterLinks = new List<char>();

            foreach (var link in FindLetterLinks)
            {
                char firstLetter = link.Text.Trim()[0];
                allLetterLinks.Add(firstLetter);
            }

            return firstLetters.Select(c => char.ToUpper(c)).Distinct().ToList().SequenceEqual(allLetterLinks);
        }

        /// <summary>
        /// Checks that all the letter links correspond unique Document links starting with the letter
        /// </summary>
        public bool AbcApplicableLetterLinksDisplayed()
        {
            List<char> firstLetters = GetFirstLettersOfDocumentLinks();
            return AreLetterLinksMatchedToDocumentLinks(firstLetters);
        }

        /// <summary>
        /// Check if the letter links are working correctly
        /// </summary>
        /// <returns>True if all letter links are working, otherwise false</returns>
        public bool AreLetterLinksWorking()
        {
            // Array of alphabet characters
            char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWY".ToCharArray();

            // List to store prime document links for each letter
            List<ILink> AllDocLinksPrimeLinks = new List<ILink>();

            // List to store the first letter of each document link
            List<char> AllDocLinksFirstLetter = new List<char>();

            // Get the first letter of each document link
            foreach (var link in FindDocumentLinks)
            {
                AllDocLinksFirstLetter.Add(link.Text.Trim()[0]);
            }

            // Iterate through each letter of the alphabet
            foreach (char letter in alpha)
            {
                // Get the index of the first occurrence of the letter in document links
                int index = AllDocLinksFirstLetter.IndexOf(letter);

                // If the letter is found in document links
                if (index != -1)
                {
                    // Iterate through document links starting from the index of the letter
                    for (int j = index; j < FindDocumentLinks.Count; j++)
                    {
                        // Get the document link that starts with the current letter
                        var docLink = FindDocumentLinks.FirstOrDefault(link => link.Text.Trim().StartsWith(letter.ToString()));

                        // Check if a document link is found
                        if (docLink != null)
                        {
                            // Add the document link to the list of prime links
                            AllDocLinksPrimeLinks.Add(docLink);
                            break;
                        }
                    }
                }
            }

            // Variable to track if all letter links are working
            bool areLetterLinksWorking = true;

            // Iterate through each letter link
            for (int k = FindLetterLinks.Count - 1; k >= 0; k--)
            {
                // Click on the letter link                
                FindLetterLinks.ElementAt(k).Click();

                // Check if the corresponding document link is in view
                areLetterLinksWorking &= IsView(AllDocLinksPrimeLinks[k], 0, 0);
            }

            return areLetterLinksWorking;
        }

        /// <summary>
        /// Checks if a specified web element is within the visible viewport of the browser window,
        /// considering optional top and bottom buffer spaces.
        /// </summary>        
        /// <returns></returns>
        public Boolean IsView(ILink link, int topBuffer, int bottomBuffer)
        {
            var element = DriverExtensions.GetElement(By.LinkText(link.Text));
            StringBuilder script = new StringBuilder();
            script.Append("var scrollStart = $(window).scrollTop(); ");
            script.Append("var scrollEnd = $(window).scrollTop() + $(document.body).height(); ");
            script.Append("var elementYPos = Math.round($(arguments[0]).offset().top); ");
            script.Append("return scrollStart + arguments[1] <= elementYPos && elementYPos <= scrollEnd + arguments[2];");
            return (bool)DriverExtensions.ExecuteScript(script.ToString(), element, topBuffer, bottomBuffer);
        }
    }
}


