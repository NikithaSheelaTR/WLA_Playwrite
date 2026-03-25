namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// NegativeTreatmentInfoComponent
    /// Information on the document page near yellow or red flags
    /// </summary>
    public class ReadingModeKeyCiteComponent : BaseModuleRegressionComponent
    {
        private EnumPropertyMapper<KeyCiteComponent, WebElementInfo> keyCiteLocators;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadingModeKeyCiteComponent"/> class. 
        /// </summary>
        /// <param name="additionalInfo"> Additional Info </param>
        public ReadingModeKeyCiteComponent(string additionalInfo = "")
        {
            this.keyCiteLocators = EnumPropertyModelCache.GetMap<KeyCiteComponent, WebElementInfo>(additionalInfo);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(this.keyCiteLocators[KeyCiteComponent.InfoContainer].LocatorString);

        /// <summary>
        /// Get Static Phrase By Index
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> Static Phrase </returns>
        public string GetStaticPhraseByIndex(int index = 0)
            => DriverExtensions.WaitForElement(By.Id(string.Format(this.keyCiteLocators[KeyCiteComponent.StaticPhrase].LocatorMask, index))).GetText();

        /// <summary>
        /// Text from Negative Treatment
        /// </summary>
        /// <returns> Negative treatment text </returns>
        public string GetNegativeTreatmentText() => DriverExtensions.WaitForElement(this.ComponentLocator).GetText();

        /// <summary>
        /// Verify that SNT flag is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSntFlagDisplayed() => DriverExtensions.WaitForElement(By.XPath(this.keyCiteLocators[KeyCiteComponent.Flag].LocatorString)).Displayed;

        /// <summary>
        /// Checks whether the citator flag is displayed.
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCitatorFlagDisplayed() => DriverExtensions.WaitForElement(By.XPath(this.keyCiteLocators[KeyCiteComponent.CitatorFlag].LocatorString)).Displayed;

        /// <summary>
        /// Click on flag
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickOnCitatorFlag<T>() where T : TabPage
        {
            DriverExtensions.WaitForElement(By.XPath(this.keyCiteLocators[KeyCiteComponent.CitatorFlag].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Date
        /// </summary>
        /// <returns> Date if exists, empty string otherwise </returns>
        public virtual string GetDate() => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.keyCiteLocators[KeyCiteComponent.Date].LocatorString)).Text;

        /// <summary>
        /// Get Court
        /// </summary>
        /// <returns> Court if exists, empty string otherwise </returns>
        public virtual string GetCourt() => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.keyCiteLocators[KeyCiteComponent.Court].LocatorString)).Text;

        /// <summary>
        /// Click Negative treatment Link
        /// </summary>
        public virtual void ClickNegativeTreatmentLink()
        {
            DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.keyCiteLocators[KeyCiteComponent.NegativeTreatmentLink].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get Negative treatment Link text
        /// </summary>
        /// <returns> Negative treatment Link </returns>
        public string GetNegativeTreatmentLinkText()
            => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.keyCiteLocators[KeyCiteComponent.NegativeTreatmentLink].LocatorString)).Text;

        /// <summary>
        /// Get Phrase
        /// </summary>
        /// <returns> Phrase </returns>
        public string GetPhrase() => DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.keyCiteLocators[KeyCiteComponent.Phrase].LocatorString)).Text;
    }
}
