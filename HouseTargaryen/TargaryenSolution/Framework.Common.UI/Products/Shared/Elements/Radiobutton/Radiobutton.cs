namespace Framework.Common.UI.Products.Shared.Elements
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// The Radiobutton
    /// </summary>
    public class Radiobutton : BaseWebElement, IRadiobutton
    {

        /// <inheritdoc />
        public Radiobutton(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Radiobutton(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Radiobutton(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public bool Selected => DriverExtensions.IsRadioButtonSelected(this.GetContainer());

        /// <inheritdoc />
        public void Select()
        {
            string radiobuttonName = this.Text;
            DriverExtensions.GetElement(this.GetContainer()).Click();
            Logger.LogDebug("Select Radiobutton: " + radiobuttonName);
        }
    }
}