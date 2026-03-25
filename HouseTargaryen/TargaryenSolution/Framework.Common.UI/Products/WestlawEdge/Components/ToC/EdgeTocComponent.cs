namespace Framework.Common.UI.Products.WestlawEdge.Components.ToC
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.FocusHighlighting;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The indigo toc component.
    /// </summary>
    public class EdgeTocComponent : BaseEdgeTocComponent
    {
        private const string TocBlockLinkLctMask = "//div[@class='TocEntryContent']/a/span[text()={0}]";
        private const string TocBlockHighlightLinkLctMask = "//div[@class='TocEntryWrapper TocEntryHighlight']/div/a/span[text()={0}]";
        private const string TocPreviewTextLctMask = "//span[@class='TocPreviewText' and text()={0}]";
        private const string TocToolTipLocatorMask = "//div[@id='{0}']";
        private const string TocCheckBoxLctMask = "//*[text()='{0}']/preceding-sibling :: input";
        private const string TocHeadingLctMask = "//span[text()='{0}']";
        private static readonly By TocCollapseLocator = By.XPath("//span[@class='TocSectionToggleIcon Icon-expanded']");
        private static readonly By TocLinkLocator = By.Id("co_tocLink");
        private static readonly By ContainerLocator = By.Id("co_tocContainer");
        private static readonly By HeatmapSectionLocator = By.XPath("//div[@class = 'co_heatmapSection']");
        private static readonly By TocElementLocator = By.XPath("//div[@class='TocEntryContent']/a");
        private static readonly By TocIcon = By.XPath("//*[@id=\"co_tocLink\"]/span");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The toc title text.
        /// </summary>
        public string TitleText => DriverExtensions.GetText(TocLinkLocator);

        /// <summary>
        /// Focus Highlight selected terms component
        /// </summary>
        public FocusHighlightSelectedTermsComponent SelectedTermsComponent { get; } = new FocusHighlightSelectedTermsComponent();

        /// <summary>
        /// List of heatmap sections
        /// </summary>
        public List<HeatmapSectionComponent> ListOfHeatmapSections =>
            DriverExtensions.GetElements(HeatmapSectionLocator).Select(item => new HeatmapSectionComponent(item)).ToList();

        /// <summary>
        /// Is list of heatmaps sections displayed
        /// </summary>
        /// <returns>True if </returns>
        public bool IsListOfHeatmapSectionsDisplayed() => DriverExtensions.IsDisplayed(HeatmapSectionLocator);

        /// <summary>
        /// The is toc block displayed.
        /// </summary>
        /// <param name="blockTitle">
        /// The block title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocBlockDisplayed(string blockTitle) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(TocBlockLinkLctMask, blockTitle));

        /// <summary>
        /// The is toc block displayed.
        /// </summary>
        /// <param name="blockTitle">
        /// The block title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocBlockHighlightDisplayed(string blockTitle) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(TocBlockHighlightLinkLctMask, blockTitle));

        /// <summary>
        /// Get Title Of TOC toggle
        /// </summary>
        /// <returns> Title Of TOC toggle </returns>
        public string GetTitleOfTocToggle()
        {
            string tocTooltipID=DriverExtensions.WaitForElement(TocLinkLocator).GetAttribute("aria-describedby");
            DriverExtensions.Hover(TocLinkLocator);
            return DriverExtensions.GetElement(By.XPath(string.Format(TocToolTipLocatorMask, tocTooltipID))).Text;
        }

        /// <summary>
        /// Expand/Collapse TOC toggle
        /// </summary>
        /// <param name="state"> state </param>
        public void ToggleExpand(bool state)
        { 
            if (state != this.IsTocIconExpanded())
            {
                DriverExtensions.WaitForElement(TocLinkLocator).Click();
            }
        }

        /// <summary>
        /// The is toc collapse displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocCollapseDisplayed() => DriverExtensions.IsDisplayed(TocCollapseLocator);

        /// <summary>
        /// The is toc preview text displayed.
        /// </summary>
        /// <param name="title">
        /// The block title.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTocPreviewTextDisplayed(string title) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(TocPreviewTextLctMask, title));

        /// <summary>
        /// Click all collapse locators to expand all Toc 
        /// </summary>
        public void CollapseAllTocEntries()
        {
            while (DriverExtensions.IsDisplayed(TocCollapseLocator))
            {
                DriverExtensions.Click(DriverExtensions.WaitForElement(TocCollapseLocator));
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Gets a list of the toc titles
        /// </summary>
        /// <returns>List of toc elements</returns>
        public List<string> GetTocTitleElements()
        {
            var tocList = new List<string>();
            IEnumerable<IWebElement> elements = DriverExtensions.GetElements(TocElementLocator).Where(s => !string.IsNullOrWhiteSpace(s.Text));
            foreach (IWebElement element in elements)
            {
                tocList.Add(GetIdentLevel(element) + element.GetAttribute("title").Replace("\r\n", " ").Replace("…", ""));
            }
            return tocList;
        }

        /// <summary>
        /// Gets a list of the toc text first 40 char
        /// </summary>
        /// <returns>List of toc elements</returns>
        public List<string> GetTocTextElements()
        {
            var tocList = new List<string>();
            IEnumerable<IWebElement> elements = DriverExtensions.GetElements(TocElementLocator).Where(s => !string.IsNullOrWhiteSpace(s.Text));
            foreach (IWebElement element in elements)
            {
                string elementText = GetIdentLevel(element) + element.Text.Replace("\r\n", " ").Replace("…", "");
                tocList.Add(elementText.Substring(0,Math.Min(elementText.Length,40)));
            }
            return tocList;
        }

        /// <summary>
        /// Toc icon is collapsed
        /// </summary>
        /// <returns>
        /// /// The <see cref="bool"/>
        /// </returns>
        public bool IsTocIconCollapsed() => DriverExtensions.GetElement(TocLinkLocator).GetAttribute("aria-expanded").Equals("false");

        /// <summary>
        /// Toc icon is expanded
        /// </summary>
        /// <returns>
        /// /// The <see cref="bool"/>
        /// </returns>
        public bool IsTocIconExpanded() => DriverExtensions.GetElement(TocLinkLocator).GetAttribute("aria-expanded").Equals("true");

        /// <summary>
        /// Set Toc Content Checkbox
        /// </summary>
        /// <param name="contentHeadingName">
        /// Toc Content Heading Name
        /// </param>
        /// <param name="selected">
        /// checkbox select state - true or false
        /// </param>
        public void SetTocContentCheckBox(string contentHeadingName, bool selected) 
            => DriverExtensions.WaitForElement(By.XPath(string.Format(TocCheckBoxLctMask, contentHeadingName))).SetCheckbox(selected);

        /// <summary>
        /// Click on Toc Content Heading to Expand it
        /// </summary>
        /// <param name="headingName">
        /// Toc Content Heading Name
        /// </param>
        public void ExpandTocContentHeading(string headingName) 
            => DriverExtensions.GetElement(By.XPath(string.Format(TocHeadingLctMask, headingName))).Click();

        /// <summary>
        /// Gets IdentLevel value
        /// </summary>
        /// <param name="element">element</param>
        /// <returns>IdentLevel</returns>
        private string GetIdentLevel(IWebElement element)
        {
            string indentLevel = "";
            IWebElement parent = element.GetParentElement(".TocList");
            while (parent != null)
            {
                parent = parent.GetParentElement(".TocList");
                if (parent != null)
                    indentLevel = indentLevel + "--";
            }

            return indentLevel;
        }

        /// <summary>
        /// Hovers over TOC.
        /// </summary>
        public void HoverOverToc()
        {
            var tocHover = DriverExtensions.GetElement(TocIcon);
            tocHover.ScrollToElement();
            DriverExtensions.WaitForJavaScript();
            tocHover.SeleniumHover();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click to close the expanded Toc 
        /// </summary>
        public void ExpandTocPane(bool state)
        {
                if(state == this.IsTocIconCollapsed())
                DriverExtensions.Click(DriverExtensions.WaitForElement(TocIcon));
                DriverExtensions.WaitForJavaScript();
        }
    }
}