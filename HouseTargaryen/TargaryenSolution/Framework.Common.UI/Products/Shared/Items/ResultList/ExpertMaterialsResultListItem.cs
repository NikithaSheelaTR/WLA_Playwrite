namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The testimonial history result list item.
    /// todo: make this class internal when Search Manager is implemented
    /// </summary>
    public sealed class ExpertMaterialsResultListItem : ResultListItem
    {
        private static readonly By PublicationsLocator = By.XPath(".//a[contains(text(),'Publications')]");

        private static readonly By TestimonialHistoryLocator = By.XPath(".//a[contains(text(),'Testimonial History')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpertMaterialsResultListItem"/> class.
        /// </summary>
        /// <param name="containerElement">
        /// The container element.
        /// </param>
        public ExpertMaterialsResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// checks if a Publications Link item is displayed for an item
        /// </summary>
        /// <returns>
        /// true if the result has the link
        /// </returns>
        public bool IsPublicationsLinkDisplayed() => DriverExtensions.IsDisplayed(this.Container, PublicationsLocator);

        /// <summary>
        /// checks if a Testimonial Link item is displayed for an item
        /// </summary>
        /// <returns>
        /// true if the result has the link
        /// </returns>
        public bool IsTestimonialHistoryLinkDisplayed() => DriverExtensions.IsDisplayed(this.Container, TestimonialHistoryLocator);

        /// <summary>
        /// Click Publications link
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickPublicationsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, PublicationsLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Testimonial link
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam> 
        /// <returns> New instance of the page </returns> 
        public T ClickTestimonialHistoryLink<T>()
            where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.Container, TestimonialHistoryLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}