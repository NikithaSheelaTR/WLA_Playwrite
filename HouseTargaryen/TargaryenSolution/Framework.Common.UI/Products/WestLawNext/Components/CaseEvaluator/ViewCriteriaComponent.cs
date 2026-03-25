namespace Framework.Common.UI.Products.WestLawNext.Components.CaseEvaluator
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// component representing View Criteria widget in CE toolbar
    /// </summary>
    public class ViewCriteriaComponent : BaseModuleRegressionComponent
    {
        private static readonly By MyCriteriaListLocator =
            By.XPath("//div[@id='co_viewEditReportContent']//div[contains(@ng-if,'sectionIsActive')]//span");

        private static readonly By ViewCriteriaButtonLocator = By.XPath("//div[@id='co_viewEditReportButton']/a");

        private static readonly By ContainerLocator = By.Id("co_docToolbarViewEditCriteria");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// get list of current view criteria
        /// </summary>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public List<string> GetCriteriaList()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(ViewCriteriaButtonLocator).Click();
            IReadOnlyCollection<IWebElement> criteriaElements = DriverExtensions.GetElements(MyCriteriaListLocator);

            return criteriaElements.Select(el => el.Text).ToList();
        }
    }
}