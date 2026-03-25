namespace Framework.Common.UI.Products.Shared.Components.HomePage.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Miscellaneous;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.MyContent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// My content Tab Panel
    /// </summary>
    public class MyContentTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Browse-widget']//div[contains(@class,'Tab-panel') and @aria-hidden='false']");

        private EnumPropertyMapper<MyContent, WebElementInfo> myContentMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "My content";

        /// <summary>
        /// Gets the PracticeArea enumeration to WebElementInfo map.
        /// </summary>
        private EnumPropertyMapper<MyContent, WebElementInfo> MyContentMap
            => this.myContentMap = this.myContentMap ?? EnumPropertyModelCache.GetMap<MyContent, WebElementInfo>();

        /// <summary>
        /// Gets the Miscellaneous component
        /// </summary>
        public MiscellaneousComponent MiscellaneousComponent { get; } = new MiscellaneousComponent();

        /// <summary>
        /// Clicks a given browse tab
        /// </summary>
        /// <typeparam name="T">MyContents Tab</typeparam>
        /// <param name="tab"> Tab to click</param>
        /// <returns>
        /// This object, for fluent interfaces
        /// </returns>
        public T ClickMyContentsTab<T>(MyContent tab) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.MyContentMap[tab].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is Tab Active
        /// </summary>
        /// <param name="tab"> My Content tab </param>
        /// <returns> True if expected tab is active </returns>
        public bool IsActive(MyContent tab) =>
            DriverExtensions.WaitForElementDisplayed(By.Id(this.MyContentMap[tab].Id)).GetAttribute("aria-selected")
                            .Equals("true");
    }
}
