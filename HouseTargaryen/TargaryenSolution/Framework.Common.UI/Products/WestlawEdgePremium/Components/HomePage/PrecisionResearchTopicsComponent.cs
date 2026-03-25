namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using System.Collections.Generic;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;

    /// <summary>
    /// Precision Research Topics component
    /// </summary>
    public class PrecisionResearchTopicsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='Athens-HomepageScopeModal-tabsContainer']");
        private static readonly By ComingSoonToggleLocator = By.XPath(".//span[text()='Coming soon']//ancestor::button[contains(@class,'Athens-HomepageScopeModal-tabsContainerHeading')]//span/following-sibling::span");        
        private static readonly By AvailableTopicsButtonsLocator = By.XPath(".//span[text()='Available topics']//ancestor:: div//ul[@class='Athens-HomepageScopeModal-tabsContainerSection']//li");
        private static readonly By ComingSoonButtonsLocator = By.XPath(".//span[text()='Coming soon']//ancestor::button//following-sibling::ul/li/button");

        /// <summary>
        /// Coming soon toggle
        /// </summary>
        public IToggle ComingSoonToggle => new Toggle(DriverExtensions.GetElement(this.ComponentLocator), ComingSoonToggleLocator, "class", "upCaret");

        /// <summary>
        /// Buttons under 'Available topics'
        /// </summary>
        public IReadOnlyCollection<IButton> AvailableTopicsButtonsList => new ElementsCollection<Button>(this.ComponentLocator, AvailableTopicsButtonsLocator);

        /// <summary>
        /// Buttons under 'Coming soon'
        /// </summary>
        public IReadOnlyCollection<IButton> ComingSoonButtonsList => new ElementsCollection<Button>(this.ComponentLocator, ComingSoonButtonsLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
