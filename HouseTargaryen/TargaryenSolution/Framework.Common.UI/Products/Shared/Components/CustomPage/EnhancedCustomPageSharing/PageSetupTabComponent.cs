namespace Framework.Common.UI.Products.Shared.Components.CustomPage.EnhancedCustomPageSharing
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.CustomPages.EnhancedCustomPageSharing;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// PageSetupTab tab in AdminSettingsMenuPanel
    /// </summary>
    public class PageSetupTabComponent : BaseTabComponent
    {
        private static readonly By SaveButtonLocator = By.Id("co_CustomPagesPageSetupSave");
        private static readonly By CancelButtonLocator = By.Id("co_CustomPagesPageSetupCancel");
        private static readonly By NonBillableZoneBlockLocator = By.Id("toggleCheckboxes");
        private static readonly By SetAsStartPageCheckBoxLocator = By.Id("pageSetupStartPage-checkbox");
        private static readonly By SetAsNonBillableZoneCheckBoxLocator = By.Id("pageSetupNonBillable-checkbox");
        private static readonly By CreateElibraryCheckBoxLocator = By.Id("createLibrary-checkbox");
        private static readonly By ClientIdTextFieldLocator = By.Id("clientId-text");
        private static readonly By EnhancedSearchResultsCheckBoxLocator = By.Id("enhancedSearchResults-checkbox");
        private static readonly By EnhancedSearchResultsTextLocator =
            By.XPath("//input[@id='enhancedSearchResults-checkbox']/parent::label/following-sibling::p");

        private static readonly By ContainerLocator = By.Id("co_pageSetupContainer");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Page Setup";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Set start page check box.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetStartPageCheckBox(bool selected)
            => DriverExtensions.SetCheckbox(SetAsStartPageCheckBoxLocator, selected);

        /// <summary>
        /// Set non billable zone check box.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetNonBillableZoneCheckBox(bool selected)
            => DriverExtensions.SetCheckbox(SetAsNonBillableZoneCheckBoxLocator, selected);

        /// <summary>
        /// Set enhanced search results check box.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetEnhancedSearchResultsCheckBox(bool selected)
            => DriverExtensions.SetCheckbox(EnhancedSearchResultsCheckBoxLocator, selected);

        /// <summary>
        /// Set create elibrary check box.
        /// </summary>
        /// <param name="selected">
        /// The selected.
        /// </param>
        public void SetCreateElibraryCheckBox(bool selected)
            => DriverExtensions.SetCheckbox(CreateElibraryCheckBoxLocator, selected);

        /// <summary>
        /// Enter client id.
        /// </summary>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        /// <param name="clearFirst">
        /// The clear first.
        /// </param>
        public void EnterClientId(string clientId, bool clearFirst = true)
        {
            if (clearFirst)
            {
                DriverExtensions.WaitForElementDisplayed(ClientIdTextFieldLocator).Clear();
            }

            DriverExtensions.WaitForElement(ClientIdTextFieldLocator).SendKeys(clientId);
        }

        /// <summary>
        /// Is set start page check box selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSetStartPageCheckBoxSelected()
            => DriverExtensions.IsCheckboxSelected(SetAsStartPageCheckBoxLocator);

        /// <summary>
        /// Is set non billable zone check box selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSetNonBillableZoneCheckBoxSelected()
            => DriverExtensions.IsCheckboxSelected(SetAsNonBillableZoneCheckBoxLocator);

        /// <summary>
        /// Is set create elibrary check box selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSetCreateElibraryCheckBoxSelected()
            => DriverExtensions.IsCheckboxSelected(CreateElibraryCheckBoxLocator);

        /// <summary>
        /// Is non billable zone block disabled.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNonBillableZoneBlockDisabled()
            => DriverExtensions.WaitForElement(NonBillableZoneBlockLocator).GetAttribute("class").Equals("co_disabled");

        /// <summary>
        /// Is client id field enabled.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsClientIdFieldEnabled() => DriverExtensions.WaitForElement(ClientIdTextFieldLocator).Enabled;

        /// <summary>
        /// Get client id text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetClientIdText() => DriverExtensions.GetText(ClientIdTextFieldLocator);

        /// <summary>
        /// Is enhanced search results check box selected.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsEnhancedSearchResultsCheckBoxSelected()
            => DriverExtensions.IsCheckboxSelected(EnhancedSearchResultsCheckBoxLocator);

        /// <summary>
        /// Get description of enhanced check box.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDescriptionOfEnhancedCheckBox()
            => DriverExtensions.WaitForElement(EnhancedSearchResultsTextLocator).Text;

        /// <summary>
        /// The click save button.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The click cancel button.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Set sharing options for custom pages, exclude access permissions
        /// </summary>
        /// <param name="options">The options.</param>
        public void SetSharingOptions(CustomPageSharingModel options)
        {
            this.SetStartPageCheckBox(options.MakeStartPage);
            this.SetNonBillableZoneCheckBox(options.MakeNonBillableZone);

            if (options.MakeNonBillableZone)
            {
                this.SetCreateElibraryCheckBox(options.MakeELibrary);

                if (options.ZoneName != null && options.MakeELibrary)
                {
                    this.EnterClientId(options.ZoneName);
                }

                this.SetEnhancedSearchResultsCheckBox(options.MakeEnhancedSearchResults);
            }
        }
    }
}
