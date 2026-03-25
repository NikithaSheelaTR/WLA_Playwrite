namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNextMobile.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Related Info page
    /// </summary>
    public class BaseRelatedInfoPage : MobileBasePage
    {
        private static readonly By EmailListLinkLocator = By.Id("coid_website_emailDocumentLink");

        /// <summary>
        /// The component for the Related Info section at the bottom of the page
        /// </summary>
        public RelatedInfoSectionComponent RiSection { get; } = new RelatedInfoSectionComponent();

        /// <summary>
        /// Click on the Email List link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickEmailListLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EmailListLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
