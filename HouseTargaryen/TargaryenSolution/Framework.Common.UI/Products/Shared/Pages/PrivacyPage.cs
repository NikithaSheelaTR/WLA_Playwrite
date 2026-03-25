namespace Framework.Common.UI.Products.Shared.Pages
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Privacy Page
    /// </summary>
    public class PrivacyPage : CommonAuthenticatedWestlawNextPage
    {
        // TODO: change locators if page DOM will be changed
        private const string LinkLctMask = "./div[2]/p/a[text() = '{0}']";

        private static readonly By ContainerLocator = By.Id("coid_website_consumerPrivacyPage");
        private static readonly By PageTitlesLocator = By.XPath("./div[1]/h2");
        private static readonly By InfoTextLocator = By.XPath("./div[1]/p");
        private static readonly By SubTitleLocator = By.XPath("./div[2]/h3");
        private static readonly By SubTitleInfoTextLocator = By.XPath("./div[2]/p[1]");
        private static readonly By LinkLocator = By.XPath("./div[2]/p/a");
        private static readonly By TitleUnderButtonLocator = By.XPath("./div[3]/h3");
        private static readonly By InfoTextUnderButtonLocator = By.XPath("./div[3]/p[1]");
        private static readonly By RequestMyDataButtonLocator = By.Id("coid_website_requestDataButton");
        private static readonly By VisitOurPortalInfoTextLocator = By.XPath("./div[4]");
        private static readonly By VisitOurPortalLinkLocator = By.XPath(".//div[4]//a");
        
        /// <summary>
        /// Label Info Text
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel InfoLabel => new Label(ContainerLocator, InfoTextLocator);

        /// <summary>
        ///  new Label Sub Title 
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel SubTitleLabel => new Label(ContainerLocator, SubTitleLocator);

        /// <summary>
        /// Get Sub Title Info Text
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel SubTitleInfoLabel => new Label(ContainerLocator, SubTitleInfoTextLocator);

        /// <summary>
        /// All Links
        /// </summary>
        /// <returns>List of strings</returns>
        public IReadOnlyCollection<ILink> AllLinks => new ElementsCollection<Link>(ContainerLocator, LinkLocator);

        /// <summary>
        /// Label that describes button
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel UnderButtonLabel => new Label(ContainerLocator, TitleUnderButtonLocator);

        /// <summary>
        /// Label that describes button
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel InfoUnderButtonLabel => new Label(ContainerLocator, InfoTextUnderButtonLocator);

        /// <summary>
        /// Get Button Text
        /// </summary>
        /// <returns>The <see cref="IButton"/>.</returns>
        public IButton RequestMyDataButton => new Button(ContainerLocator, RequestMyDataButtonLocator);

        /// <summary>
        /// Visit Our Portal Info Label
        /// </summary>
        /// <returns>
        /// The <see cref="ILabel"/>.
        /// </returns>
        public ILabel VisitOurPortalInfoLabel => new Label(ContainerLocator, VisitOurPortalInfoTextLocator);

        /// <summary>
        /// Visit Our Portal Link
        /// </summary>
        public ILink VisitOurPortalLink => new Link(ContainerLocator, VisitOurPortalLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Label Titles Text
        /// </summary>
        /// <returns>
        /// The IReadOnlyCollection.
        /// </returns>
        public IReadOnlyCollection<ILabel> PageTitlesLabel() => new ElementsCollection<Label>(ContainerLocator, PageTitlesLocator);

        /// <summary>
        /// Click link by name
        /// </summary>
        /// <param name="linkText"> Text of a link. </param>
        /// <typeparam name="T"> Type of object to return.  </typeparam>
        /// <returns> New page instance. </returns>
        public T ClickLinkByName<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(ContainerLocator, By.LinkText(linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}