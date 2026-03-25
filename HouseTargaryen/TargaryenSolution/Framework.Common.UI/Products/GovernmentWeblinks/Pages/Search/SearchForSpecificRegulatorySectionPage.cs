namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search
{
    using System.Collections.Generic;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.GovernmentWeblinks.Interfaces;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page for search for a specific regulatory section
    /// </summary>
    public class SearchForSpecificRegulatorySectionPage : BaseWeblinksSearchPage, IWeblinksSearchPage
    {
        private static readonly By LabelLocator = By.XPath(".//preceding-sibling::label");

        private static readonly By TitleFieldLocator = By.XPath("//input[@id='title']");

        private static readonly By PartFieldLocator = By.XPath("//input[@id='part']");

        private static readonly By SectionFieldLocator = By.XPath("//input[@id='section']");

        /// <summary>
        /// Gets list of queries
        /// </summary>
        /// <returns>The list of string. Query</returns>
        public List<string> GetQuery() => new List<string>
                                              {
                                                  this.GetTextFromTextarea(TitleFieldLocator),
                                                  this.GetTextFromTextarea(PartFieldLocator),
                                                  this.GetTextFromTextarea(SectionFieldLocator)
                                              };

        /// <summary>
        /// Search For Specific Regulatory Section
        /// </summary>
        /// <typeparam name="T">The type of a page</typeparam>
        /// <param name="query">The query for search</param>
        /// <returns>The instance of a page</returns>
        public T Search<T>(List<string> query) where T : ICreatablePageObject
        {
            DriverExtensions.SetTextField(query[0], TitleFieldLocator);
            DriverExtensions.SetTextField(query[1], PartFieldLocator);
            DriverExtensions.SetTextField(query[2], SectionFieldLocator);
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Title Label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleFieldLocator, LabelLocator);

        /// <summary>
        /// Part Label
        /// </summary>
        public ILabel PartLabel => new Label(PartFieldLocator, LabelLocator);

        /// <summary>
        /// Section Label
        /// </summary>
        public ILabel SectionLabel => new Label(SectionFieldLocator, LabelLocator);
    }
}
