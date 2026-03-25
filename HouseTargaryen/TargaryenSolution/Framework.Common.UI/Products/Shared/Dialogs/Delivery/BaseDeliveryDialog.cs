namespace Framework.Common.UI.Products.Shared.Dialogs.Delivery
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Delivery;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Components.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Delivery Dialog Base page to implement common methods needed for Email, Download, Export etc delivery actions
    /// </summary>
    public abstract class BaseDeliveryDialog : BaseModuleRegressionDialog
    {
        private static readonly By CancelButtonLocator = By.Id("co_deliveryCancelButton");

        private static readonly By XCloseButtonLocator = By.XPath("//button[@class='co_overlayBox_closeButton co_iconBtn']");

        private static readonly By DeliveryDialogLocator = By.ClassName("co_overlayBox_delivery");

        private static readonly By DialogTitleLocator = By.XPath("//div[@class='co_overlayBox_headline']//h3 | //div[@class='co_overlayBox_headline']//h2");

        private static readonly By LoadingSpinnerLocator = By.Id("co_deliveryWaitProgress");

        private static readonly By SelectedTabLocator = By.XPath("//*[contains(@id,'co_deliveryOptions') and contains(@class,'Tab--active')]");

        private static readonly By Eamillocater = By.Id("co_deliveryEmailButton");

        private static readonly By Printloactor = By.Id("co_deliveryPrintButton");

        private static readonly By Downloadloactor = By.Id("co_deliveryDownloadButton");

        private static readonly By Kindleloactor = By.Id("co_deliverySendButton");


        private EnumPropertyMapper<DeliveryDialogTab, WebElementInfo> deliveryTabMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDeliveryDialog"/> class. 
        /// Constructs the Widgets Base page for delivery including export
        /// </summary>
        protected BaseDeliveryDialog()
        {
            this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(default(DeliveryDialogTab), null);
        }

        /// <summary>
        /// Content To Append Tab Component
        /// </summary>
        public ContentToAppendTabComponent ContentToAppendTab
            => this.SetActiveTab<ContentToAppendTabComponent>(DeliveryDialogTab.ContentToAppend);

        /// <summary>
        /// Layout And Limits Tab Component
        /// </summary>
        public LayoutAndLimitsTabComponent LayoutAndLimitsTab
            => this.SetActiveTab<LayoutAndLimitsTabComponent>(DeliveryDialogTab.LayoutAndLimits);

        /// <summary>
        /// What to deliver Component
        /// ToDo: remove from here
        /// </summary>
        public WhatToDeliverComponent WhatToDeliver { get; } = new WhatToDeliverComponent();

        /// <summary>
        /// Canada Layout And Limits Tab Component
        /// </summary>
        public CanadaLayoutAndLimitsTabComponent CanadaLayoutAndLimitsTab =>
            this.SetActiveTab<CanadaLayoutAndLimitsTabComponent>(DeliveryDialogTab.LayoutAndLimits);

        /// <summary>
        /// Taxnet Pro3 Delivery Basic tab component
        /// </summary>
        public TaxnetProDeliveryBasicTabComponent TaxnetProBasicTabComponent =>
            this.SetActiveTab<TaxnetProDeliveryBasicTabComponent>(DeliveryDialogTab.TheBasics);

        /// <summary>
        /// Active Tab
        /// </summary>
        protected KeyValuePair<DeliveryDialogTab, BaseTabComponent> ActiveTab { get; set; }

        /// <summary>
        /// Delivery Dialog Tabs Mapper
        /// </summary>
        protected EnumPropertyMapper<DeliveryDialogTab, WebElementInfo> DeliveryTabMap
            => this.deliveryTabMap = this.deliveryTabMap ?? EnumPropertyModelCache.GetMap<DeliveryDialogTab, WebElementInfo>();

        /// <summary>
        /// Clicks the Cancel link on the Delivery options dialogs
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCancel<T>() where T : ICreatablePageObject => this.ClickElement<T>(CancelButtonLocator);

        /// <summary>
        /// Clicks specified Delivery Dialog Tab
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="tab"> tab </param>
        /// <returns> New instance of the page </returns>
        public BaseTabComponent ClickTab<T>(DeliveryDialogTab tab) where T : BaseTabComponent
        {
            DriverExtensions.WaitForElement(By.XPath(this.DeliveryTabMap[tab].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the dialog title text
        /// </summary>
        /// <returns>the title text from the dialog headline element</returns>
        public string GetDialogTitle() => DriverExtensions.WaitForElement(DialogTitleLocator).Text;

        /// <summary>
        /// Gets the currently active/selected delivery dialog tab
        /// </summary>
        /// <returns>the current active delivery dialog tab</returns>
        public DeliveryDialogTab GetSelectedTab()
        {
            DriverExtensions.WaitForElementDisplayed(SelectedTabLocator);
            return DriverExtensions.GetText(DeliveryDialogLocator, SelectedTabLocator).GetEnumValueByText<DeliveryDialogTab>();
        }

        /// <summary>
        /// Verifies that cancel button is displayed.
        /// </summary>
        /// <returns> True if cancel button is displayed </returns>
        public bool IsCancelButtonDisplayed() => DriverExtensions.IsDisplayed(CancelButtonLocator, 5);

        /// <summary>
        /// 
        /// </summary>
        public IButton Email => new Button(Eamillocater);

        /// <summary>
        /// 
        /// </summary>
        public IButton Print => new Button(Printloactor);

        /// <summary>
        /// 
        /// </summary>
        public IButton Download => new Button(Downloadloactor);

        /// <summary>
        /// 
        /// </summary>
        public IButton Kindle => new Button(Kindleloactor);

        /// <summary>
        /// Is X Close Button Displayed
        /// </summary>
        /// <returns> True if C close button is displayed </returns>
        public bool IsXCloseButtonDisplayed() => DriverExtensions.IsDisplayed(XCloseButtonLocator);

        /// <summary>
        /// Determines whether or not a specified tab on the delivery dialog is displayed
        /// </summary>
        /// <param name="tab">the tab to determine the visibility of</param>
        /// <returns>true if the tab is present and displayed</returns>
        public bool IsTabDisplayed(DeliveryDialogTab tab)
            => DriverExtensions.IsDisplayed(By.Id(this.DeliveryTabMap[tab].Id));

        /// <summary>
        /// Click on the button and wait when spinner will disappear
        /// </summary>
        /// <param name="buttonLocator"> Button to click </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        protected T ClickButtonAndWaitSpinnerToDisappear<T>(By buttonLocator) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElementDisplayed(buttonLocator);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.Click(buttonLocator);            
            return this.WaitForUpdateComplete<T>(2100000, LoadingSpinnerLocator);
        }

        /// <summary>
        /// This method returns either new instance of the tab by clicking on it or existing instance. 
        /// In addition to that, this method set to null all inactive tabs.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="tabId">tabId </param>
        /// <returns> New instance of the page </returns>
        protected T SetActiveTab<T>(DeliveryDialogTab tabId) where T : BaseTabComponent
        {
            // Added this wait condition as Delivery dialog is taking time to load the tabs
            DriverExtensions.WaitForElementDisplayed(By.XPath(this.DeliveryTabMap[tabId].LocatorString));

            if (this.ActiveTab.Key != tabId || this.ActiveTab.Value == null)
            {
                this.ActiveTab = new KeyValuePair<DeliveryDialogTab, BaseTabComponent>(tabId, this.ClickTab<T>(tabId));
            }

            return this.ActiveTab.Value as T;
        }
    }
}