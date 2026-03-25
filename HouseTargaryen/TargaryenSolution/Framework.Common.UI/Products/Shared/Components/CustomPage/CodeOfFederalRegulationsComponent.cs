namespace Framework.Common.UI.Products.Shared.Components.CustomPage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.CustomPage.CodeOfFederalRegulationsWidget;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Code of Federal Regulations widget
    /// </summary>
    public class CodeOfFederalRegulationsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(
            "//span[text() ='Code of Federal Regulations']//ancestor-or-self::div[@id ='cp_citationDeliveryToolSection']");

        private static readonly By EmailTextboxMessageLocator = By.XPath("//div[@class = 'co_formInline-inputHint']");
        private static readonly By GoButtonLocator = By.XPath(".//button[@id = 'coid_deliverCitation']");
        private static readonly By ClearFormButtonLocator = By.XPath(".//button[@id = 'coid_clearForm']");
        private static readonly By TitleIdFieldLocator = By.Id("co_cfrCitationTitleId");
        private static readonly By SectionIdFieldLocator = By.Id("co_cfrCitationSectionId");
        private static readonly By DeliveryFormatDropdownLocator = By.XPath("//select[contains(@id, 'coid_deliveryFormatSelection')]");

        /// <summary>
        /// Gets the format dropdown.
        /// </summary>
        public IDropdown<DeliveryFormat> FormatDropdown { get; } = new Dropdown<DeliveryFormat>(DeliveryFormatDropdownLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Result options mapper
        /// </summary>
        protected EnumPropertyMapper<CfrResultOptions, WebElementInfo> ResultOptionsMap
            => EnumPropertyModelCache.GetMap<CfrResultOptions, WebElementInfo>();

        /// <summary>
        /// Deliver options mapper
        /// </summary>
        protected EnumPropertyMapper<CfrDeliveryOptions, WebElementInfo> DeliverOptionsMap
            => EnumPropertyModelCache.GetMap<CfrDeliveryOptions, WebElementInfo>();

        /// <summary>
        /// Enter citation
        /// </summary>
        /// <param name="titleId">Title number</param>
        /// <param name="sectionId">Section number</param>
        public void EnterCitation(string titleId, string sectionId)
        {
            this.EnterTitleId(titleId);
            this.EnterSectionId(sectionId);
        }

        /// <summary>
        /// Click on Go button
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickGoButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(ContainerLocator, GoButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on 'Clear Form' button
        /// </summary>
        public void ClickClearForm() => DriverExtensions.GetElement(ContainerLocator, ClearFormButtonLocator).Click();

        /// <summary>
        /// Select Result option
        /// </summary>
        /// <param name="option">Certain Result option</param>
        public void SelectResultOption(CfrResultOptions option) =>
            DriverExtensions.WaitForElement(By.Id(this.ResultOptionsMap[option].Id)).Click();

        /// <summary>
        /// Select Delivery option
        /// </summary>
        /// <param name="option">Certain Delivery option</param>
        public void SelectDeliveryOption(CfrDeliveryOptions option) =>
            DriverExtensions.WaitForElement(By.XPath(this.DeliverOptionsMap[option].LocatorString)).Click();

        /// <summary>
        /// Get Email text-box message
        /// </summary>
        /// <returns> Email text-box message</returns>
        public string GetEmailTextboxMessage() => DriverExtensions.GetText(EmailTextboxMessageLocator);

        /// <summary>
        /// Verify that Result option is selected
        /// </summary>
        /// <param name="option">Certain Result option</param>
        /// <returns>True if option is selected, false otherwise</returns>
        public bool IsResultOptionSelected(CfrResultOptions option) =>
            DriverExtensions.IsRadioButtonSelected(By.Id(this.ResultOptionsMap[option].Id));

        /// <summary>
        /// Verify that Result option is displayed
        /// </summary>
        /// <param name="option">Certain Result option</param>
        /// <returns>True if option is displayed, false otherwise</returns>
        public bool IsResultOptionDisplayed(CfrResultOptions option) =>
            DriverExtensions.IsDisplayed(By.Id(this.ResultOptionsMap[option].Id));

        /// <summary>
        /// Verify that Delivery option is selected
        /// </summary>
        /// <param name="option">Certain Delivery option</param>
        /// <returns>True if option is selected, false otherwise</returns>
        public bool IsDeliveryOptionSelected(CfrDeliveryOptions option) =>
            DriverExtensions.IsRadioButtonSelected(By.XPath(this.DeliverOptionsMap[option].LocatorString));

        /// <summary>
        /// Verify that Delivery option is displayed
        /// </summary>
        /// <param name="option">Certain Result option</param>
        /// <returns>True if option is displayed, false otherwise</returns>
        public bool IsDeliveryOptionDisplayed(CfrDeliveryOptions option) =>
            DriverExtensions.IsDisplayed(By.XPath(this.DeliverOptionsMap[option].LocatorString));

        private void EnterTitleId(string titleId) => DriverExtensions.GetElement(TitleIdFieldLocator).SendKeys(titleId);

        private void EnterSectionId(string sectionId) => DriverExtensions.GetElement(SectionIdFieldLocator).SendKeys(sectionId);
    }
}