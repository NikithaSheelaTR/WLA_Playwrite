namespace Framework.Common.UI.Products.Shared.Dialogs.CustomPages
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.CustomPage;
    using Framework.Common.UI.Products.Shared.Models.CustomPages;
    using Framework.Common.UI.Products.Shared.Pages.CustomPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Works with the Custom Page settings sharing widget actions
    /// </summary>
    public class ShareCustomPageSettingsDialog : ShareCustomPageBaseDialog
    {
        private static readonly By ContactItemSharedWithLocator = By.CssSelector("td.co_detailsTable_content");

        private static readonly By MakeCustomPageELibraryCheckboxLocator = By.Id("co_CustomPagesELibrary");

        private static readonly By MakeCustomPageNonBillableCheckboxLocator = By.Id("co_CustomPagesNonBillableZone");

        private static readonly By MakeCustomPageStartPageCheckboxLocator = By.Id("co_CustomPagesStartPage");

        private static readonly By ShareCommitButtonLocator = By.Id("co_CustomPagesShareCommit");

        private static readonly By SharedWithUsersRowsLocator = By.XPath("//tbody[@id='co_shareFolder_collaboratorsAndRoles']//td");

        private static readonly By ZoneIdInputLocator = By.Id("co_CustomPagesDefaultClientId");

        /// <summary>
        /// Share custom page with current settings
        /// </summary>
        /// <returns>CustomPage instance</returns>
        public CustomPage ClickSharePageButton() => this.ClickElement<CustomPage>(ShareCommitButtonLocator);

        /// <summary>
        /// Set sharing options for custom pages, exclude access permissions
        /// </summary>
        /// <param name="options">The options.</param>
        public void SetSharingOptions(CustomPageSharingOptions options)
        {
            if (options.MakeStartPage)
            {
                DriverExtensions.SetCheckbox(options.MakeStartPage, MakeCustomPageStartPageCheckboxLocator);
            }

            if (options.MakeNonBillableZone && !options.MakeELibrary)
            {
                DriverExtensions.SetCheckbox(options.MakeNonBillableZone, MakeCustomPageNonBillableCheckboxLocator);

                IWebElement zoneIdInputIwe = DriverExtensions.WaitForElement(ZoneIdInputLocator);
                zoneIdInputIwe.Clear();
                zoneIdInputIwe.SendKeys(options.ZoneName);
            }

            if (options.MakeELibrary)
            {
                DriverExtensions.SetCheckbox(options.MakeELibrary, MakeCustomPageELibraryCheckboxLocator);
            }
        }

        /// <summary>
        /// The set sharing options.
        /// </summary>
        /// <param name="userInfo">The user Info.</param>
        /// <param name="sharingRole">The sharing Role.</param>
        public void SetSharingOptions(WlnUserInfo userInfo, CustomPageSharingRole sharingRole)
            =>
                DriverExtensions.GetElements(SharedWithUsersRowsLocator)
                                .Where(
                                    el =>
                                        string.Equals(
                                            el.Text,
                                            $"{userInfo.FirstName} {userInfo.LastName}",
                                            StringComparison.InvariantCultureIgnoreCase))
                                .Select(el => DriverExtensions.GetElement(el, By.XPath("..//select")))
                                .First()
                                .SetDropdown(sharingRole.ToString());

        /// <summary>
        /// The verify contact present in shared with.
        /// </summary>
        /// <param name="contact">
        /// The contact full name
        /// </param>
        /// <returns> <see cref="bool"/>.
        /// </returns>
        public bool VerifyContactPresentInSharedWith(string contact)
            => DriverExtensions.GetElements(ContactItemSharedWithLocator).Any(e => e.Text.Contains(contact, StringComparison.InvariantCultureIgnoreCase));

        /// <summary>
        /// This method verify that after selecting E-Library checkbox non billable checkbox is auto selected
        /// and specify text is displayed in ZoneId field
        /// </summary>
        /// <returns> <see cref="bool"/>.
        /// </returns>
        public bool VerifyIsElibrarySetNonBillableZoneWithPresetZoneId()
            => DriverExtensions.WaitForElement(MakeCustomPageNonBillableCheckboxLocator).Selected
                && DriverExtensions.WaitForElement(ZoneIdInputLocator).GetAttribute("value").Contains("ELIBRARY - CLIENT BILLING OFF");
    }
}