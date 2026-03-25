namespace Framework.Common.UI.Products.WestlawEdge.Pages.JurisdictionalSurveys
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Jurisdictional Surveys Page
    /// </summary>
    public class EdgeJurisdictionalSurveysPage : BaseModuleRegressionPage
    {
        private const string TopicNameLctMask = "//*[@id='coid_browseTabContents']//a[text()='{0}']";
        private const string SuggestedTermLctMask = "//label[text()='{0}']/preceding-sibling::input";
        private static readonly By CreateSurveyButtonLocator = By.Id("viewSurveyButton");
        private static readonly By ByTopicLocator = By.Id("topicTab");

        /// <summary>
        /// Clicking the Create Survey button
        /// </summary>
        /// <returns>The <see cref="EdgeJurisdictionalSurveyResultPage"/>.</returns>
        public T ClickCreateSurveyButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(CreateSurveyButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicking the By topic tab
        /// </summary>
        /// <returns>The <see cref="EdgeJurisdictionalSurveysPage"/>.</returns>
        public EdgeJurisdictionalSurveysPage ClickByTopicTab()
        {
            DriverExtensions.Click(ByTopicLocator);
            return this;
        }

        /// <summary>
        /// Clicking the By topic tab
        /// </summary>
        /// <param name="topicName"> Topic name </param>
        /// <returns>The <see cref="EdgeJurisdictionalSurveysPage"/>.</returns>
        public EdgeJurisdictionalSurveysPage ClickTopicByName(string topicName )
        {
            DriverExtensions.Click(By.XPath(string.Format(TopicNameLctMask, topicName)));
            return this;
        }

        /// <summary>
        /// Clicking the suggested term checkbox
        /// </summary>
        /// <param name="termName"> Term name </param>
        /// <returns>The <see cref="EdgeJurisdictionalSurveysPage"/>.</returns>
        public EdgeJurisdictionalSurveysPage ClickSuggestedTermByName(string termName)
        {
            DriverExtensions.Click(By.XPath(string.Format(SuggestedTermLctMask, termName)));
            return this;
        }
    }
}
