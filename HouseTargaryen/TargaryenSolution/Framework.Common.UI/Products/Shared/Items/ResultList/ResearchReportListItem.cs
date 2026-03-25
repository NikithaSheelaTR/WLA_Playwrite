namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;


    /// <summary>
    /// The research report list item.
    /// </summary>
    public class ResearchReportListItem : BaseItem
    {
        private static readonly By TitleLocator = By.XPath(".//a[@ng-click='documentClick(doc.Data.DocumentMetaInformation)']");

        private static readonly By CourtLocator = By.XPath(".//span[@ng-if='doc.Data.DocumentMetaInformation.Court']");

        private static readonly By DateLocator = By.XPath(".//span[@ng-if='doc.Data.DocumentMetaInformation.Date']");

        private static readonly By CitationLocator = By.XPath(".//span[@ng-if='doc.Data.DocumentMetaInformation.Cite']");

        private static readonly By JudgeLocator = By.XPath(".//span[contains(@ng-if,'doc.Data.ShowJudge')] |.//div[contains(@ng-if,'doc.Data.ShowJudge')]");

        private static readonly By PageBreakLinkLocator = By.XPath(".//a[contains(@ng-click, 'PageBreak')]");

        private static readonly By IncludeAnnotationForInlineNoteCheckboxLocator = By.XPath(".//div[@ng-if='note.InlineNotes']//ancestor::div[contains(@class,'co_documentInlineNote')]//label/input");

        private static readonly By IncludeAnnotationForDocLevelNoteCheckboxLocator = By.XPath(".//div[contains(@class,'co_documentLevelNote')]//label/input");

        private static readonly By IncludeAnnotationForHighlightCheckboxLocator = By.XPath(".//div[contains(@class,'co_documentInlineNote') and not (descendant::div[@ng-if='note.InlineNotes'])]//label/input");

        private static readonly By ItemIdLocator = By.XPath(".//h2[contains(@class,'co_headtext')]");

        private static readonly By KeyCiteFlagLocator = By.XPath(".//span[contains(@class,'co_citatorFlag ')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ResearchReportListItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container Element.
        /// </param>
        public ResearchReportListItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// The Item Id.
        /// </summary>
        public string ItemId => DriverExtensions.GetElement(this.Container, ItemIdLocator).GetAttribute("id");

        /// <summary>
        /// The title.
        /// </summary>
        public string Title => DriverExtensions.GetElement(this.Container, TitleLocator).GetText();

        /// <summary>
        /// Gets or sets the court.
        /// </summary>
        public string Court => DriverExtensions.GetElement(this.Container, CourtLocator).GetText();

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public string Date => DriverExtensions.GetElement(this.Container, DateLocator).GetText();

        /// <summary>
        /// Gets or sets the cite.
        /// </summary>
        public string Citation => DriverExtensions.GetElement(this.Container, CitationLocator).GetText();

        /// <summary>
        /// Gets or sets the page break.
        /// </summary>
        public string PageBreakLinkText =>
            DriverExtensions.GetElement(this.Container, PageBreakLinkLocator).GetText();

        /// <summary>
        /// Gets or sets the judge.
        /// </summary>
        public string Judge
        {
            get
            {
                IWebElement webElement;
                DriverExtensions.TryGetElement(this.Container, JudgeLocator, out webElement);
                return webElement?.Text;
            }
        }
        
        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public List<KeyCiteFlag> KeyCiteFlags
        {
            get
            {
                var flagList = new List<KeyCiteFlag>();
                if (DriverExtensions.IsDisplayed(KeyCiteFlagLocator))
                {
                    List<string> flagClasses = DriverExtensions.GetElements(KeyCiteFlagLocator).Select(flag => flag.GetAttribute("class")).ToList();

                    flagClasses.ForEach(flagClass =>
                    {
                        flagList.Add(KeyCiteFlagsMap.Single(
                            map => !string.IsNullOrEmpty(map.Value.ClassName)
                                   && flagClass.Contains(map.Value.ClassName)).Key);
                    });
                }

                return flagList;
            }
        }

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// SetIncludeAnnotationForInlineNoteCheckbox
        /// </summary>
        /// <param name="setTo">
        /// The set To.
        /// </param>
        public void SetIncludeAnnotationForInlineNoteCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(setTo, this.Container, IncludeAnnotationForInlineNoteCheckboxLocator);

        /// <summary>
        /// SetIncludeAnnotationForHighlightCheckbox
        /// </summary>
        /// <param name="setTo">
        /// The set To.
        /// </param>
        public void SetIncludeAnnotationForHighlightCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(setTo, this.Container, IncludeAnnotationForHighlightCheckboxLocator);

        /// <summary>
        /// SetIncludeAnnotationForDocLevelNoteCheckbox
        /// </summary>
        /// <param name="setTo">
        /// The set To.
        /// </param>
        public void SetIncludeAnnotationForDocLevelNoteCheckbox(bool setTo = true)
            => DriverExtensions.SetCheckbox(setTo, this.Container, IncludeAnnotationForDocLevelNoteCheckboxLocator);

        /// <summary>
        /// Click page break link.
        /// </summary>
        public void ClickPageBreakLink() => DriverExtensions.GetElement(this.Container, PageBreakLinkLocator).Click();

        /// <summary>
        /// Is 'Page Break' Link' don't exist
        /// </summary>
        /// <returns> True if link don't exist, false link exist </returns>
        public bool IsPageBreakLinkDisplayed() => DriverExtensions.IsDisplayed(this.Container, PageBreakLinkLocator);
    }
}