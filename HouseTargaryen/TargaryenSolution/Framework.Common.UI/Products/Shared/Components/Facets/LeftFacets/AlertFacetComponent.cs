namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alert Facet Component.
    /// </summary>
    public class AlertFacetComponent : BaseModuleRegressionComponent
    {
        /// <summary>
        /// The active_id.
        /// </summary>
        private const string ActiveCheckboxLctMask = "//input[@id='coid_alert_active_{0}']";

        /// <summary>
        /// The select groups link.
        /// </summary>
        private static readonly By SelectGroupsLinkLocator = By.Id("selectGroupsLink");

        private static readonly By ContainerLocator = By.Id("coid_facet_placeholder_alertGroup");

        private static readonly By WestlawTodayButtonLocator = By.XPath("//*[@id='coid_alert_alertType_LegalNewsComposite']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;       

        /// <summary>
        /// Click select Alert Groups link.
        /// </summary>
        /// <typeparam name="T"> Type of object to return. </typeparam>
        /// <returns> New page instance. </returns>
        public T ClickSelectAlertGroupsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(SelectGroupsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks that required filter checkbox displayed
        /// </summary>
        /// <param name="filterName">required filter name</param>
        /// <returns>true if displayed</returns>
        public bool IsFilterCheckboxDisplayed(string filterName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(ActiveCheckboxLctMask, filterName)));

        /// <summary>
        /// Select active facet.
        /// </summary>
        /// <param name="filterName"> Filter name. </param>
        /// <param name="select">True if select, false otherwise</param>
        public void SelectActiveFacet(string filterName, bool select)
            => DriverExtensions.SetCheckbox(By.XPath(string.Format(ActiveCheckboxLctMask, filterName)), @select);

        /// <summary>
        /// WestlawToday button
        /// </summary>
        public IButton WestlawTodayButton => new Button(this.ComponentLocator, WestlawTodayButtonLocator);
    }
}