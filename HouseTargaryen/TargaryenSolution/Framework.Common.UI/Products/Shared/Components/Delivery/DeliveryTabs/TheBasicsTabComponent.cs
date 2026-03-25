namespace Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Components.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The Basics Tab Component
    /// </summary>
    public class TheBasicsTabComponent : BaseTabComponent
    {
        private static readonly By InfoMessageLocator = By.XPath("//div[@id='co_deliveryOptionsTabPanel1']//div[@class='co_infoBox_message']");

        private static readonly By KindleEmailAddressLocator = By.Id("co_delivery_emailAddress");

        private static readonly By ContainerLocator = By.Id("co_deliveryOptionsTabPanel1");

        private static readonly By NumberToDeliverDropdownLocator
            = By.XPath("//div[contains(@class,'co_formTextSelect') and not(contains(@style,'none'))]//select[@id='coid_RecipientDeliverAsFullTextCount' or @id='coid_RecipientDeliverAsListCount']");

        private static readonly By DeliveryAsDropDownLocator = By.XPath("//select[@id= 'co_delivery_fileContainer' or @name = 'co_delivery_fileContainer']");

        private static readonly By DeliveryFormatDropdownLocator
            = By.XPath("//div[not(contains(@style, 'display: none'))]/select[contains(@id, 'co_delivery_format')]");

        private static readonly By IncludeUnSubscribedContentCheckboxLocator = By.Id("coid_chkDdcIncludeUnSubscribedContent");

        private EnumPropertyMapper<TheBasicsTabOption, WebElementInfo> theBasicsMap;

        /// <summary>
        /// What To Deliver component
        /// </summary>
        public WhatToDeliverComponent WhatToDeliver { get; } = new WhatToDeliverComponent();

        /// <summary>
        /// NumberOfDocumentsDropdown
        /// </summary>
        public IDropdown<string> NumberToDeliver { get; } = new Dropdown(NumberToDeliverDropdownLocator);

        /// <summary>
        /// Deliver as dropdown
        /// </summary>
        public IDropdown<string> DeliveryAsDropdown { get; } = new Dropdown(DeliveryAsDropDownLocator);

        /// <summary>
        /// Gets the format dropdown.
        /// </summary>
        public IDropdown<DeliveryFormat> FormatDropdown { get; } = new Dropdown<DeliveryFormat>(DeliveryFormatDropdownLocator);

        /// <summary>
        /// Number of items component
        /// </summary>
        public NumberOfItemsRadiobuttonComponent NumberOfItemsRadiobutton { get; } = new NumberOfItemsRadiobuttonComponent();

        /// <summary>
        /// Gets the TheBasicsTabOption enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<TheBasicsTabOption, WebElementInfo> TheBasicsMap
            => this.theBasicsMap = this.theBasicsMap ?? EnumPropertyModelCache.GetMap<TheBasicsTabOption, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "The Basics";

        /// <summary>
        /// Get the message when attempting to deliver the folder containing Km document and compare to required message
        /// </summary>
        /// <param name="infoMessage"> The info Message. </param>
        /// <returns> True info message is displayed, false otherwise </returns>
        public bool IsDeliveryMessageDisplayed(string infoMessage)
            => DriverExtensions.WaitForElement(InfoMessageLocator).Text.Equals(infoMessage);

        /// <summary>
        /// Verify the specified option on The Basics tab is displayed
        /// </summary>
        /// <param name="expectedTabOption"> The expected option on The Basics tab </param>
        /// <returns> True if the option is displayed, false otherwise </returns>
        public bool IsTabOptionDisplayed(TheBasicsTabOption expectedTabOption)
            => DriverExtensions.IsDisplayed(By.Id(this.TheBasicsMap[expectedTabOption].Id));

        /// <summary>
        /// Verify TabOption is Enabled
        /// </summary>
        /// <param name="expectedTabOption"> The expected option on The Basics tab </param>
        /// <returns> True if the option is enabled, false otherwise </returns>
        public bool IsTabOptionEnabled(TheBasicsTabOption expectedTabOption)
            => DriverExtensions.IsEnabled(By.Id(this.TheBasicsMap[expectedTabOption].Id));

        /// <summary>
        /// The set kindle email address.
        /// </summary>
        /// <param name="emailAddress">
        /// The email address.
        /// </param>
        public void SetKindleEmailAddress(string emailAddress) => DriverExtensions.SetTextField(emailAddress, KindleEmailAddressLocator);

        /// <summary>
        /// Un Subscribed Content Checkbox
        /// </summary>
        public ICheckBox UnSubscribedContentCheckbox => new CheckBox(this.ComponentLocator, IncludeUnSubscribedContentCheckboxLocator);
    }
}