namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Improved Filter Tooltip Component
    /// </summary>
    public class EdgeImprovedFilterTooltipComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@role='tooltip'][contains(@class,'FilterPanelOnboarding')]");

        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='co_infoBox_closeButton']");

        private static readonly By OkayButtonLocator = By.XPath(".//button[contains(@id,'Onboarding-primaryButton')]");

        private static readonly By DontShowMeThisCheckboxLocator = By.Id("coid_filterPanelOnboardingCheckbox");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Close button 
        /// </summary>	
        public IButton CloseButton { get; } = new Button(ContainerLocator, CloseButtonLocator);

        /// <summary>
        /// Okay button 
        /// </summary>
        public IButton OkayButton { get; } = new Button(ContainerLocator, OkayButtonLocator);

        /// <summary>
        /// "Don't Show Me This" checkbox
        /// </summary>
        public ICheckBox DontShowMeThisCheckbox => new CheckBox(DontShowMeThisCheckboxLocator);

        /// <summary>
        /// Get text from Tooltip
        /// </summary>
        /// <returns>Text</returns>
        public string GetTooltipText() => DriverExtensions.GetText(ComponentLocator);
    }
}