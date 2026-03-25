namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.Document;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using System.Collections.Generic;
    using Framework.Common.UI.Products.WestlawEdgePremium.WrapperElements;

    /// <summary>
    /// Precision document page with headnotes
    /// </summary>
    public class PrecisionDocumentPageWithHeadnotes : EdgeDocumentPageWithHeadnotes
    {
        private const string ParagraphHighligtingStripedFlagLctMask = "//a[@id = 'co_partiallyOverruled_{0}_arrow']";
        private const string CitationLinkLctMask = "//a[contains(@class,'ui-draggable') and contains(text(),'{0}')]";

        private static readonly By BlueBoxInfoboxLocator = By.XPath("//div[@class='Athens-headnotesPopupWrapper']/div[not(contains(@class, 'co_hideState'))]");
        private static readonly By CompassIconsLocator = By.XPath(".//span[contains(@class, 'icon_compass-blue')]");
        private static readonly By InternalHeadnoteLinkLocator = By.XPath("//a[contains(@id, 'co_pp_HNF') and contains(@class,'co_internalLink co_headnoteLink')]");
        private static readonly By OutlineBuilderDropTargetLocator = By.XPath("//*[@id='co_outlinePanelContainer']/div/div[1]");
        private static readonly By OutlineBuilderSelectedTextDragLocator = By.XPath("//button[contains(@class, 'ui-draggable') and contains(@class, 'SelectedText-drag')] | //button[contains(@class, 'drag co_secondaryBtn co_drag_1') and contains(@class, 'SelectedText-drag')]");
        private static readonly By OvveruledInPartBorderLocator = By.XPath("//div[contains(@class, 'PartiallyOverruledWrapper')]");

        /// <summary>
        /// The Athens West Headnotes component
        /// </summary>
        public PrecisionWestHeadnotesComponent WestHeadnotes => new PrecisionWestHeadnotesComponent();

        /// <summary>
        /// Internal headnote links
        /// </summary>
        public IReadOnlyCollection<ILink> InternalHeadnoteLinks => new ElementsCollection<Link>(InternalHeadnoteLinkLocator);

        /// <summary>
        /// Blue box infobox
        /// </summary>
        public IPrecisionDocumentBlueBoxInfobox DocumentBlueBoxInfobox => new PrecisionDocumentBlueBoxInfobox(BlueBoxInfoboxLocator);

        /// <summary>
        /// Get number of compass icons on the page
        /// </summary>
        /// <returns>number of item</returns>
        public int GetNumberOfCompassIcons => DriverExtensions.GetElements(CompassIconsLocator).Count;

        /// <summary>
        /// Is paragraph highlighting has partially ovveruled border
        /// </summary>
        /// <returns>bool</returns>
        public bool IsPartiallyOveruledBorderPresent => DriverExtensions.IsElementPresent(OvveruledInPartBorderLocator);

        /// <summary>
        /// Is partially ovveruled border in view
        /// </summary>
        /// <returns>bool</returns>
        public bool IsPartiallyOveruledBorderInView => DriverExtensions.WaitForElement(OvveruledInPartBorderLocator).IsElementInView();

        /// <summary>
        /// click the  paragraph highlighting 
        /// </summary>
        /// <returns></returns>
        public bool IsPartiallyOverruledFlagInView(int number) => DriverExtensions.WaitForElement(By.XPath(string.Format(ParagraphHighligtingStripedFlagLctMask, number))).IsElementInView();
              
        /// <summary>
        /// click the  paragraph highlighting 
        /// </summary>
        /// <returns></returns>
        public PrecisionOverrulingFlagDialog ClickPartiallyOverruledFlag(int number)
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ParagraphHighligtingStripedFlagLctMask, number))).CustomClick();
            return DriverExtensions.CreatePageInstance<PrecisionOverrulingFlagDialog>();
        }

        /// <summary> 
        /// Drag Selected Snippet to Right Panel Outline Builder
        /// </summary>
        public void DragAndDropSelectedSnippetToOutlineBuilder()
        {
            var dragElement = DriverExtensions.GetElement(OutlineBuilderSelectedTextDragLocator);
            var dropElement = DriverExtensions.GetElement(OutlineBuilderDropTargetLocator);

            DriverExtensions.DragAndDrop(dropElement, dragElement);
        }

        /// <summary> 
        /// Drag citation link to Right Panel Outline Builder
        /// </summary>
        /// <param name="citationText">The citation text or partial text</param>
        public void DragAndDropCitationLinkToOutlineBuilder(string citationText)
        {
            By citationLink = By.XPath(string.Format(CitationLinkLctMask, citationText));
            DriverExtensions.ScrollIntoView(citationLink);// Link has be visible for dragging to work
           
            var dragElement = DriverExtensions.GetElement(citationLink);
            var dropElement = DriverExtensions.GetElement(OutlineBuilderDropTargetLocator);

            DriverExtensions.DragAndDrop(dropElement, dragElement);
        }

        /// <summary> 
        /// Check if drag handle is visible
        /// </summary>
        public bool IsDragAndDropHandleVisible() => DriverExtensions.GetElement(OutlineBuilderSelectedTextDragLocator).IsDisplayed();
    }
}
