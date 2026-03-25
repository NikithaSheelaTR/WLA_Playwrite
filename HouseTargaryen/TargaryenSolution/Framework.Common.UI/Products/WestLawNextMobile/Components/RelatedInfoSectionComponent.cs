namespace Framework.Common.UI.Products.WestLawNextMobile.Components
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Component for the Related Info section at the bottom of document pages and RI pages
    /// </summary>
    public class RelatedInfoSectionComponent : BaseModuleRegressionComponent
    {
        private const string RiItemSubcategoryTmpl =
            "id('main')//div[contains(text(), \"{0}\") and contains(@class, 'hdr')]/following-sibling::ul[contains(@class, 'lstTwo')]/li/a[contains(text(), \"{1}\")]";

        private const string RiItemTmpl = "id('main')/ul[contains(@class, 'lstTwo')]/li/a[contains(text(), \"{0}\")]";

        private static readonly By EmailListLinkLocator = By.Id("coid_website_emailDocumentLink");

        private static readonly By RiSectionButton = By.XPath("//div[contains(text(), 'Related Information')]//a");

        private static readonly By ContainerLocator = By.Id("main");

        /// <summary>
        /// The tabs map.
        /// </summary>
        private EnumPropertyMapper<RiTab, WebElementInfo> linksMap;

        /// <summary>
        /// Clicks on Related info button
        /// </summary>
        public bool IsRiButtonDisplayed => DriverExtensions.IsDisplayed(RiSectionButton, 5);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the tabs Map.
        /// </summary>
        private EnumPropertyMapper<RiTab, WebElementInfo> LinksMap
            => this.linksMap = this.linksMap ?? EnumPropertyModelCache.GetMap<RiTab, WebElementInfo>();

        /// <summary>
        /// Click on the Email List link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickEmailListLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EmailListLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on specified RI link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="tab"> RI tab to click on. </param>
        /// <returns> New instance of the page </returns>
        public T ClickRiLink<T>(RiTab tab) where T : ICreatablePageObject
        {
            this.GetTabWebElement(tab).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on specified RI link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="tab"> RI tab to click on. </param>
        /// <param name="riSubcategory">Related info subcategory</param>
        /// <returns> New instance of the page </returns>
        public T ClickRiSubcategoryLink<T>(RiTab tab, string riSubcategory) where T : ICreatablePageObject
        {
            this.GetTabSubcategoryWebElement(tab, riSubcategory).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks the presence of specified tabs.
        /// </summary>
        /// <param name="tabs">The tabs to check</param>
        /// <returns>True if specified tabs are presented, false if they aren't.</returns>
        public bool IsRiLinkDisplayed(params RiTab[] tabs) => tabs.All(tab => this.GetTabWebElement(tab).Displayed);

        /// <summary>
        /// Checks the presence of specified tabs.
        /// </summary>
        /// <param name="tab">The tab to check</param>
        /// <param name="riSubcategories">The Related info Subcategories</param>
        /// <returns>True if specified tabs are presented, false if they aren't.</returns>
        public bool IsRiSubcategoryLinkDisplayed(RiTab tab, params string[] riSubcategories)
            => riSubcategories.All(subcategory => this.GetTabSubcategoryWebElement(tab, subcategory).Displayed);

        /// <summary>
        /// The get tab web element.
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <param name="subcategoryOfTab"></param>
        /// <returns> The <see cref="IWebElement"/>. </returns>
        private IWebElement GetTabSubcategoryWebElement(RiTab tab, string subcategoryOfTab)
        {
            string tabText = this.LinksMap[tab].Text;
            By tabSubcategoryLocator = By.XPath(string.Format(RiItemSubcategoryTmpl, tabText, subcategoryOfTab));
            DriverExtensions.WaitForElement(tabSubcategoryLocator);

            IWebElement elementToClick =
                DriverExtensions.GetElements(tabSubcategoryLocator, el => el.Text.RetainText().Equals(subcategoryOfTab))
                                .First();

            return elementToClick;
        }

        /// <summary>
        /// The get tab web element.
        /// </summary>
        /// <param name="tab"> The tab. </param>
        /// <returns> The <see cref="IWebElement"/>. </returns>
        private IWebElement GetTabWebElement(RiTab tab)
        {
            string expectedTabText = this.LinksMap[tab].Text;
            By tabLocator = By.XPath(string.Format(RiItemTmpl, expectedTabText));
            DriverExtensions.WaitForElement(tabLocator);
            IWebElement elementToClick =
                DriverExtensions.GetElements(tabLocator, el => el.Text.RetainText().Equals(expectedTabText)).First();

            return elementToClick;
        }
    }
}