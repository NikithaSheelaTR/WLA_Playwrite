namespace Framework.Common.UI.Products.Shared.Components.RecentResearch
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.SignIn;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The recent research list item.
    /// </summary>
    public class RecentResearchListItem : BaseItem
    {
        private static readonly By ItemLocator = By.CssSelector("div.co_recentResearch_item");

        private static readonly By ResultsCountLocator = By.CssSelector("span");

        private static readonly By TitleLinkLocator = By.XPath(".//a[contains(@href,'java')] | .//div[contains(@class,'Link')]");

        private static readonly By InfoIconLocator = By.XPath(".//span[contains(@class,'icon25 icon_help-blueOutline')]");

        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");

        private static readonly By KeyCiteFlagLocator = By.XPath("./preceding-sibling::*//span[@class='co_foldering_keyciteContainer']/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentResearchListItem"/> class.
        /// </summary>
        /// <param name="rootElement">
        /// The root element.
        /// </param>
        public RecentResearchListItem(IWebElement rootElement) : base(DriverExtensions.GetElement(rootElement, ItemLocator))
        {
            this.LinkText = DriverExtensions.GetElement(this.Container, TitleLinkLocator).Text;
            this.Text = rootElement.Text;
            this.Type = this.DetectType();
        }

        /// <summary> 
        /// Link of Recent Research ListItem. 
        /// </summary>
        public string LinkText { get; set; }

        /// <summary>
        /// Results Count
        /// </summary>
        public int ResultsCount
            => DriverExtensions.GetElement(this.Container, ResultsCountLocator).Text.RetrieveCountFromBrackets();

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary> 
        /// Recent Research ListItem Type. 
        /// </summary>
        public RecentResearchListItemType Type { get; set; }

        /// <summary>  
        /// Click on the title link. 
        /// </summary>
        /// <typeparam name="T"> Page Object. </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickTitleLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, TitleLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        #region Disabled item
        /// <summary>
        /// Is links enabled
        /// </summary>
        /// <returns>
        /// Return true if the link is enabled
        /// </returns>
        public bool IsLinkEnabled() => !DriverExtensions.GetElement(this.Container, TitleLinkLocator).GetAttribute("class").Contains("disabled");

        /// <summary>
        /// Is info icon (?) displayed
        /// </summary>
        /// <returns>
        /// Return true if the info icon is displayed
        /// </returns>
        public bool IsInfoIconDisplayed() => DriverExtensions.IsDisplayed(this.Container, InfoIconLocator);
           
        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// Return true if the hover message is displayed
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(InfoIconLocator);
            return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");                 
        }

        /// <summary>
        /// Get Hover Message text
        /// </summary>
        /// <returns>
        /// The hover message's text
        /// </returns>
        public string GetHoverMessageText() => DriverExtensions.GetText(HoverMessageLocator);
        #endregion

        /// <summary> 
        /// Detect the type of the recent research item
        /// </summary>
        /// <returns> The RecentResearchListItemType </returns>
        private RecentResearchListItemType DetectType()
        {
            RecentResearchListItemType type;

            switch (this.Container.GetAttribute("class"))
            {
                case "co_recentResearchItemSearch":
                    type = RecentResearchListItemType.Search;
                    break;
                case "co_recentResearchItemDocument":
                    type = RecentResearchListItemType.Document;
                    break;
                default:
                    type = RecentResearchListItemType.Search; 
                    break;
            }

            return type;
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
                    return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(model => model.ClassName);
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