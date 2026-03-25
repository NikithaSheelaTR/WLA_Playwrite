namespace Framework.Common.UI.Products.WestLawNext.Items
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    ///  The question-answer result list item class 
    /// </summary>
    public class QuestionAnswerResultListItem : BaseItem
    {
        private static readonly By DocumentTitleLocator = By.XPath(".//h3/a");
        private static readonly By AssociatedContentHideButtonLocator = By.XPath(".//a[@class='co_statutoryCitation_toggleButton']/span[text()='Hide associated content']");
        private static readonly By AssociatedContentShowButtonLocator = By.XPath(".//a[@class='co_statutoryCitation_toggleButton']/span[text()='Show associated content']");
        private static readonly By AssociatedContentContainerLocator = By.ClassName("co_statutoryCitation_content");
        private static readonly By PreviouslyViewedIconLocator = By.XPath(".//li[@class='co_document_icon_previouslyviewed']");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionAnswerResultListItem"/> class. 
        /// Question-Answer result list constructor
        /// </summary>
        /// <param name="container">The container.</param>
        public QuestionAnswerResultListItem(IWebElement container)
            : base(container)
        {
        }

        #region Public properties

        /// <summary>
        /// The get item title text
        /// </summary>
        /// <returns></returns>
        public string TitleText => DriverExtensions.GetElement(this.Container, DocumentTitleLocator).Text;
        
        #endregion
    
        #region Displaying

        /// <summary>
        /// Verify that associated content container is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAssociatedContentDisplayed() => DriverExtensions.IsDisplayed(this.Container, AssociatedContentContainerLocator);

        /// <summary>
        /// Verify that associated PUI icon is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPreviouslyViewedIconDisplayed() => DriverExtensions.IsDisplayed(this.Container, PreviouslyViewedIconLocator);

        /// <summary>
        /// Checks if previously opened document is scrolled into view
        /// </summary>
        /// <returns>True if the document is scrolled into view, false otherwise</returns>
        public bool IsInView()
            => DriverExtensions.GetElement(this.Container, DocumentTitleLocator).IsElementInView();

        /// <summary>
        /// Verify that associated content container is collapsed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAssociatedContentCollapsed() => DriverExtensions.IsDisplayed(this.Container, AssociatedContentShowButtonLocator);

        /// <summary>
        /// Verify that associated content container is expanded
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAssociatedContentExpanded() => DriverExtensions.IsDisplayed(this.Container, AssociatedContentHideButtonLocator);
        #endregion
        #region Clicks
        /// <summary>
        /// Click 'Hide associated content' button
        /// </summary>
        public void ClickHideAssociatedContent() =>
            DriverExtensions.WaitForElement(this.Container, AssociatedContentHideButtonLocator).Click();

        /// <summary>
        /// Click 'Show associated content' button
        /// </summary>
        public void ClickShowAssociatedContent() =>
            DriverExtensions.WaitForElement(this.Container, AssociatedContentShowButtonLocator).Click();

        /// <summary>
        /// The click title link.
        /// </summary>
        /// <typeparam name="T">
        /// The ICreatable PO
        /// </typeparam>
        /// <returns>
        /// The new item of T class
        /// </returns>
        public T ClickTitleLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(this.Container, DocumentTitleLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
        #endregion
    }
}
