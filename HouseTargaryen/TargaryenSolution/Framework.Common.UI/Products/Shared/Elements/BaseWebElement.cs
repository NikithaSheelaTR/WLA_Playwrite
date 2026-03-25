namespace Framework.Common.UI.Products.Shared.Elements
{
    using Framework.Common.UI.Constants;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The base web element abstraction.
    /// </summary>
    public abstract class BaseWebElement : BaseContainerWrapper, IBaseWebElement
    {
        /// <inheritdoc />
        protected BaseWebElement(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        protected BaseWebElement(IWebElement outerContainer, params By[] locatorBys)
            : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        protected BaseWebElement(IWebElement currentContainer)
            : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public virtual bool Displayed => this.GetContainer(WebDriverTimeouts.ElementDisplayed)?.Displayed ?? false;

        /// <inheritdoc />
        public virtual bool Enabled => this.GetContainer()?.Enabled ?? false;

        /// <inheritdoc />
        public virtual bool Present => this.GetContainer(WebDriverTimeouts.ElementPresent) != null;

        /// <inheritdoc />
        public virtual string Text => this.GetContainer()?.GetText() ?? string.Empty;

        /// <inheritdoc />
        public virtual bool IsInView => this.GetContainer()?.IsElementInView() ?? false;

        /// <inheritdoc />
        public virtual string GetAttribute(string attribute) =>
            this.GetContainer()?.GetAttribute(attribute) ?? string.Empty;

        /// <inheritdoc />
        public virtual string GetCssValue(string propertyName) =>
            this.GetContainer()?.GetCssValue(propertyName) ?? string.Empty;

        /// <inheritdoc />
        public virtual void SendKeys(string text)
        {
            this.GetContainer().SendKeys(text);
            Logger.LogDebug("Send keys: " + text);
        }

        /// <inheritdoc />
        public virtual void Hover()
        {
            string name = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().SeleniumHover();
            Logger.LogDebug("Hover performed on element: " + name);
        }

        /// <inheritdoc />
        public virtual void HoverOut()
        {
            string name = this.GetElementName(this.GetContainer().Text); 
            this.GetContainer().HoverOut();
            Logger.LogDebug("Hover out performed on element: " + name);
        }
        
        /// <inheritdoc />
        public virtual void ScrollToElement()
        {
            string name = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().ScrollToElementCenter();
            Logger.LogDebug("Scroll to element: " + name);
        }

        /// <inheritdoc />
        public virtual void WaitDisplayed(int milliseconds = WebDriverTimeouts.ElementWaitForDisplayed) =>
            this.GetContainer().WaitForElementDisplayed(milliseconds);

        /// <inheritdoc />
        public virtual void WaitNotDisplayed() => this.GetContainer().WaitForElementNotDisplayed();

        /// <inheritdoc />
        public virtual void WaitEnabled(int milliseconds = WebDriverTimeouts.ElementWaitForDisplayed) =>
            this.GetContainer().WaitForElementEnabled(milliseconds);

        /// <summary>
        /// Get parent element
        /// </summary>
        /// <returns> parent element</returns>
        protected IWebElement GetParentElement() => this.GetContainer().GetParentElement();

        /// <summary>
        /// Get element name
        /// </summary>
        /// <param name="elementName"> initial element name </param>
        /// <returns> The <see cref="string"/>.element name </returns>
        protected string GetElementName(string elementName) => elementName.Equals("") ? TruncateName(this.GetParentElementName()) : TruncateName(elementName);

        private string GetParentElementName()
        {
            IWebElement parentElement = this.GetParentElement();
            while (parentElement.Text.Equals(""))
            {
                parentElement = parentElement.GetParentElement();
            }

            return parentElement.Text;
        }

        private string TruncateName(string name) => name.Length > 50 ? name.Substring(0, 50) + "..." : name;
    }
}