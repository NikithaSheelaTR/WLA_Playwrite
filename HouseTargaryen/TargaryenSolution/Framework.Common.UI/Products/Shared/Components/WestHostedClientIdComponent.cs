namespace Framework.Common.UI.Products.Shared.Components
{
    using System;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Component with elements which depend on Account settings in Analytics
    /// </summary>
    public class WestHostedClientIdComponent : BaseModuleRegressionComponent
    {
        private static readonly By MatterIdTextboxLocator = By.Id("co_matterIDTextbox");
        // Practice Area
        private static readonly By SelectPracticeAreaLinkLocator = By.XPath("//a[@id='co_practiceAreaInfoBoxLink']");
        private static readonly By PracticeAreaMessageLocator = By.XPath("//*[@class='co_infoBox_message']");
        private static readonly By ClickHereLinkLocator = By.XPath("//strong[@class='co_practiceAreaLabel']");
        // Reason Code
        private static readonly By NonChargeableCheckboxLocator = By.XPath("//input[@id='co_isNonChargeable']");
        private static readonly By DescriptionOfResearchTextboxLocator = By.XPath("//textarea[@id='co_researchDescriptionTextbox']");
        private static readonly By NonChargeableInfoIconLocator = By.XPath("//span[@id='co_isNonChargeableInfoIcon']");
        private static readonly By ResearchCodeDropdownLocator = By.XPath("//select[@id='co_researchCodesList']");
        private static readonly By HoverMessageLocator = By.XPath("//span[contains(@id,'InfoboxPlaceHolder')]//*[@class='co_infoBox_message']");
        private static readonly By ContainerLocator = By.Id("co_clientIdLightbox");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Enters the matter id in the textbox
        /// </summary>
        /// <param name="matterId">Matter id to enter</param>
        public void EnterMatterId(string matterId) => this.SetIdText(matterId, MatterIdTextboxLocator);

        /// <summary>
        /// Enter text to the Description Of Research textbox
        /// </summary>
        /// <param name="text">The text</param>
        public void EnterDescriptionOfResearchText(string text)
            => DriverExtensions.SetTextField(text, DescriptionOfResearchTextboxLocator);

        /// <summary>
        /// Click on the 'Select Practice Area' link
        /// </summary>
        /// <returns>The <see cref="PracticeAreaPreferencesDialog"/></returns>
        public PracticeAreaPreferencesDialog ClickSelectPracticeAreaLink()
        {
            DriverExtensions.WaitForElement(SelectPracticeAreaLinkLocator).Click();
            return new PracticeAreaPreferencesDialog();
        }

        /// <summary>
        /// Click on the 'Recommend as non-chargeable to client' checkbox
        /// </summary>
        /// <returns>The <see cref="CommonClientIdPage"/></returns>
        public CommonClientIdPage ClickNonChargeableCheckbox()
        {
            DriverExtensions.WaitForElement(NonChargeableCheckboxLocator).Click();
            return new CommonClientIdPage();
        }

        /// <summary>
        /// Gets the text from the Practice Area message
        /// </summary>
        /// <returns>The text from messsage</returns>
        public string GetPracticeAreaMessageText() => this.IsPracticeAreaMessageDisplayed()
                                                          ? DriverExtensions.GetText(PracticeAreaMessageLocator)
                                                          : string.Empty;

        /// <summary>
        /// Gets hover over folder message text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHoverMessageText()
        {
            IWebElement elementToClick = DriverExtensions.GetElement(NonChargeableInfoIconLocator);

            DriverExtensions.GetElement(elementToClick).SeleniumHover();
            return DriverExtensions.WaitForElement(HoverMessageLocator).Text;
        }

        /// <summary>
        /// Select random value for Practice Area
        /// </summary>
        public void SelectResearchCodesByRandomIndex()
        {
            var practiceAreaDropdown = new SelectElement(DriverExtensions.WaitForElement(ResearchCodeDropdownLocator));
            practiceAreaDropdown.SelectByIndex(new Random().Next(practiceAreaDropdown.Options.Count));
        }

        /// <summary>
        /// Set Non Chargeable checkbox
        /// </summary>
        /// <param name="isSet">True  -to check, false to uncheck</param>
        public void SetNonChargeableCheckbox(bool isSet = true) => DriverExtensions.SetCheckbox(NonChargeableCheckboxLocator, isSet);

        #region IsDisplayed 
        /// <summary>
        ///  Checks if the "Select practice area" link is diplayed on the page
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsPracticeAreaLinkDisplayed() => DriverExtensions.IsDisplayed(SelectPracticeAreaLinkLocator);

        /// <summary>
        ///  Checks if the "Your firm has requested that you enter a Practice Area." link is diplayed on the page
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsPracticeAreaMessageDisplayed() => DriverExtensions.IsDisplayed(PracticeAreaMessageLocator);

        /// <summary>
        /// Determines is 'Click here to set 'practice area" link displayed
        /// </summary>
        /// <returns>True if opened, false otherwise</returns>
        public bool IsClickHereLinkDisplayed() => DriverExtensions.IsDisplayed(ClickHereLinkLocator);

        /// <summary>
        /// Verify that Non Chargeable checkbox is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsNonChargeableCheckboxDisplayed() => DriverExtensions.IsDisplayed(NonChargeableCheckboxLocator, 5);

        /// <summary>
        /// Verify that Description Of Research textbox is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDescriptionOfResearchTextboxDisplayed() => DriverExtensions.IsDisplayed(DescriptionOfResearchTextboxLocator, 5);

        /// <summary>
        /// Verify that ResearchCodesListDropdown is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsResearchCodeDropdownDisplayed() => DriverExtensions.IsDisplayed(ResearchCodeDropdownLocator, 5);
        #endregion IsDisplayed

        /// <summary>
        /// The set id text.
        /// </summary>
        /// <param name="idValue">The id value</param>
        /// <param name="idLocator">The id locator</param>
        private void SetIdText(string idValue, By idLocator)
        {
            // need this wait for certain pages that have an in-between load before the ID element, such as WLN Patron
            IWebElement textbox = DriverExtensions.WaitForElement(idLocator);

            if (textbox.GetAttribute("value") != idValue.ToUpper())
            {
                textbox.SetTextField(idValue);
            }
        }
    }
}
