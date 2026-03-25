namespace Framework.Common.UI.Products.TaxnetPro.Components.NewTypeahead
{
    using System.Linq;
    using System.Collections.Generic;

    using Framework.Core.Utils.Enums;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.NewTypeahead;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The content type details base component
    /// </summary>
    public abstract class ContentTypeDetailsBaseComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        /// <summary>
        /// Titles Locator
        /// </summary>
        protected static readonly By TitlesLocator = By.XPath(".//h2 | .//h3");

        private const string DocumentsTitleLocator = "//ul[@id='co_categoryItems']//a";

        /// <summary>
        /// More Link Locator
        /// </summary>
        private const string MoreLinkLctMask =
            "//button[@id='{0}']";

        /// <summary>
        /// Gets the categories map.
        /// </summary>
        protected EnumPropertyMapper<TNPNewTypeaheadContentType, WebElementInfo> TabsMap { get; } =
            EnumPropertyModelCache.GetMap<TNPNewTypeaheadContentType, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/TaxnetPro/NewTypeahead");

        /// <summary>
        /// The click more link.
        /// </summary>
        /// <param name="tabs">
        /// The category.
        /// </param>
        public virtual void ClickMoreLink(TNPNewTypeaheadContentType tabs) =>
            DriverExtensions.GetElement(By.XPath(string.Format(MoreLinkLctMask, this.TabsMap[tabs].Text)))
                            .Click();

        /// <summary>
        /// This method clicks the very first document and opens it
        /// </summary>
        /// <param name="tabs"></param>
        public void OpenFirstDocument(TNPNewTypeaheadContentType tabs) =>
            DriverExtensions.GetElement(By.XPath(string.Format(DocumentsTitleLocator, this.TabsMap[tabs].Text))).Click();
        
        /// <summary>
        /// This method return the list of document titles text
        /// </summary>
        /// <param name="tabs"></param>
        /// <returns></returns>
        public virtual List<string> GetDocumentsTitlesText(TNPNewTypeaheadContentType tabs) =>
            DriverExtensions.GetElements(By.XPath(string.Format(DocumentsTitleLocator, this.TabsMap[tabs].Text))).Select(el => el.Text).Where(x => x.Length > 0).ToList();

        /// <summary>
        /// The is more link displayed.
        /// </summary>
        /// <param name="tabs">
        /// The category.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool IsMoreLinkDisplayed(TNPNewTypeaheadContentType tabs) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(MoreLinkLctMask, this.TabsMap[tabs].Text)));

        /// <summary>
        /// The get document list.
        /// </summary>
        /// <param name="resultLocator">
        /// The result locator.
        /// </param>
        /// <returns> List of items </returns>
        protected virtual IList<IWebElement> GetItems(params By[] resultLocator)
        {
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElements(resultLocator).ToList();
        }

        /// <summary>
        /// The get items.
        /// </summary>
        /// <param name="lctMask">
        /// The lct mask.
        /// </param>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <returns>
        /// The <see cref="T:IList"/>.
        /// IList
        /// </returns>
        protected virtual IList<IWebElement> GetItems(string lctMask, params string[] values) =>
            this.GetItems(SafeXpath.BySafeXpath(lctMask, values));
    }
}
