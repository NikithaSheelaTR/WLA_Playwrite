namespace Framework.Common.UI.Products.Shared.Components.SignOff
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The sign off session details component.
    /// </summary>
    public class SignOffSessionDetailsComponent : CommonAuthenticatedWestlawNextPage
    {
        private static readonly By SessionDetailsItemLocator = By.XPath("//*[@class='co_signOff_sessionDetails']/tbody/tr");

        /// <summary>
        /// The get session items.
        /// </summary>
        /// <returns>
        /// The list of items
        /// </returns>
        public List<SignOffSessionDetailsItem> GetSessionItems()
            => DriverExtensions.GetElements(SessionDetailsItemLocator)
                .Select(sessionItem => new SignOffSessionDetailsItem(sessionItem)).ToList();
    }
}
