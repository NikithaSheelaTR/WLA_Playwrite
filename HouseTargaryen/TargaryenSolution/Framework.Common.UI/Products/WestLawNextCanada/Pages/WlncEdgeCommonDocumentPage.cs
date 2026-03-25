namespace Framework.Common.UI.Products.WestLawNextCanada.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNextCanada.Pages.Documents;
    using Framework.Common.UI.Products.WestLawNextCanada.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// WLNCEdgeCommonDocumentPage
    /// </summary>
    public class WlncEdgeCommonDocumentPage : DocumentPage
    {
        private const string LinkBesideKeyciteFlagLctMask = "{0}/ancestor::span/following-sibling::span[contains(@class,'docLinkWrapper')]/a[starts-with(@id,'co_link_')]";
        private const string ParagarphLocatorMask = "crsw_paragraph_num_{0}";
        private static readonly By SearchWithinHighlightedTermsLocator = By.XPath("//span[contains(@class,'co_searchWithinTerm')]/parent::div/parent::div");
        private static readonly By SearchWithinResultsGreenHighlightedTermsLocator = By.XPath("//span[contains(@class,'co_searchWithinTerm')]");
        private static readonly By RelevantJudicialConsiderationHeadingLocator = By.ClassName("co_notesOfDecisionsHeading");
        private static readonly By RelevantJudicialConsiderationSubnoteLocator = By.ClassName("co_notesOfDecisionsExplaination");
        private static readonly By HierarchyLinksLocator = By.CssSelector("div.co_customDigestInner>a");
        private static readonly By ProceduralPostureLocator = By.Id("crsw_proceedingsHeader");
        private static readonly By DocumentLeftSidebarLocator = By.ClassName("crsw_sidebar_left");
        private static readonly By DocumentRightSidebarLocator = By.ClassName("crsw_sidebar_right");
        private static readonly By DocumentTitleLocator = By.Id("titleInfo");
        private static readonly By DocumentAuthorLocator = By.ClassName("crsw_judge");
        private static readonly By ToolbarContainerLocator = By.Id("co_docToolbarContainer");
        private static readonly By ProfileTabLocator = By.Id("coid_DocumentTab_link");

        private EnumPropertyMapper<KeyCiteFlag, WebElementInfo> keyCiteFlagMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagMap
            => this.keyCiteFlagMap = this.keyCiteFlagMap ?? EnumPropertyModelCache
                                         .GetMap<KeyCiteFlag, WebElementInfo>(string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new CanadaEdgeHeaderComponent Header { get; } = new CanadaEdgeHeaderComponent();

        /// <summary>
        /// Gets the toolbar.
        /// </summary>
        public new CanadaToolbarComponent Toolbar { get; } = new CanadaToolbarComponent(ToolbarContainerLocator);

        /// <summary>
        /// Label for Judicial Consideration Banner
        /// </summary>
        public ILabel JudicialConsiderationBanner => new Label(RelevantJudicialConsiderationHeadingLocator);

        /// <summary>
        /// Label for Judicial Consideration Note
        /// </summary>
        public ILabel JudicialConsiderationSubNote => new Label(RelevantJudicialConsiderationSubnoteLocator);

        /// <summary>
        /// Label for Procedural Posture Proceedings: label
        /// </summary>
        public ILabel ProcedingsLabel => new Label(ProceduralPostureLocator);

        /// <summary>
        /// Label for Document Author
        /// </summary>
        public ILabel DocumentAuthor => new Label(DocumentAuthorLocator);

        /// <summary>
        /// Gets the paragraph.
        /// </summary>
        /// <param name="paragraphNum"></param>
        /// <returns></returns>
        public ILabel Paragraph(string paragraphNum) 
            => new Label(By.Id(string.Format(ParagarphLocatorMask,paragraphNum)));

        /// <summary>
        /// Click by Notes of Decisions link
        /// </summary>
        /// <returns>Notes of Decisions page</returns>
        public JudicialConsiderationPage ClickJudicialConsiderationLink()
        {
            this.ClickNotesOfDecisionsLink();
            return new JudicialConsiderationPage();
        }

        /// <summary>
        /// Verifies that heading is selected on Toc pane.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchTermPresentInJudicialConsiderationBanner()
        {
            bool isTextFound = false;
            foreach (IWebElement webElement in DriverExtensions.GetElements(SearchWithinHighlightedTermsLocator))
            {
                if (webElement.GetAttribute("class").Equals("crsw_notesOfDecisionsBlock"))
                {
                    isTextFound = true;
                    break;
                }
            }

            return isTextFound;
        }

        /// <summary>
        /// Verifies that heading is selected on Toc pane.
        /// </summary>
        /// <param name="notetext">
        /// The heading.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchWithinResultsHightlightedInGreen(string notetext)
        {
            bool isTextFound = false;
            foreach (IWebElement webElement in DriverExtensions.GetElements(SearchWithinResultsGreenHighlightedTermsLocator))
            {
                if (webElement.Text.Equals(notetext))
                {
                    isTextFound = true;
                    break;
                }
            }

            return isTextFound;
        }

        /// <summary>
        /// Gets list of hierarchy links
        /// </summary>
        /// <returns>list of hierarchy links in abridgement classification</returns>
        public IReadOnlyCollection<ILink> GetHierarchyLinks() => new ElementsCollection<Link>(HierarchyLinksLocator);

        /// <summary>
        /// Verify if left sidebar is displayed in the document
        /// </summary>
        /// <returns>return true is displayed, else false</returns>
        public bool IsLeftSidebarDisplayed() => DriverExtensions.IsDisplayed(DocumentLeftSidebarLocator);

        /// <summary>
        /// Verify if right sidebar is displayed in the document
        /// </summary>
        /// <returns>return true is displayed, else false</returns>
        public bool IsRightSidebarDisplayed() => DriverExtensions.IsDisplayed(DocumentRightSidebarLocator);

        /// <summary>
        /// Gets the title of the document
        /// </summary>
        /// <returns>Title of the document</returns>
        public string GetCanadaDocumentTitle() => DriverExtensions.GetText(DocumentTitleLocator);

        /// <summary>
        /// Get Click Here Link element
        /// </summary>
        public ILink ClickDocumentTab => new Link(ProfileTabLocator);

        /// <summary>
        /// Click on a Flag in the doc.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public T ClickKeyCiteFlag<T>(KeyCiteFlag flag) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(this.KeyCiteFlagMap[flag].LocatorString), 5).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on a link beside Flag in the doc.
        /// </summary>
        /// <param name="flag">The flag.</param>
        public T ClickOnLinkBesideKeyCiteFlag<T>(KeyCiteFlag flag) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                By.XPath(string.Format(LinkBesideKeyciteFlagLctMask, this.KeyCiteFlagMap[flag].LocatorString)), 5).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
