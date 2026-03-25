namespace Framework.Common.UI.Products.Shared.Pages.Footer
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contact Us Page from Footer
    /// </summary>
    public class ContactUsPage : BaseModuleRegressionPage
    {
        private static readonly By ColumnsSubtitlesLocator = By.XPath("//h2[@class='co_contactUs_subtitle']");

        private static readonly By ResearchHelpDescriptionLocator =
            By.XPath("//h2[text()='Research Help (Reference Attorneys)']/following-sibling::p[1]");

        private static readonly By ResearchHelpInfoBlockLocator =
            By.XPath("//h2[text()='Research Help (Reference Attorneys)']/following-sibling::div[1]");

        /// <summary>
        /// Get Columns Titles
        /// </summary>
        /// <returns>list of titles</returns>
        public List<string> GetColumnsTitles()
            => DriverExtensions.GetElements(ColumnsSubtitlesLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get Research Help Column Description
        /// </summary>
        /// <returns> description text</returns>
        public string GetResearchHelpColumnDescription() => DriverExtensions.GetText(ResearchHelpDescriptionLocator);

        /// <summary>
        /// Get Research Help Column InfoBlock Text
        /// </summary>
        /// <returns>InfoBlock Text without spaces and new lines</returns>
        public string GetResearchHelpColumnInfoBlockText()
            => DriverExtensions.GetElement(ResearchHelpInfoBlockLocator).Text.RemoveMultipleSpaces();
    }
}