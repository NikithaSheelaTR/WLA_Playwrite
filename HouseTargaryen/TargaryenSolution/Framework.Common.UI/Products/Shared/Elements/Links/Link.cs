namespace Framework.Common.UI.Products.Shared.Elements.Links
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// The link.
    /// </summary>
    public class Link : BaseWebElement, ILink
    {
        /// <inheritdoc />
        public Link(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Link(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Link(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Link"/> class.
        /// </summary>
        /// <param name="linkText">
        /// The link text.
        /// </param>
        public Link(string linkText)
        {
            this.CurrentElementLocator = new ByChained(By.LinkText(linkText));
        }

        /// <inheritdoc />
        public virtual string LinkUrl => this.GetAttribute("href");

        /// <inheritdoc />
        public virtual void Click()
        {
            string linkName = this.GetElementName(this.GetContainer().Text);
            this.GetContainer().CustomClick();
            Logger.LogDebug("Click on link: " + linkName);
        }

        /// <inheritdoc />
        public TWebObject Click<TWebObject>()
            where TWebObject : ICreatablePageObject
        {
            this.Click();
            DriverExtensions.WaitForAnimation();                                                                                                                                                                            
            return DriverExtensions.CreatePageInstance<TWebObject>();
        }
    }
}