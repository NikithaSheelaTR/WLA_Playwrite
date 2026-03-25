namespace Framework.Common.UI.Products.WestLawNextCanada.Components
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// MyAlerts Widget on homepage
    /// </summary>
    public class MyAlertsComponent : BaseModuleRegressionComponent
    {
        private static readonly By AlertsListLocator = By.XPath(".//*[@id='co_selectAlerts_list']//input");
        private static readonly By AlertsNameLocator = By.XPath(".//*[@class= 'co_alertName']");
        private static readonly By AlertsRemoveLinkLocator = By.XPath(".//*[@class='co_alertDelete']");
        private static readonly By DoneOrganizeLinkLocator = By.XPath(".//*[@id= 'co_myAlerts_doneOrganizing']");
        private static readonly By MyAlertsComponentLocator = By.XPath(".//*[@id= 'cp_myAlertsToolSection']");
        private static readonly By NoAlertsGenerationHeaderLocator = By.XPath(".//*[@class='co_noResults']/h3");
        private static readonly By NoAlertGenearationMessageLocator = By.XPath(".//*[@class='co_noResults']/p");
        private static readonly By OrganizeLinkLocator = By.XPath(".//*[@id= 'co_myAlerts_organize']");
        private static readonly By SelectContentAlertsLocator = By.XPath(".//*[@id='co_myAlerts_select']/button");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => MyAlertsComponentLocator;

        /// <summary>
        /// MyAlertsWidget
        /// </summary>
        public IWebElement MyAlertsWidget => DriverExtensions.GetElement(this.ComponentLocator);

        /// <summary>
        /// OrganizeAlertsLink
        /// </summary>
        public ILink OrganizeAlertsLink => new Link(this.ComponentLocator, OrganizeLinkLocator);

        /// <summary>
        /// DoneOrganizeAlertsLink
        /// </summary>
        public ILink DoneOrganizeAlertsLink => new Link(this.ComponentLocator, DoneOrganizeLinkLocator);

        /// <summary>
        /// SelectContentAlertsbutton
        /// </summary>
        public IButton SelectContentAlertsButton => new Button(this.ComponentLocator, SelectContentAlertsLocator);

        /// <summary>
        /// AlertsRemoveLinks
        /// </summary>
        public IReadOnlyCollection<ILink> AlertsRemoveLinks => new ElementsCollection<Link>(AlertsRemoveLinkLocator);

        /// <summary>
        /// AlertsListCheckBoxes
        /// </summary>
        public IReadOnlyCollection<ICheckBox> AlertsListCheckBoxes => new ElementsCollection<CheckBox>(AlertsListLocator);

        /// <summary>
        /// AlertsNameLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> AlertsNameLabels => new ElementsCollection<Label>(AlertsNameLocator);

        /// <summary>
        /// NoAlertsGenerationHeaderLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> NoAlertsGenerationHeaderLabels => new ElementsCollection<Label>(NoAlertsGenerationHeaderLocator);

        /// <summary>
        /// NoAlertGenerationMessageLabel
        /// </summary>
        public IReadOnlyCollection<ILabel> NoAlertGenerationMessageLabels => new ElementsCollection<Label>(NoAlertGenearationMessageLocator);
    }
}