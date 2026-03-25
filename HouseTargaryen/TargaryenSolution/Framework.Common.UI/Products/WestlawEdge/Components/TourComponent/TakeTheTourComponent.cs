namespace Framework.Common.UI.Products.WestlawEdge.Components.TourComponent
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Tours;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Take the tour component with options take the tour, may be later, dont show me this 
    /// </summary>
    public class TakeTheTourComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.ClassName("co_overlayBox_container");

        private static readonly By CloseIconLocator = By.XPath(".//*[@class='co_overlayBox_closeButton co_iconBtn']");

        private static readonly By DontShowMeThisButtonLocator = By.Id("coid_doNotShowButton");

        /// <summary>
        /// Don't show me this button
        /// </summary>
        public IButton DontShowMeThisButton { get; } = new Button(DontShowMeThisButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the 'Take the tour' enumeration
        /// </summary>
        private EnumPropertyMapper<TakeTheTourOption, WebElementInfo> TakeTheTourMap =>
             EnumPropertyModelCache.GetMap<TakeTheTourOption, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Tours");

        /// <summary>
        /// Click on tour options
        /// </summary>
        /// <param name="option">Take the tour option</param>
        public void ClickOnTourOption(TakeTheTourOption option)
        {
            DriverExtensions.WaitForElement(By.XPath(this.TakeTheTourMap[option].LocatorString)).Click();
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// Close 'Take the tour' component
        /// </summary>
        public void ClickCloseButton() => DriverExtensions.Click(this.ComponentLocator,CloseIconLocator);
    }
}
