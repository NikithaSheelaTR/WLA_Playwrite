namespace Framework.Common.UI.Products.Shared.Dialogs.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The alert groups dialog.
    /// </summary>
    public class AlertGroupsDialog : BaseModuleRegressionDialog
    {
        private const string GroupToAssignLctMask = "//ul[@id='coid_alerts_widgets_alertGroups_listItems']/li/div/*[@title={0}]";

        private static readonly By AssignButtonLocator = By.Id("coid_alerts_widgets_alertGroups_lightbox_assignButton");

        private static readonly By CreateGroupButtonLocator = By.Id("coid_alerts_widgets_alertGroups_createButton");

        private static readonly By SaveGroupButtonLocator = By.Id("coid_alerts_widgets_alertGroups_createSave");

        private static readonly By SaveGroupTextBoxLocator = By.Id("coid_alerts_widgets_alertGroups_createInput");

        private static readonly By CancelButtonLocator = By.Id("coid_alerts_widgets_alertGroups_lightbox_cancelLink");

        private static readonly By LightboxHeaderLocator = By.XPath("//*[contains(@id,'coid_lightboxAriaLabel')]");

        private static readonly By AllAlertGroupLinkLocator = By.XPath("//div[@id='breadcrumb']/span[@id='breadcrumbRoot']");

        /// <summary>
        ///  Alert groups lightbox header label
        /// </summary>
        public ILabel LightboxHeaderLabel => new Label(LightboxHeaderLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);

        /// <summary>
        /// Create group button
        /// </summary>
        public IButton CreateGroupButton => new Button(CreateGroupButtonLocator);

        /// <summary>
        /// Assign button
        /// </summary>
        public IButton AssignButton => new Button(AssignButtonLocator);

        /// <summary>
        /// Save group button
        /// </summary>
        public IButton SaveGroupButton => new Button(SaveGroupButtonLocator);

        /// <summary>
        /// All alert groups link
        /// </summary>
        public ILink AllAlertGroupsLink => new Link(AllAlertGroupLinkLocator);

        /// <summary>
        /// Select group button
        /// </summary>
        public IButton SelectGroupButton(string groupName) => 
            new Button(SafeXpath.BySafeXpath(GroupToAssignLctMask, groupName));

        /// <summary>
        /// Save group textbox
        /// </summary>
        public ITextbox SaveGroupTextbox => new Textbox(SaveGroupTextBoxLocator);

        /// <summary>
        /// Create Group.
        /// </summary>
        /// <param name="groupName"> Group name.  </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage CreateGroup(string groupName)
        {
            this.CreateGroupButton.Click();
            this.SaveGroupTextbox.SetText(groupName);
            this.SaveGroupButton.Click();
            return this.AssignButton.Click<CreateAlertPage>();
        }

        /// <summary>
        /// If group name present then assign else create and assign
        /// </summary>
        /// <param name="groupName"> Group name.  </param>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage AssignGroup(string groupName)
        {
            DriverExtensions.WaitForElementDisplayed(CreateGroupButtonLocator);
            if (this.SelectGroupButton(groupName).Displayed)
            {
                this.SelectGroupButton(groupName).Click();
                return this.AssignButton.Click<CreateAlertPage>();
            }
            else
            {
                return this.CreateGroup(groupName);
            }
        }

        /// <summary>
        /// Click All Alerts Group breadcrumb link.
        /// </summary>
        /// <returns>
        /// The <see cref="AlertGroupsDialog"/>.
        /// </returns>
        public AlertGroupsDialog ClickOnAllAlertsGroupsLink()
        {
            AllAlertGroupsLink.Click();
            return new AlertGroupsDialog();
        }
    }
}