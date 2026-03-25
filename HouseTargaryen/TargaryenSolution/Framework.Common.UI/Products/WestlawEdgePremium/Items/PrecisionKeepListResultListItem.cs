namespace Framework.Common.UI.Products.WestlawEdgePremium.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdgePremium.Items.ResultList;

    /// <summary>
    /// Precision Keep List result item
    /// </summary>
    public class PrecisionKeepListResultListItem : PrecisionResultListItem
    {
        private static readonly By TitleLocator = By.ClassName("ResultItem-title");
        private static readonly By OutOfPlanLabelLocator = By.ClassName("co_outOfPlanLabel");
        private static readonly By SummaryLabelLocator = By.ClassName("co_searchResults_summary");
        private static readonly By CitingReferenceLinkLocator = By.ClassName("co_doc_citing_refs_link");
        private static readonly By ClientIdAndDateLabelLocator = By.ClassName("Panel-keepList-clientID");
        private static readonly By ItemCheckboxLocator = By.XPath(".//input[@type='checkbox']");
        private static readonly By RemoveButtonLocator = By.XPath(".//*[contains(@class,'icon_trash')]");
        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[@class = 'ResultItem-flags']/a");
        private static readonly By ImageDocumentIconLocator = By.XPath(".//li[contains(@class,'PUI-iconWrapper')]/button/span");
        private static readonly By TitleLinkLocator = By.XPath(".//h3[@class='ResultItem-title']/a");
        private static readonly By SnippetLocator = By.XPath(".//div[@class='co_snippet']/a");        
        private static readonly By SynopsisLinkLocator = By.ClassName("co_searchResults_synopsisToggle");
        private static readonly By SearchResultCitationLocator = By.ClassName("ResultItem-metadata");

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionKeepListResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> container </param>
        public PrecisionKeepListResultListItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Check box
        /// </summary>
        public ICheckBox CheckBox => new CheckBox(Container, ItemCheckboxLocator);

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(Container, TitleLocator);

        /// <summary>
        /// Remove button
        /// </summary>
        public IButton RemoveButton => new Button(Container, RemoveButtonLocator);

        /// <summary>
        /// Out of plan label
        /// </summary>
        public ILabel OutOfPlanLabel => new Label(Container, OutOfPlanLabelLocator);

        /// <summary>
        /// Gets the key cite flag button.
        /// </summary>
        public IButton KeyCiteFlagButton => new Button(this.Container, KeyCiteFlagLocator);

        /// <summary>
        /// Title link.
        /// </summary>
        public ILink TitleLink => new Link(this.Container, TitleLinkLocator);

        /// <summary>
        /// Citing reference link.
        /// </summary>
        public ILink CitingReferenceLink => new Link(this.Container, CitingReferenceLinkLocator);

        /// <summary>
        /// Search snippets
        /// </summary>
        public IReadOnlyCollection<ILink> SnippetsList => new ElementsCollection<Link>(this.Container, SnippetLocator);

        /// <summary>
        /// Summary Label
        /// </summary>
        public ILabel SummaryLabel => new Label(Container, SummaryLabelLocator);

        /// <summary>
        /// Client Id and Added date
        /// </summary>
        public ILabel ClientIdAndDateLabel => new Label(Container, ClientIdAndDateLabelLocator);

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public List<KeyCiteFlag> KeyCiteFlags
        {
            get
            {
                var flagList = new List<KeyCiteFlag>();
                if (DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
                {
                    List<string> flagClasses = DriverExtensions.GetElements(this.Container, KeyCiteFlagLocator).Select(flag => flag.GetAttribute("class")).ToList();

                    flagClasses.ForEach(flagClass =>
                    {
                        flagList.Add(KeyCiteFlagsMap.FirstOrDefault(
                            map => !string.IsNullOrEmpty(map.Value.ClassName)
                                   && flagClass.Contains(map.Value.ClassName)).Key);
                    });
                }

                return flagList;
            }
        }

        /// <summary>
        /// Gets the aggregated citations.
        /// </summary>
        public new string AggregatedCitation
        {
            get
            {
                IWebElement citationElement = DriverExtensions.GetElement(this.Container, SearchResultCitationLocator);

                return
                    DriverExtensions.GetElements(citationElement, By.XPath("./span"))
                                            .Select(elem => elem.Text)
                                            .ToList()
                                            .Aggregate((i, j) => i + " " + j)
                                            .Trim();
            }
        }

        /// <summary>
        /// Gets all of the document icons on the search result item
        /// </summary>
        /// <returns> The IList of the document icons</returns>
        public IList<DocumentIcon> DocIconsImage
        {
            get
            {
                return DriverExtensions.GetElements(this.Container, ImageDocumentIconLocator)
                    .Select(i => i.GetAttribute("class")).Select(icon => icon.GetEnumValueByPropertyModel<DocumentIcon, WebElementInfo>(mod => mod.ClassName, 
                    sourceFolder: @"Resources/EnumPropertyMaps/WestlawEdgePremium"))
                           .ToList();
            }
        }
   
        /// <summary>
        /// Synopsis link
        /// </summary>
        public ILink SynopsisLink => new Link(Container, SynopsisLinkLocator);

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>();
    }
}