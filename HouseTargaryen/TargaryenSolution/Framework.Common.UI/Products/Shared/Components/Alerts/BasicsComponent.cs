namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Basics Section
    /// </summary>
    public class BasicsComponent : BaseAlertComponent
    {
        private static readonly By AssignToAlertGroupLinkLocator =
            By.Id("coid_alerts_widgets_alertGroups_selector_assignToLink");

        private static readonly By AssignToTextboxLocator = By.Id("coid_alerts_widgets_alertGroups_selector_assignToPath");

        private static readonly By BasicsHeaderLocator = By.XPath("//*[@id='basicsBellowHeader']/strong");

        private static readonly By BasicComponentLocator = By.Id("basicOptionsDiv");

        private static readonly By ChangeClientIdLinkLocator = By.XPath("//*[@class='co_clientIDInline_change']");

        private static readonly By CitationTextboxLocator = By.Id("optionsAlertCitation");

        private static readonly By ClientIdLocator = By.ClassName("co_clientIDInline_recent");

        private static readonly By DescriptionTextboxLocator = By.XPath("//textarea[@id='optionsAlertDescription' or @id='coid_newsletterDescription']");

        private static readonly By NameTextboxLocator = By.XPath("//input[@id='optionsAlertName' or @id='coid_newsletterName']");

        private static readonly By SummaryAlertNameLocator = By.Id("summaryAlertName");

        private static readonly By SupportedJurisdictionsTextLocator =
            By.XPath("//*[@id='alertGroupsSelectorContainer']/div");

        private static readonly By CloseWarningMessageButtonLocator =
            By.XPath("//div[@id='co_alerts']//a[@class='co_infoBox_closeButton']");

        private static readonly By WarningMessageLocator =
            By.XPath("//div[@id='co_alerts']//div[@class='co_infoBox_message']");

        private static readonly By WarningMessageLinkLocator = By.XPath("./a[contains(text(),'Training and support')]");

        private static readonly By BasicNameFieldLocator = By.Id("co_basicNameField");

        private static readonly By CitationLocator = By.Id("optionsAlertCitation");

        private static readonly By EditAlertGroupLinkLocator = By.Id("coid_alerts_widgets_alertGroups_selector_editAlertGroupsLink");

        private static readonly By RemoveAlertGroupLinkLocator = By.Id("coid_alerts_widgets_alertGroups_selector_cancelAlertGroupsLink");

        private static readonly By SaveAlertButtonLocator = By.Id("co_button_edit_save_Basics");

        /// <summary>
        ///  Basic name field label
        /// </summary>
        public ILabel BasicNameFieldLabel => new Label(BasicNameFieldLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => BasicComponentLocator;

        /// <summary>
        /// Changes the client Id to the specified value using the alert client id box
        /// </summary>
        /// <param name="clientId">
        /// The client id to use
        /// </param>
        public void ChangeClientId(string clientId)
            => this.ClickChangeClientIdLink<ChangeClientIdDialog>().EnterClientIdAndHitContinue<CreateAlertPage>(clientId);

        /// <summary>
        /// Click Assign to Alert Group link.
        /// </summary>
        /// <returns>
        /// The <see cref="AlertGroupsDialog"/>.
        /// </returns>
        public AlertGroupsDialog ClickAssignToAlertGroupLink()
        {
            DriverExtensions.GetElement(AssignToAlertGroupLinkLocator).Click();
            return new AlertGroupsDialog();
        }

        /// <summary>
        /// Click Edit link to edit alert group.
        /// </summary>
        /// <returns>
        /// The <see cref="AlertGroupsDialog"/>.
        /// </returns>
        public AlertGroupsDialog ClickEditAssignToAlertGroupLink()
        {
            DriverExtensions.GetElement(EditAlertGroupLinkLocator).Click();
            return new AlertGroupsDialog();
        }

        /// <summary>
        /// Click Remove link to remove assigned alert group.
        /// </summary>
        /// <returns>
        /// The <see cref="CreateAlertPage"/>.
        /// </returns>
        public BasicsComponent ClickRemoveAssignToAlertGroupLink()
        {
            DriverExtensions.GetElement(RemoveAlertGroupLinkLocator).Click();
            return this;
        }

        /// <summary>
        /// Clicks the change client id link
        /// </summary>
        /// <typeparam name="T"> Type of the page </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickChangeClientIdLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(ChangeClientIdLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enters citation text.
        /// </summary>
        /// <param name="inputText"> The text to enter. </param>
        /// <returns> The <see cref="BasicsComponent"/>. </returns>
        public BasicsComponent EnterCitationText(string inputText)
        {
            DriverExtensions.WaitForElement(CitationTextboxLocator).SetTextField(inputText);
            return this;
        }

        /// <summary>
        /// Enters name text.
        /// </summary>
        /// <param name="inputText"> The text to enter. </param>
        /// <returns> The <see cref="BasicsComponent"/>. </returns>
        public BasicsComponent EnterDescriptionText(string inputText)
        {
            DriverExtensions.WaitForElement(DescriptionTextboxLocator).SetTextField(inputText);
            return this;
        }

        /// <summary>
        /// Get assigned to group link text.
        /// </summary>
        /// <returns> assigned to group link text </returns>
        public string GetAssignedToGroupLinkText() => DriverExtensions.GetText(AssignToAlertGroupLinkLocator);

        /// <summary>
        ///  Assign to group link
        /// </summary>
        public ILink AssignToGroupLink => new Link(AssignToAlertGroupLinkLocator);

        /// <summary>
        /// Get assigned to group name.
        /// </summary>
        /// <returns> assigned to group name </returns>
        public string GetAssignedToGroup() => DriverExtensions.GetText(AssignToTextboxLocator);

        /// <summary>
        /// Get basics header text.
        /// </summary>
        /// <returns>Basics section name</returns>
        public string GetBasicsHeaderText() => DriverExtensions.GetText(BasicsHeaderLocator);

        /// <summary>
        /// Gets the citation text in the textbox
        /// </summary>
        /// <returns> The citation text </returns>
        public string GetCitationText() => DriverExtensions.GetText(CitationTextboxLocator);

        /// <summary>
        /// Gets the client id text in the box
        /// </summary>
        /// <returns> Client Id </returns>
        public string GetClientIdText() => DriverExtensions.WaitForElementDisplayed(ClientIdLocator).Text;

        /// <summary>
        /// Gets the alert Description test
        /// </summary>
        /// <returns> Description </returns>
        public string GetDescriptionText() => DriverExtensions.GetText(DescriptionTextboxLocator);

        /// <summary>
        /// Gets the alert name test
        /// </summary>
        /// <returns>string</returns>
        public string GetNameText() => DriverExtensions.GetText(NameTextboxLocator);

        /// <summary>
        /// Gets summary alert name.
        /// </summary>
        /// <returns>Alert name from summary</returns>
        public string GetSummaryAlertName() => DriverExtensions.GetText(SummaryAlertNameLocator);

        /// <summary>
        /// Checks if the alert name entry textbox exists
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsAlertNameTextboxDisplayed() => DriverExtensions.IsDisplayed(NameTextboxLocator);

        /// <summary>
        /// Checks change client Id link is displayed on the page
        /// </summary>
        /// <returns> True If the change client Id link is displayed, false otherwise </returns>
        public bool IsChangeClientIdLinkDisplayed() => DriverExtensions.IsDisplayed(ChangeClientIdLinkLocator);

        /// <summary>
        /// Checks Edit Alert group link is displayed on the page
        /// </summary>
        /// <returns> True If the Edit alert link is displayed, false otherwise </returns>
        public bool IsEditAlertGroupLinkDisplayed() => DriverExtensions.IsDisplayed(EditAlertGroupLinkLocator);

        /// <summary>
        /// Checks Remove Alert group link is displayed on the page
        /// </summary>
        /// <returns> True If the Remove alert link is displayed, false otherwise </returns>
        public bool IsRemoveAlertGroupLinkDisplayed() => DriverExtensions.IsDisplayed(RemoveAlertGroupLinkLocator);

        /// <summary>
        /// Checks if a content component is opened </summary>
        /// <returns>True if the basics component is open</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Checks the client Id textbox is displayed on the page
        /// </summary>
        /// <returns> True If the client id is displayed, false otherwise </returns>
        public bool IsClientIdWidgetDisplayed() => DriverExtensions.IsDisplayed(ClientIdLocator);

        /// <summary>
        /// Get Supported Jurisdictions Text 
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetSupportedJurisdictionsText() => DriverExtensions.GetText(SupportedJurisdictionsTextLocator);

        /// <summary>
        /// Click warning message link and open new browser tab
        /// </summary>
        /// <typeparam name="T">Page to return</typeparam>
        /// <param name="newTabName">The new Tab Name.</param>
        /// <returns> New Page Object</returns>
        public T ClickWarningMessageLinkAndOpenNewBrowserTab<T>(string newTabName)
            where T : ICreatablePageObject =>
            this.ClickAndOpenNewBrowserTab<T>(
                DriverExtensions.GetElement(WarningMessageLocator, WarningMessageLinkLocator),
                newTabName);

        /// <summary>
        /// Close the warning message
        /// </summary>
        public void CloseWarningMessage() => DriverExtensions.GetElement(CloseWarningMessageButtonLocator).Click();

        /// todo: make warning message separate component to use on basic and select content alert components.
        /// todo: Check if created infrastructure for infoboxes can be used there
        /// <summary>
        /// Get text from the Warning message
        /// </summary>
        /// <returns> Warning message </returns>
        public string GetWarningMessageText() => DriverExtensions.GetText(WarningMessageLocator);

        /// <summary> 
        /// Enters name text. 
        /// </summary>
        /// <param name="inputText"> The text to enter. </param>
        /// <returns> The <see cref="BasicsComponent"/>. </returns>
        public BasicsComponent SetNameText(string inputText)
        {
            DriverExtensions.WaitForElementDisplayed(NameTextboxLocator).SetTextField(inputText);
            return this;
        }

        /// <summary>
        /// Enters citation.
        /// </summary>
        /// <param name="inputText"> The text to enter. </param>
        /// <returns> The <see cref="BasicsComponent"/>. </returns>
        public BasicsComponent EnterCitation(string inputText)
        {
            DriverExtensions.WaitForElement(CitationLocator).SetTextField(inputText);
            return this;
        }

        /// <summary>
        /// Click on save alert button to save the updated basics and go back to alert list page.
        /// </summary>
        /// <returns>
        /// The <see cref="AlertCenterPage"/>.
        /// </returns>
        public AlertCenterPage ClickOnSaveAlertButton()
        {
            DriverExtensions.WaitForElement(SaveAlertButtonLocator).Click();
            return new AlertCenterPage();
        }
    }
}