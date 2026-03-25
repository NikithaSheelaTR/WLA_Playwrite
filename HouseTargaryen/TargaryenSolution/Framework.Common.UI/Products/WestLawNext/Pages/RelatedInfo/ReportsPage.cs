using Framework.Common.UI.Interfaces;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    /// <summary>
    /// The Reports Page 
    /// </summary>
    public class ReportsPage : TabPage
    {
        private static readonly By ExpertChallengeReportLink = By.Id("coid_website_link_Professional_ExpertChallenge");

        /// <summary>
        /// Clicks on ExpertChallengeReportLink
        /// </summary>
        /// <returns> DocumentPage instance </returns>
        public T ClickExpertChallengeReportLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ExpertChallengeReportLink).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
