namespace Framework.Common.UI.Products.Shared.Elements.Image
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <summary>
    /// The standard image.
    /// </summary>
    public class Image : BaseWebElement, IImage
    {
        /// <inheritdoc />
        public Image(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Image(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Image(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public virtual string Source => this.GetAttribute("src");

        /// <inheritdoc />
        public virtual void Click()
        {
            string imageName = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().CustomClick();
            Logger.LogDebug("Click image: " + imageName);
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
