namespace Framework.Common.UI.Products.Shared.Pages.Tools
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.FindAndPrint;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// FindAndPrintPage
    /// </summary>
    public class FindAndPrintPage : CommonAuthenticatedWestlawNextPage
    {
        private const string DeliveryOptionRadiobuttonLctMask = "//ul[@id='deliveryOptions']//label[contains(.,'{0}')]/input";

        private static readonly By CitationTextBoxLocator = By.XPath("//textarea[@id='co_citations']");

        private static readonly By SubmitButtonLocator = By.XPath("//div[@class='submitDiv']/input[@id='submitBtn']");

        private static readonly By FindAndPrintHeaderLocator = By.XPath("//div[@id='co_subHeader']//h1[@id='H1' and text()='Find & Print']");

        private static readonly By EmailTextboxLocator = By.Id("coid_contacts_addedContactsInput_co_collaboratorWidget");

        private static readonly By NewUserTextboxLocator = By.CssSelector(".co_contacts_collector_addNew input");

        private static readonly By MyContactsButtonLocator = By.Id("co_delivery_open_addressBook");

        private static readonly By RemoveEmailRecipientsLocator =
            By.XPath("//ul[contains(@id, 'coid_contacts_addedContactsInput_co_collaboratorWidget')]//button[@type='button']");

        private static readonly By DeliveryAsDropDownLocator = By.XPath("//select[@id= 'co_delivery_fileContainer' or @name = 'co_delivery_fileContainer']");

        private static readonly By DeliveryFormatDropdownLocator
            = By.XPath("//div[not(contains(@style, 'display: none'))]/select[contains(@id, 'co_delivery_format')]");

        /// <summary>
        /// The delivery method map.
        /// </summary>
        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> deliveryMethodMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="FindAndPrintPage"/> class. 
        /// FindAndPrintPage
        /// </summary>
        public FindAndPrintPage()
        {
            DriverExtensions.WaitForElementDisplayed(FindAndPrintHeaderLocator);
        }

        /// <summary>
        /// My contact button
        /// </summary>
        public IButton MyContactsButton => new Button(MyContactsButtonLocator);

        /// <summary>
        /// DocumentOptionsComponent
        /// </summary>
        public DocumentOptionsComponent DocumentsOptionsComponent => new DocumentOptionsComponent();

        /// <summary>
        /// KeyCiteOptionsComponent
        /// </summary>
        public KeyCiteOptionsComponent KeyCiteOptionsComponent => new KeyCiteOptionsComponent();

        /// <summary>
        /// LayoutAndLimits component
        /// </summary>
        public LayoutsAndLimitsComponent LayoutsAndLimitsComponent => new LayoutsAndLimitsComponent();

        /// <summary>
        /// Deliver as dropdown
        /// </summary>
        public IDropdown<string> DeliveryAsDropdown => new Dropdown(DeliveryAsDropDownLocator);

        /// <summary>
        /// Gets the format dropdown.
        /// </summary>
        public IDropdown<DeliveryFormat> FormatDropdown => new Dropdown<DeliveryFormat>(DeliveryFormatDropdownLocator);

        /// <summary>
        /// Get a list of email labels
        /// </summary>
        /// <returns>A list of treatment links</returns>
        private IReadOnlyCollection<IButton> EmailRecipients => new ElementsCollection<Button>(RemoveEmailRecipientsLocator);

        /// <summary>
        /// Gets the Delivery Method Map.
        /// </summary>
        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> DeliveryMethodMap
            => this.deliveryMethodMap = this.deliveryMethodMap ?? EnumPropertyModelCache.GetMap<DeliveryMethod, WebElementInfo>();

        /// <summary>
        /// Delete All Recipients And Add New Recipient
        /// Note - Set Delivery Checkbox and Toggle Email Settings Section is also called
        /// </summary>
        /// <param name="email"> email </param>
        public void AddNewRecipient(string email)
        {
            if (!DriverExtensions.IsDisplayed(NewUserTextboxLocator))
            {
                DriverExtensions.GetElement(EmailTextboxLocator).Click();
                DriverExtensions.WaitForPageLoad();
                DriverExtensions.WaitForJavaScript();
            }

            DriverExtensions.GetElement(NewUserTextboxLocator).SendKeys(email);
            DriverExtensions.GetElement(NewUserTextboxLocator).SendKeys(Keys.Enter);
        }

        /// <summary>
        /// Get Email Recipients List
        /// </summary>
        /// <returns>List of items</returns>
        public List<string> GetEmailRecipientsList()
            => this.EmailRecipients.Select(email => email.Text).ToList();

        /// <summary>
        /// Delete specific email recipient
        /// </summary>
        /// <param name="email">email</param>
        public void ClickOnEmailRecipient(string email)
            => this.EmailRecipients.First(el => el.Text.Equals(email)).Click();

        /// <summary>
        /// Deletes all current Email Recipients
        /// </summary>
        public void DeleteAllEmailRecipients()
        {
            this.EmailRecipients.ToList().ForEach(email => email.Click());

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// click submit button
        /// </summary>
        /// <typeparam name="TPage"> Page type </typeparam>
        /// <returns>The type of page.</returns>
        public TPage ClickSubmit<TPage>() where TPage : ICreatablePageObject
        {
            DriverExtensions.ScrollTo(SubmitButtonLocator);
            DriverExtensions.WaitForElement(SubmitButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Enter citation
        /// </summary>
        /// <param name="citations">Citations</param>
        public void EnterCitations(params string[] citations)
            => DriverExtensions.GetElement(CitationTextBoxLocator).SendKeys(string.Join(";", citations));

        /// <summary>
        /// This method selects the given delivery option. If the delivery option is Email, user's email id is added to To Field
        /// </summary>
        /// <param name="deliveryMethod"> The delivery Method. </param>
        public void SelectDeliveryOption(DeliveryMethod deliveryMethod)
        {
            DriverExtensions.Click(
                DriverExtensions.WaitForElement(
                    By.XPath(string.Format(DeliveryOptionRadiobuttonLctMask, this.DeliveryMethodMap[deliveryMethod].Text))));
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Enter citation
        /// </summary>
        /// <param name="citation">Citations</param>
        public void EnterCitations(string citation)
            => DriverExtensions.GetElement(CitationTextBoxLocator).SendKeys(citation);
    }
}