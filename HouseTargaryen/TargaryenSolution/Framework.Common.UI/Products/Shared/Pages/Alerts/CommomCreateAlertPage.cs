namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Common page for CreateAlertPage, CreateNewsletterPage, CreateReportPage
    /// </summary>
    public abstract class CommomCreateAlertPage : BaseModuleRegressionPage
    {
        private const int HeaderHeight = 86;

        private static readonly By AlertTitleLocator = By.CssSelector("div#co_alerts h1.co_alertWizardTitle");

        private static readonly By WaitDialogueTitleLocator = By.Id("co_docketUpdateWaitDialogTitle");

        private EnumPropertyMapper<AlertSections, AlertSectionInfo> editAlertSectionLinksMap;

        /// <summary> The WLN header </summary>
        public WestlawNextHeaderComponent Header { get; private set; } = new WestlawNextHeaderComponent();

        /// <summary> The basic component </summary>
        public BasicsComponent Basics { get; } = new BasicsComponent();

        /// <summary>
        /// Alert BreadCrumb Component
        /// </summary>
        public AlertBreadCrumbComponent AlertBreadcrumb { get; private set; } = new AlertBreadCrumbComponent();

        /// <summary>
        /// Customize Delivery
        /// </summary>
        public CustomizeDeliveryComponent CustomizeDelivery { get; private set; } = new CustomizeDeliveryComponent();

        /// <summary>
        /// Schedule Alert
        /// </summary>
        public ScheduleAlertComponent ScheduleAlert { get; private set; } = new ScheduleAlertComponent();

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        protected EnumPropertyMapper<AlertSections, AlertSectionInfo> EditAlertSectionLinksMap
            =>
                this.editAlertSectionLinksMap =
                    this.editAlertSectionLinksMap ?? EnumPropertyModelCache.GetMap<AlertSections, AlertSectionInfo>();

        /// <summary>
        /// Verify that edit link is displayed
        /// </summary>
        /// <param name="alertSection"> Edit link to verify </param>
        /// <returns> True if link is displayed, false otherwise </returns>
        public bool IsEditLinkDisplayed(AlertSections alertSection)
            => DriverExtensions.IsDisplayed(By.Id(this.EditAlertSectionLinksMap[alertSection].EditSectionLinkId), 5);

        /// <summary>
        /// Click on the edit link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="alertSection"> Link to click </param>
        /// <returns> New instance of the page </returns>
        public T ClickEditLink<T>(AlertSections alertSection) where T : ICreatablePageObject
        {
            DriverExtensions.ScrollIntoView(By.Id(this.EditAlertSectionLinksMap[alertSection].SectionId), HeaderHeight);
            DriverExtensions.WaitForJavaScript(5000);
            DriverExtensions.Click(DriverExtensions.WaitForElement(By.Id(this.EditAlertSectionLinksMap[alertSection].EditSectionLinkId)));
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the Alert title
        /// </summary>
        /// <returns>Element text</returns>
        public string GetAlertTitle() => DriverExtensions.GetText(AlertTitleLocator);

        /// <summary>
        /// Checks if the WaitDialogTitle is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsWaitDialogTitleDisplayed() => DriverExtensions.IsDisplayed(WaitDialogueTitleLocator);

        /// <summary> Fills out the basics and returns the next component after hitting continue </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="title"> The alert title </param>
        /// <param name="description"> The alert description. Optional </param>
        /// <returns> The next component in the alert creation process </returns>
        public T FillOutBasicsSection<T>(string title, string description = "") where T : BaseAlertComponent
            => this.Basics.SetNameText(title).EnterDescriptionText(description).ClickContinue<T>();
    }
}
