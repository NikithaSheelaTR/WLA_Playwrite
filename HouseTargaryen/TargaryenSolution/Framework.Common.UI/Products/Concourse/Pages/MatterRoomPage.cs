namespace Framework.Common.UI.Products.Concourse.Pages
{
    using Framework.Common.UI.Products.Concourse.Pages.Base;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// MatterRoomPage
    /// </summary>
    public class MatterRoomPage : BaseMatterPage
    {
        private static readonly By ViewAllLinkLocator = By.LinkText("View All");

        /// <summary>
        /// Click on 'View All' link
        /// </summary>
        /// <returns>The <see cref="MattersPage"/>.</returns>
        public MattersPage ClickOnViewAllLink()
        {
            DriverExtensions.WaitForElement(ViewAllLinkLocator).Click();
            return new MattersPage();
        }
    }
}