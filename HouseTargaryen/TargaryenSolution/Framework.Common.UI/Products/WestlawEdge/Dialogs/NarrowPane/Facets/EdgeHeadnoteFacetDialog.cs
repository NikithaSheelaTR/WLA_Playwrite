namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets.NarrowComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Models.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Headnote Topics Facet Dialog
    /// </summary>
    public class EdgeHeadnoteFacetDialog : BaseAvailableAndSelectedOptionsListsDialog
    {
        private const string TopicItemLctMask = "//section[contains(@id,'{0}')]/div[@class='HeadnoteTopicContent']";

        private const string HeadnoteTopicsItemLctMask = "//ul[@class='Tab-list']/li[contains(text(),'{0}')]";

        private static readonly By HeadnoteTopicsItemLocator = By.XPath("//ul[@class='Tab-list']/li");

        private static readonly By SelectAllCheckBoxLabelLocator = By.XPath("//div[@class='HeadnoteMeta Meta']//label");

        private static readonly By ContainerLocator = By.Id("co_facet_WestHeadnoteTopics_popup");

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElementDisplayed(ContainerLocator);

        /// <summary>
        /// Verify that there is no headnote topics with zero count
        /// </summary>
        /// <returns>False - if there is no items with zero count, true - otherwise</returns>
        public List<int> GetHeadnoteTopicsCounts()
            => DriverExtensions.GetElements(HeadnoteTopicsItemLocator).Select(t => StringExtensions.ConvertCountToInt(t.Text)).ToList();

        /// <summary>
        /// Get text from Select all checkbox
        /// </summary>
        /// <returns>Text</returns>
        public string GetSelectAllCheckboxText() => DriverExtensions.GetText(SelectAllCheckBoxLabelLocator);

        /// <summary>
        /// Get list of headnotes for selected topic
        /// </summary>
        /// <param name="topic">The topic</param>
        /// <returns>List of topics</returns>
        public List<EdgeHeadnoteTopicsModel> GetHeadnotesForSelectedTopic(string topic) =>
            this.GetHeadnotesForSelectedTopicItems(topic).Select(item => item.ToModel<EdgeHeadnoteTopicsModel>())
                .ToList();

        /// <summary>
        /// Select Topic
        /// </summary>
        /// <param name="topic">Topic name</param>
        public void SelectTopic(string topic) => this.ClickElement(By.XPath(string.Format(HeadnoteTopicsItemLctMask, topic)));

        private List<EdgeHeadnoteTopicsDialogItem> GetHeadnotesForSelectedTopicItems(string topic)
        {
            this.SelectTopic(topic);
            return DriverExtensions.GetElements(By.XPath(string.Format(TopicItemLctMask, this.GetTopicId(topic))))
                                   .Select(i => new EdgeHeadnoteTopicsDialogItem(i)).ToList();
        }

        /// <summary>
        /// Get topic id.
        /// </summary>
        /// <param name="topic">The topic</param>
        /// <returns>Id</returns>
        private int GetTopicId(string topic) => int.Parse(new string(DriverExtensions
                                    .GetElement(By.XPath(string.Format(HeadnoteTopicsItemLctMask, topic)))
                                    .GetAttribute("id")
                                    .Where(char.IsDigit)
                                    .ToArray()));
    }
}