namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Selected topics tab component
    /// </summary>
    public class SelectedTopicsTabComponent : BaseTabComponent
    {
        private const string RelatedTopicLctMask = "//ul[@class='Related-topics-wrapper']//following::li/a[text()='{0}']";

        private static readonly By ContainerLocator = By.Id("panel_topicsTabId");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Selected topics";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks related topic by its name
        /// </summary>
        /// <typeparam name="T">T </typeparam>
        /// <param name="topicName"> topic name  </param>
        /// <returns> The T page  </returns>
        public T ClickRelatedTopicByText<T>(string topicName)
            where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(string.Format(RelatedTopicLctMask, topicName)));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Selected Topics Links Text
        /// </summary>
        /// <returns>links text</returns>
        public string GetSelectedTopicsLinksText() => DriverExtensions.GetElement(this.ComponentLocator).Text;
    }
}
