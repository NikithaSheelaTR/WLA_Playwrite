namespace Framework.Common.UI.Products.WestlawEdge.Elements.NarrowPanel
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// Narrow Panel Toggle
    /// </summary>
    public class NarrowPanelToggle :  BaseWebElement, IToggle
    {
        private readonly string attribute;
        private readonly string attributeState;

        private readonly By toggleStateLocator = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        /// <param name="currentContainer">Container</param>
        /// <param name="cssToggleStateLocator">Toggle state locator</param>
        /// <param name="cssAttribute">Attribute</param>
        /// <param name="cssAttributeState">Attribute state</param>
        public NarrowPanelToggle(By currentContainer, By cssToggleStateLocator, string cssAttribute = "", string cssAttributeState = "")
            : base(currentContainer)
            => (toggleStateLocator, attribute, attributeState)
            = (cssToggleStateLocator, cssAttribute, cssAttributeState);

        /// <summary>
        /// Toggle State 
        /// </summary>
        public bool State =>
               DriverExtensions.GetElement(GetContainer(), toggleStateLocator).GetCssValue(this.attribute)
                                .Contains(this.attributeState);

        /// <summary>
        /// change toggle State
        /// </summary>
        /// <param name="state"></param>
        public void ToggleState(bool state)
        {
            if (this.State != state)
            {
                this.GetContainer().Click();
                Logger.LogDebug("Change toggle state to " + state);
            }
        }

        /// <summary>
        /// change toggle State
        /// </summary>
        public TPageObject ToggleState<TPageObject>(bool state) where TPageObject : ICreatablePageObject
        {
            this.ToggleState(state);
            return DriverExtensions.CreatePageInstance<TPageObject>();
        }
    }
}
