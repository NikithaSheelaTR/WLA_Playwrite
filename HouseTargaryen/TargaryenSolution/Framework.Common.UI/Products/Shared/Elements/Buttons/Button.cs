namespace Framework.Common.UI.Products.Shared.Elements.Buttons
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The standard button.
    /// </summary>
    public class Button : BaseWebElement, IButton
    {
        /// <inheritdoc />
        public Button(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Button(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Button(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public virtual void Click()
        {
            string buttonName = this.Text;
            this.GetContainer().Click();
            Logger.LogDebug("Click button: " + buttonName);
        }

        /// <inheritdoc />
        public TWebObject Click<TWebObject>()
            where TWebObject : ICreatablePageObject
        {
            this.Click();
            return DriverExtensions.CreatePageInstance<TWebObject>();
        }
    }
}