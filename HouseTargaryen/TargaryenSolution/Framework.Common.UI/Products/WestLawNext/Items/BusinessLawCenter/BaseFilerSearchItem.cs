namespace Framework.Common.UI.Products.WestLawNext.Items.BusinessLawCenter
{
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.BusinessLawCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Filer Search Item
    /// </summary>
    public abstract class BaseFilerSearchItem : BaseItem
    {
        private EnumPropertyMapper<BlcItemOptions, WebElementInfo> map;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFilerSearchItem"/> class. 
        /// </summary>
        /// <param name="additionalInfo"> Additional Info for JSON </param>
        /// <param name="container"> The container. </param>
        public BaseFilerSearchItem(IWebElement container, string additionalInfo) : base(container)
        {
            this.AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Options Map 
        /// </summary>
        protected EnumPropertyMapper<BlcItemOptions, WebElementInfo> Map =>
            this.map = this.map ?? EnumPropertyModelCache.GetMap<BlcItemOptions, WebElementInfo>(this.AdditionalInfo);

        /// <summary>
        /// Additional Info for JSON
        /// </summary>
        protected string AdditionalInfo { get; set; }

        /// <summary>
        /// Title element
        /// </summary>
        protected By TitleLocator { get; set; }

        /// <summary>
        /// Is item contains cik
        /// </summary>
        /// <param name="cik">CIK</param>
        /// <returns>True - if it contains, false  - otherwise</returns>
        public bool IsItemContainsCik(string cik) => DriverExtensions
            .GetAttribute("href", this.Container, this.TitleLocator).Contains(cik);

        /// <summary>
        /// Get Text From Option
        /// </summary>
        /// <param name="option">Option</param>
        /// <returns>Text from option</returns>
        public string GetTextFromOption(BlcItemOptions option)
        {
            IWebElement item = DriverExtensions.SafeGetElement(this.Container, By.XPath(this.Map[option].LocatorString));
            return item != null && item.Displayed ? item.GetText() : string.Empty;
        }
    }
}