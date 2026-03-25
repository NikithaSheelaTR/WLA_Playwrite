namespace Framework.Common.UI.Products.Shared.Elements.Checkboxes
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The standard check box.
    /// </summary>
    public class CheckBox : BaseWebElement, ICheckBox
    {
        /// <inheritdoc />
        public CheckBox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public CheckBox(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public CheckBox(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public virtual bool Selected => DriverExtensions.IsCheckboxSelected(this.GetContainer());

        /// <inheritdoc />
        public virtual void Set(bool value)
        {
            string checkboxName = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().SetCheckbox(value);
            Logger.LogDebug("Set checkbox '" + checkboxName + "' to " + value);
        }

        /// <inheritdoc />
        public TWebObject Set<TWebObject>(bool value)
            where TWebObject : ICreatablePageObject
        {
            this.Set(value);
            return DriverExtensions.CreatePageInstance<TWebObject>();
        }
    }
}