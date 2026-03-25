namespace Framework.Common.UI.Products.Shared.Items.FolderHistory
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Related to the RecentHistoryDialog which displayed in the header of the Home page 
    /// </summary>
    public class SearchesRecentHistoryItem : BaseItem
    {
        private static readonly By InfoIconLocator = By.XPath(".//span[@class='icon25 icon_help-blueOutline']");
        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");
        private static readonly By MetaDataLocator = By.ClassName("Meta");
        private static readonly By TitleLocator = By.XPath(".//div[contains(@class,'historyItem')]/a | .//ul[contains(@id,'co_recentSearchesList')]");
        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[contains(@class, 'flags')]/a");
        private static readonly By HistoryItemDateLabelLocator = By.XPath("./*[@class = 'Meta']");

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchesRecentHistoryItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// Container
        /// </param>
        public SearchesRecentHistoryItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Get search query text
        /// </summary>
        public string SearchQuery => DriverExtensions.GetElement(this.Container).Text;

        /// <summary>
        /// Gets the metadata
        /// </summary>
        public string MetaData => DriverExtensions.GetElement(this.Container, MetaDataLocator).Text;

        /// <summary>
        /// Gets the history item link
        /// </summary>
        public ILink HistoryItemLink => new Link(this.Container, TitleLocator);

        /// <summary>
        /// Gets the history item date label
        /// </summary>
        public ILabel HistoryItemDateLabel => new Label(this.Container, HistoryItemDateLabelLocator);

        /// <summary>
        /// Is Info icon (?) displayed 
        /// </summary>
        /// <returns>
        /// Return True if info icon is displayed
        /// </returns>
        public bool IsInfoIconDisplayed => DriverExtensions.IsDisplayed(this.Container, InfoIconLocator);

        /// <summary>
        /// Is history item link is clickable
        /// </summary>
        /// <returns>
        /// Return True if history item link is clickable
        /// </returns>
        public bool IsClickable => !string.IsNullOrEmpty(this.HistoryItemLink.LinkUrl);

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// Return true if the hover message is displayed after hover overing info icon
        /// </returns>
        public bool IsHoverMessageDisplayedIfInfoIconExists()
        {
            if (this.IsInfoIconDisplayed)
            {
                DriverExtensions.GetElement(this.Container, InfoIconLocator).Hover();
                return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");
            }

            return false;
        }

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
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName);
                }
                
                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement => DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator);
    }
}
