namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.RelatedInfo
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNextCanada.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial Consideration (Notes of Decisions) tab Page
    /// </summary>
    public class JudicialConsiderationPage : EdgeNotesOfDecisionsPage
    {
        private const string HierarchyTreeLeafNodeLctMask = "//b[text()='{0}']";
        
        private const string HierarchyTreeInternalNodeLctMask = "facet_div_AbridgmentTopics{0}Link";

        private static readonly By AbridgmentLocator = By.XPath("(//img[@alt='Book Symbol']/following-sibling::a)[1]");

        private static readonly By SubNodLocator = By.CssSelector("p.noteText");

        private static readonly By SortByOptionsLocator = By.XPath("//ul[@id='co_search_sortOptions']/li/span");

        private static readonly By NoFilterResultsMessageLocator = By.Id("coid_nod_ContentResponse");

        private static readonly By UndoFiltersLinkLocator = By.Id("coid_relatedInfo_facet_undoFilter_link");

        private static readonly By JudicialConsiderationBannerLocator = By.CssSelector("a.co_notesOfDecisionsLink");

        private static readonly By SortByButtonLocator = By.CssSelector("button.a11yDropdown-button");

        private static readonly By AbridgementClassificationContinueLocator = By.Id("co_facet_AbridgmentTopics_continueButton");

        private static readonly By AbridgementClassificationFilterLocator = By.Id("co_facet_AbridgmentTopics_filterButton");

        /// <summary>
        /// Sort By Button
        /// </summary>
        public IButton SortByBtn => new Button(SortByButtonLocator);

        /// <summary>
        /// Abridgement Classification Continue Button
        /// </summary>
        public IButton AbridgementClassificationContinue => new Button(AbridgementClassificationContinueLocator);

        /// <summary>
        /// Abridgement Classification Continue Button
        /// </summary>
        public IButton AbridgementClassificationFilter => new Button(AbridgementClassificationFilterLocator);

        /// <summary>
        /// First abridgement Link
        /// </summary>
        public ILink AbridgementLink => new Link(AbridgmentLocator);

        /// <summary>
        /// Judicial Consideration Banner Link
        /// </summary>
        public ILink JudicialConsiderationBannerLink => new Link(JudicialConsiderationBannerLocator);
        
        /// <summary>
        /// Gets no filters present message
        /// </summary>
        public ILabel NoResultsWithAppliedFiltersMessage => new Label(NoFilterResultsMessageLocator);

        /// <summary>
        /// Undo filters link
        /// </summary>
        public ILink UndoFiltersLink => new Link(UndoFiltersLinkLocator);

        /// <summary>
        /// Sort dropdown
        /// </summary>
        public JcSortDropdown JcSortDropdown => new JcSortDropdown(DriverExtensions.GetElement(SortByOptionsLocator));

        /// <summary>
        /// Returns first 8 letters from the first NOD paragraph
        /// </summary>
        /// <returns>Text from first NOD paragraph</returns>
        public string GetTextFromFirstNodParagraph() => DriverExtensions.GetText(SubNodLocator).Trim().Substring(0, 10);

        /// <summary>
        /// Gets count of Nods
        /// </summary>
        /// <returns> Count of Nods </returns>
        public int GetCountOfNods() => DriverExtensions.GetElements(SubNodLocator).Count;

        /// <summary>
        /// Leaf node in hierarchy
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        public ILink HierarchyTreeLeafNodeLink(string link) => new Link(By.XPath(string.Format(HierarchyTreeLeafNodeLctMask, link)));

        /// <summary>
        /// Internal abridgement Link
        /// </summary>
        /// <param name="link">
        /// The link.
        /// </param>
        /// <returns>
        /// The <see cref="ILink"/>.
        /// </returns>
        public ILink HierarchyTreeInternalNodeLink(string link) => new Link(By.Id(string.Format(HierarchyTreeInternalNodeLctMask, link)));

        /// <summary>
        /// Select abridgement classification
        /// </summary>
        /// <param name="hierarchyLinks"></param>
        public void SelectAbridgmentClassifications(List<string> hierarchyLinks)
        {
            string linkText;
            for (int i = 0; i < hierarchyLinks.Count; i++)
            {
                linkText = hierarchyLinks[i].Substring(0, hierarchyLinks[i].IndexOf(" ", StringComparison.Ordinal));
                if ((hierarchyLinks.Count - 1).Equals(i))
                {
                    this.HierarchyTreeLeafNodeLink(linkText).Click();
                }
                else
                {
                    this.HierarchyTreeInternalNodeLink(linkText).Click();
                }
            }
        }
    }
}
