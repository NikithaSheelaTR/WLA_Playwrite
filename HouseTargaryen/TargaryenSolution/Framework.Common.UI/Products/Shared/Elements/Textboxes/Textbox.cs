namespace Framework.Common.UI.Products.Shared.Elements.Textboxes
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// The standard textbox implementation. Inherit from it if you need a custom implementation.
    /// </summary>
    public class Textbox : BaseWebElement, ITextbox
    {
        /// <inheritdoc />
        public Textbox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Textbox(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Textbox(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// The current textbox name.
        /// </summary>
        private string TextboxName => this.GetElementName(GetContainer().Text);

        /// <inheritdoc />
        public virtual void SetText(string textToSet)
        {
            this.GetContainer().SetTextField(textToSet);
            Logger.LogDebug("Enter '" + textToSet + "' to  textbox " + TextboxName);
        }

        /// <inheritdoc />
        public virtual T SetText<T>(string textToSet) where T : ICreatablePageObject
        {
            this.SetText(textToSet);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <inheritdoc />
        public virtual void SendKeysSlow(string textToSet)
        {
            this.GetContainer().SendKeysSlow(textToSet);
            Logger.LogDebug("Enter '" + textToSet + "' to  textbox " + TextboxName);
        }

        /// <inheritdoc />
        public virtual T SendKeysSlow<T>(string textToSet) where T : ICreatablePageObject
        {
            this.GetContainer().SendKeysSlow(textToSet);
            Logger.LogDebug("Enter '" + textToSet + "' to  textbox " + TextboxName);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <inheritdoc />
        public virtual T Click<T>() where T : ICreatablePageObject
        {
            this.GetContainer().Click();
            Logger.LogDebug("Click textbox: " + TextboxName);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <inheritdoc />
        public virtual void Clear()
        {
            this.GetContainer().Clear();
            Logger.LogDebug("Clear textbox: " + TextboxName);
        }

        /// <inheritdoc />
        public virtual T Clear<T>() where T : ICreatablePageObject
        {
            this.GetContainer().Clear();
            Logger.LogDebug("Clear textbox: " + TextboxName);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}