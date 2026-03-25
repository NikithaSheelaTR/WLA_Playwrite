namespace Framework.Common.UI.Products.Shared.Elements.Toggles
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The Toggle.
    /// </summary>
    public class Toggle : BaseWebElement, IToggle
    {
        private readonly string attribute;
        private readonly string attributeState;

        private readonly By toggleStateLocator = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        /// <param name="currentContainer">Container</param>
        /// <param name="toggleLocator">Toggle locator</param>
        /// <param name="toggleStateLocator">Toggle state locator</param>
        /// <param name="attribute">Attribute</param>
        /// <param name="attributeState">Attribute state</param>
        public Toggle( IWebElement currentContainer, By toggleLocator, By toggleStateLocator, string attribute = "", string attributeState = "")
            : base(currentContainer, toggleLocator)
        {
            this.toggleStateLocator = toggleStateLocator;
            this.attribute = attribute;
            this.attributeState = attributeState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        /// <param name="currentContainer">Container</param>
        /// <param name="toggleLocator">Toggle locator</param>
        /// <param name="attribute">Attribute</param>
        /// <param name="attributeState">Attribute state</param>
        public Toggle(IWebElement currentContainer, By toggleLocator, string attribute = "", string attributeState = "")
            : base(currentContainer, toggleLocator)
        {
            this.attribute = attribute;
            this.attributeState = attributeState;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Toggle"/> class.
        /// </summary>
        /// <param name="currentContainer">Container</param>
        /// /// <param name="toggleStateLocator">Toggle state locator</param>
        /// <param name="attribute">Attribute</param>
        /// <param name="attributeState">Attribute state</param>
        public Toggle(By currentContainer, By toggleStateLocator, string attribute = "", string attributeState = "")
            : base(currentContainer)
        {
            this.toggleStateLocator = toggleStateLocator;
            this.attribute = attribute;
            this.attributeState = attributeState;
        }

        /// <inheritdoc />
        public virtual bool State =>
            this.toggleStateLocator == null
                ? DriverExtensions.GetElement(this.GetContainer()).GetAttribute(this.attribute)
                                  ?.Contains(this.attributeState) ?? false
                : DriverExtensions.GetElement(this.GetContainer(), this.toggleStateLocator).GetAttribute(this.attribute)
                                  ?.Contains(this.attributeState) ?? false;

        /// <summary>
        /// Gets a value indicating whether or not this element is enabled.
        /// </summary>
        public override bool Enabled =>
            this.toggleStateLocator == null
                ? base.Enabled
                : DriverExtensions.GetElement(this.GetContainer(), this.toggleStateLocator)?.Enabled ?? false;

        /// <inheritdoc />
        public virtual void ToggleState(bool state)
        {
            if (this.State != state)
            {
                this.GetContainer().ScrollToElementCenter();
                DriverExtensions.WaitForPageLoad();
                this.GetContainer().Click();
                Logger.LogDebug("Change toggle state to " + state);
            }
        }

        /// <inheritdoc />
        public virtual TPageObject ToggleState<TPageObject>(bool state) where TPageObject : ICreatablePageObject
        {
            this.ToggleState(state);
            return DriverExtensions.CreatePageInstance<TPageObject>();
        }
    }
}
