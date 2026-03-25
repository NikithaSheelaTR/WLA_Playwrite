namespace Framework.Common.UI.Products.ANZ.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Global footer found on almost all ANZ pages.
    /// </summary>
    public class AnzFooterComponent : WestlawNextFooterComponent 
    {
        private static readonly By ThomsonReutersCopyRightLocator = By.XPath("//*[@id='co_footerCopyright']/ul/li");

        /// <summary>
        /// Gets the footer copyright text.
        /// </summary>
        /// <returns> The footer copyright text. </returns>
        public string GetCopyrightText() => DriverExtensions.GetText(ThomsonReutersCopyRightLocator);
    }
}
