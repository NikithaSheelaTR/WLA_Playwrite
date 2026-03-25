namespace Framework.Common.UI.Products.Shared.Components.CustomPage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Category Page List Item
    /// </summary>
    public class ContentSectionItem : BaseItem
    {
        private static readonly By CheckboxLocator = By.CssSelector("input.cp_checkbox_categorypage[type=checkbox]");

        private static readonly By LinkLocator = By.CssSelector("a");

        private static readonly By LinkTextLocator = By.CssSelector("a>span.cp_linkText");

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentSectionItem"/> class. 
        /// </summary>
        /// <param name="listElement">IWebElement
        /// </param>            
        public ContentSectionItem(IWebElement listElement) : base(listElement)
        {
        }

        /// <summary>
        /// Gets text
        /// </summary>
        /// <returns>Text</returns>
        public string Text => DriverExtensions.GetElement(this.Container, LinkTextLocator).Text;
        
        /// <summary>
        /// Click Category Page Link
        /// </summary>
        /// <typeparam name="T"> T </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickContentSectionLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, LinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check Item
        /// </summary>
        /// <param name="select"> True to check, false to uncheck </param>
        public void SetContentSectionCheckbox(bool select)
            => DriverExtensions.GetElement(this.Container, CheckboxLocator).SetCheckbox(select);
    }
}