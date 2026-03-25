namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Effective Date component.
    /// </summary>
    public class EffectiveDateComponent : BaseModuleRegressionComponent
    {
        private static readonly By EffectiveDateInputLocator = By.CssSelector("#coid_TOCVersionWidget_query");
        private static readonly By ExplanatoryLanguageForEffectiveDateLabelLocator = By.CssSelector("#coid_TOCVersionWidget_ExplanatoryText");
        private static readonly By ForEffectiveDateGoButtonLocator = By.CssSelector("#co_TOCVersion_applyButton");
        private static readonly By ForEffectiveDateResetButtonLocator = By.CssSelector("#co_TOCVersion_resetButton");
        private static readonly By ContainerLocator = By.Id("coid_browseVersionWidgetContainer");

        /// <summary>
        /// Versioning Advisory label
        /// </summary>
        public ILabel VersioningAdvisoryLabel => new Label(ComponentLocator, ExplanatoryLanguageForEffectiveDateLabelLocator);

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new Button(ComponentLocator, ForEffectiveDateGoButtonLocator);

        /// <summary>
        /// Reset button
        /// </summary>
        public IButton ResetButton => new Button(ComponentLocator, ForEffectiveDateResetButtonLocator);

        /// <summary>
        /// Explanatory language
        /// </summary>
        /// <returns> Explanatory language for Effective Date component </returns>
        public string ExplanatoryLanguage => DriverExtensions.GetText(ExplanatoryLanguageForEffectiveDateLabelLocator);

        /// <summary>
        /// Effective Date text
        /// </summary>
        /// <returns> Displayed effective date for Effective Date component </returns>
        public string EffectiveDateText => DriverExtensions.GetText(EffectiveDateInputLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Change effective date
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="effectiveDate"> Date to set </param>
        /// <returns> New instance of the page </returns>
        public T SetNewEffectiveDate<T>(string effectiveDate) where T : ICreatablePageObject
        {
            IWebElement effectiveInputElement = DriverExtensions.WaitForElement(EffectiveDateInputLocator);

            // ClearUsingButtons() should be replaced by Clear() method when Clear() will be fixed
            effectiveInputElement.ClearUsingButtons();
            effectiveInputElement.SendKeys(effectiveDate);
            DriverExtensions.Click(ForEffectiveDateGoButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enter date value in Effective Date field
        /// </summary>
        /// <param name="effectiveDate"> Date to set </param>
        public void EnterEffectiveDate(string effectiveDate)
        {
            IWebElement effectiveInputElement = DriverExtensions.WaitForElement(EffectiveDateInputLocator);
            effectiveInputElement.ClearUsingButtons();
            effectiveInputElement.SendKeys(effectiveDate);
        }
    }
}