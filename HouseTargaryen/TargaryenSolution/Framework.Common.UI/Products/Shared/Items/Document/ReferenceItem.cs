namespace Framework.Common.UI.Products.Shared.Items.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// ReferenceItem
    /// </summary>
    public class ReferenceItem : BaseItem
    {
        private static readonly By KeyCiteFlagLocator = By.XPath(".//div[contains(@class, 'co_keyCite_treatment')]/a");
        private static readonly By TitleLocator = By.CssSelector(".co_relatedInfo_grid_documentLink");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceItem"/> class.
        /// </summary>
        /// <param name="container">
        /// The container element.
        /// </param>
        public ReferenceItem(IWebElement container)
            : base(container)
        {

        }

        /// <summary>
        /// document title
        /// </summary>
        public ILink Title => new Link(this.Container,TitleLocator);

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
        /// The click key cite flag.
        /// </summary>
        /// <typeparam name="TPage">
        /// the type of page
        /// </typeparam>
        /// <returns>
        /// The Document page
        /// </returns>
        public TPage ClickKeyCiteFlag<TPage>()
            where TPage : ICreatablePageObject
        {
            this.KeyCiteFlagElement.Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement => DriverExtensions.SafeGetElement(this.Container, KeyCiteFlagLocator);
    }
}
