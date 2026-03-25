namespace Framework.Common.UI.Products.LawSchool.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains elements and methods that represent the default desktop page
    /// </summary>
    public class LawSchoolHomePage : BaseModuleRegressionPage
    {
        /// <summary>
        /// The logo identifier.
        /// </summary>
        protected static readonly By LogoIdentifierLocator = By.Id("logo_new");

        private static readonly By WestlawNextNewButtonLocator =
            By.CssSelector("a[href='/shared/westlawredirect.aspx?task=welcomewestlawnext&appflag=116.1']");

        /// <summary>
        /// click on the the west law next link
        /// </summary>
        /// <returns>New instance of CommonSearchHomePage</returns>
        public CommonSearchHomePage ClickWestLawNextHomeLink()
        {
            DriverExtensions.WaitForElement(WestlawNextNewButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return new CommonSearchHomePage();
        }
    }
}