namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Preview Results Item
    /// </summary>
    public class PreviewResultsItem : ResultListItem
    {
        private static readonly By ChapterValueLocator = By.XPath("//span[contains(text(),'BANKRUPTCY, CHAPTER')]");

        private static readonly By DateValueLocator = By.XPath(".//span[@class='co_search_detailLevel_1'][2]");

        private static readonly By DocketNumberValueLocator = By.XPath("//span[@class='co_search_detailLevel_1'][3]");

        private static readonly By JudgeNameValueLocator = By.XPath("//span[contains(text(),'Judge(s):')]");
        
        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[contains(@class, 'search_keyciteFlag')]/span");


        /// <summary>
        /// Initializes a new instance of the <see cref="PreviewResultsItem"/> class. 
        /// </summary>
        /// <param name="containerElement"> containerElement </param>
        public PreviewResultsItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Gets Docket Number
        /// </summary>
        /// <returns> The Docket Number </returns>
        public string CaseNumber => this.TryGetText(DocketNumberValueLocator);

        /// <summary>
        /// Gets Chapter Number
        /// </summary>
        /// <returns> The Chapter Number </returns>
        public string ChapterNumber => this.TryGetText(ChapterValueLocator);

        /// <summary>
        /// Gets Date
        /// </summary>
        /// <returns> The Date </returns>
        public new string Date => this.TryGetText( DateValueLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public override KeyCiteFlag KeyCiteFlag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(KeyCiteFlagElement))
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


        /// <summary>
        /// Gets Judge Name
        /// </summary>
        /// <returns> The Judge Name </returns>
        public string Judge => this.TryGetText(JudgeNameValueLocator);
    }
}