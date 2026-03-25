namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The doc analyzer base item.
    /// </summary>
    public class QuickCheckBaseItem : BaseItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//input[contains(@type,'checkbox')]");

        private static readonly By IndexLocator = By.XPath(".//span[@class='DA-TitleCount']");

        private static readonly By LinkLocator = By.XPath(".//*[contains(@class, 'DA-DocumentLink')]/a");

        private static readonly By DetailsLocator = By.XPath(".//div[contains(@class,'co_searchResults_citation')]/span");

        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[@class='co_search_keyciteFlag']/a");

        private static readonly By ImpliedOverrulingLocator = By.XPath(".//a[contains(@oldtitle,'KeyCite Overruling')]");

        private static readonly By PuiIconsLocator = By.XPath(".//ul[@class='PUI-icons']/li/button");

        private static readonly By OutOfPlanBannerLabelLocator = By.ClassName("co_outOfPlanLabel");

        private static readonly By LexisIconLocator = By.XPath(".//div[@class='co_searchResults_citation DA-DocMetadata ']/button");

        private static readonly By DepthGreenBarLocator = By.XPath(".//div[@class='DA-DepthOfDiscussion']//div[contains(@class, 'isIncreased')]");

        private EnumPropertyMapper<DocumentIcon, WebElementInfo> docIconsMap;

        /// <inheritdoc />
        public QuickCheckBaseItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// The title link.
        /// </summary>
        public ILink TitleLink => new Link(this.Container, LinkLocator);

        /// <summary>
        /// Out of plan banner label
        /// </summary>
        public ILabel OutOfPlanLabel => new Label(OutOfPlanBannerLabelLocator);

        /// <summary>
        /// The checkbox.
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckboxLocator);

        /// <summary>
        /// Lexis label
        /// </summary>
        public ILabel LexisLabel => new Label(this.Container, LexisIconLocator);

        /// <summary>
        /// Details labels
        /// </summary>
        public IReadOnlyCollection<ILabel> DetailsLabels => new ElementsCollection<Label>(this.Container, DetailsLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
                {
                    string flagClass = this.KeyCiteFlagElement.GetAttribute("class");

                    if (flagClass.Equals("co_impliedOverrulingsFlagSm"))
                    {
                        return KeyCiteFlag.ImpliedOverruling;
                    }

                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName);
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Gets Data
        /// </summary>
        public string Date
        {
            get
            {
                DateTime dateTime;
                IWebElement date = DriverExtensions
                    .GetElements(this.Container, DetailsLocator)
                    .FirstOrDefault(i => DateTime.TryParse(i.Text, out dateTime));
                return date != null ? date.Text : string.Empty;
            }
        }

        /// <summary>
        /// Gets all of the document icons on the search result
        /// </summary>
        /// <returns> The IList of the document icons </returns>
        public List<DocumentIcon> DocIcons
        {
            get
            {
                IEnumerable<IWebElement> pathsToSrc = DriverExtensions.GetElements(this.Container, PuiIconsLocator);

                return pathsToSrc.Any() && pathsToSrc.Any(item => item.IsDisplayed())
                           ? this.DocIconsMap.Where(
                                     pair => pathsToSrc
                                         .Where(elem => elem.IsDisplayed()).Select(elem => elem.GetAttribute("class"))
                                         .Any(path => path.Contains(pair.Value.ClassName))).Select(pair => pair.Key)
                                 .ToList()
                           : new List<DocumentIcon>();
            }
        }

        /// <summary>
        /// The value of docguid attribute
        /// </summary>
        public string DocGuid => this.TitleLink.GetAttribute("href").ExtractWestlawGuid();

        /// <summary>
        /// The Implied Overruling present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsImpliedOverrulingPresent => DriverExtensions.SafeGetElement(this.Container, ImpliedOverrulingLocator) != null;

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement => DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator);   

        /// <summary>
        /// The doc icons map.
        /// </summary>
        private EnumPropertyMapper<DocumentIcon, WebElementInfo> DocIconsMap =>
            this.docIconsMap = this.docIconsMap ?? EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>(
                                   "QuickCheck",
                                   @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// The click key cite flag.
        /// </summary>
        /// <typeparam name="TPage">
        /// The type of page
        /// </typeparam>
        /// <returns>
        /// The Document page
        /// </returns>
        public TPage ClickKeyCiteFlag<TPage>() where TPage : ICreatablePageObject
        {
            this.KeyCiteFlagElement.ScrollToElement();
            this.KeyCiteFlagElement.CustomClick();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Gets the index of an issue.
        /// </summary>
        /// <returns>The index of an issue.</returns>
        public int GetIndex() => DriverExtensions.GetElement(this.Container, IndexLocator).Text.ConvertCountToInt();

        /// <summary>
        /// Is in view
        /// TODO create new universal script for all Westlaw Edge
        /// </summary>
        /// <returns>True if element in view</returns>
        public bool IsInView()
        {
          const string Script = @"var rect     = arguments[0].getBoundingClientRect(),
                                wWidth   = window.innerWidth || doc.documentElement.clientWidth,
                                wHeight  = window.innerHeight || doc.documentElement.clientHeight,
                                notHidden = function (x, y) { return document.elementFromPoint(x, y) };
                                if (rect.right < 0 || rect.bottom < 0
                                || rect.left > wWidth || rect.top > wHeight)
                                return false;
                                const centerX = rect.left + rect.width / 2;
                                const centerY = rect.top + rect.height / 2;
                                return (
                                arguments[0].contains(notHidden(rect.left,  rect.top))
                                ||  arguments[0].contains(notHidden(rect.right, rect.top))
                                ||  arguments[0].contains(notHidden(rect.right, rect.bottom))
                                ||  arguments[0].contains(notHidden(rect.left,  rect.bottom))
                                ||  arguments[0].contains(notHidden(centerX,  centerY))
                                );";
          return (bool)DriverExtensions.ExecuteScript(Script, this.Container);
        }

        /// <summary>
        /// Get green bars count of depth of discussion
        /// </summary>
        /// <returns>Count of green bars</returns>
        public int GetDepthOfDiscussionBarNumber() => DriverExtensions.GetElements(this.Container, DepthGreenBarLocator).Count;

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(String.Empty, @"Resources/EnumPropertyMaps");

        /// <summary>
        /// Keycite flags for the cited authority item.
        /// </summary>
        public List<KeyCiteFlag> KeyCiteFlags
        {
            get
            {
                var flags = new List<KeyCiteFlag>();

                if (!DriverExtensions.IsDisplayed(this.Container, KeyCiteFlagLocator))
                    return flags;

                var flagElements = this.Container.FindElements(KeyCiteFlagLocator);

                foreach (var flagElement in flagElements)
                {
                    string flagClass = flagElement.GetAttribute("class");

                    if (flagClass.Contains("co_impliedOverrulingsFlagSm"))
                    {
                        flags.Add(KeyCiteFlag.ImpliedOverruling);
                        continue;
                    }

                    var matchedFlag = KeyCiteFlagsMap.FirstOrDefault(
                        map => !string.IsNullOrEmpty(map.Value.ClassName) &&
                               flagClass.Contains(map.Value.ClassName)).Key;

                    if (!matchedFlag.Equals(default(KeyCiteFlag)))
                    {
                        flags.Add(matchedFlag);
                    }
                }

                return flags.Distinct().ToList();
            }
        }

        /// <summary>
        /// Get draggable element
        /// </summary>
        /// <returns>this element</returns>
        internal IWebElement GetDraggableElement() => DriverExtensions.GetElement(this.Container, LinkLocator);
    }
}