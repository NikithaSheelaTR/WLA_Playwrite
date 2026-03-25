namespace Framework.Common.UI.Products.WestlawEdge.Components.ToC
{
    using System.Collections.Generic;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The ToC component on the Notes of Decisions tab.
    /// </summary>
    public class EdgeNotesOfDecisionsTocComponent : BaseEdgeTocComponent
    {
        private const string HeadingLctMask = ".//div[@class='TocEntryContent']/a[contains(@title,'{0}')]/span";
        private const string HeadingHighlightedLctMask = ".//a[contains(@title,'{0}')]//ancestor-or-self::div[contains(@class , 'TocEntryWrapper')]";
        private const string TopLevelHeadingChildItemsLctMask = ".//li[.//a/span[contains(text(),'{0}')]]/ol";

        private static readonly By TocSectionExpandButtonLocator = By.XPath(".//button[@aria-controls='coid_browseToc']");
        private static readonly By TocHeadingLocator = By.XPath(".//a[@class = 'co_link']/span");

        private static readonly By ContainerLocator = By.Id("coid_nodTocContainer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks the show/hide button for ToC section on NOD tab
        /// </summary>
        public void ClickTocSectionExpandButton() =>
            DriverExtensions.GetElement(this.ComponentLocator, TocSectionExpandButtonLocator).Click();

        /// <summary>
        /// Verifies that heading is selected on Toc pane.
        /// </summary>
        /// <param name="heading">
        /// The heading.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsHeadingSelected(string heading) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(string.Format(HeadingHighlightedLctMask, heading)))
            .GetAttribute("class").Contains("TocEntryHighlight");

        /// <summary>
        /// Verifies that heading in view on TOC pane
        /// </summary>
        /// <param name="headingName">
        /// The heading Name
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>. True if heading in left panel is in view
        /// </returns>
        public bool IsHeadingInView(string headingName) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(string.Format(HeadingLctMask, headingName))).IsElementInView();

        /// <summary>
        /// Get the whole heading title, by part of it.
        /// </summary>
        /// <param name="headingName"> part of heading</param>
        /// <returns>The whole heading</returns>
        public string GetHeadingText(string headingName) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(string.Format(HeadingLctMask, headingName))).Text;

        /// <summary>
        /// Get list of Toc headings
        /// </summary>
        /// <returns>List of Toc headings</returns>
        public List<string> GetTocHeadingsList() => this.GetTocLinks(TocHeadingLocator);

        /// <summary>
        /// Get list of Toc links for specific top-level heading
        /// </summary>
        /// <returns>List of Toc headings</returns>
        public List<string> GetTopLevelHeadingChildItems(string topLevelHeadingName) =>
            this.GetTocLinks(By.XPath(string.Format(TopLevelHeadingChildItemsLctMask, topLevelHeadingName)), TocHeadingLocator);
    }
}