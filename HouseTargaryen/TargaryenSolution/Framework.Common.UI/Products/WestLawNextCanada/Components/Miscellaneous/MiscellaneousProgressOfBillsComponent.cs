namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Miscellaneous
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ProgressOfBills- Document - Summary of Bills - Online Westlaw Canada
    /// </summary>
    public class MiscellaneousProgressOfBillsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath(".//*[@id='co_mainContainer']");
        private static readonly By POBLinksLocator = By.XPath(".//a[@class=' co_tocItemLink ']");
        private static readonly By HeaderLabelLocator = By.XPath(".//*[@class='crsw_fancyTableHeader']//button");
        private static readonly By SummaryCollapseButtonLocator = By.XPath(".//div[@id='crsw_pobSummaryTable_EN']//*[contains(@class,'co_excludeAnnotations')]");
        private static readonly By BillStatusLabelLocator = By.XPath(".//div[*[text()='Currency:']]/following-sibling::div");
        private static readonly By AuthorLabelLocator = By.XPath(".//*[@id='crsw_pobSummaryTable_ENBody']//*[contains(text(),'Source:')]");
        private static readonly By CreateAlertButtonLocator = By.XPath(".//div[@id='co_docToolbarCreateAlertWidget']//button");
        private static readonly By BillTackerAlertButtonLocator = By.XPath(".//a[@title='Create Bill Tracker Entry']");
        private static readonly By BreadcrumbLabelLocator = By.XPath(".//*[@id='breadcrumb']//a");
        private static readonly By BillTackerLabelLocator = By.XPath(".//*[@id='co_alerts']//h1");
        private static readonly By ChunkButtonLocator = By.XPath(".//button[@id='co_topNextChunkButton']");
        private static readonly By ChunkCountLabelLocator = By.XPath(".//*[@id='co_topChunkCountStatus']");
        private static readonly By FooterSectionLocator = By.XPath(".//div[@class='co_footnoteSection']");
        private static readonly By CitationLocator = By.XPath(".//span[@id='citeInfo']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// History ResultList
        /// </summary>
        public IReadOnlyCollection<ILink> POBLinksResultList => new ElementsCollection<Link>(this.ComponentLocator, POBLinksLocator);

        /// <summary>
        /// Header Label
        /// </summary>
        public IReadOnlyCollection<ILabel> HeaderLabelList => new ElementsCollection<Label>(this.ComponentLocator, HeaderLabelLocator);

        /// <summary>
        /// SummaryCollapse Button
        /// </summary>
        public IButton SummaryCollapseButton => new Button(this.ComponentLocator, SummaryCollapseButtonLocator);

        /// <summary>
        /// BillStatus Label
        /// </summary>
        public ILabel BillStatusLabel => new Label(this.ComponentLocator, BillStatusLabelLocator);

        /// <summary>
        /// Author Label
        /// </summary>
        public ILabel AuthorLabel => new Label(this.ComponentLocator, AuthorLabelLocator);

        /// <summary>
        /// CreateAlert Button
        /// </summary>
        public IButton CreateAlertButton => new Button(this.ComponentLocator, CreateAlertButtonLocator);

        /// <summary>
        /// BillTackerAlert Button
        /// </summary>
        public IButton BillTackerAlertButton => new Button(this.ComponentLocator, BillTackerAlertButtonLocator);

        /// <summary>
        /// Breadcrumb Label
        /// </summary>
        public ILabel BreadcrumbLabel => new Label(this.ComponentLocator, BreadcrumbLabelLocator);

        /// <summary>
        /// BillTacker Label
        /// </summary>
        public ILabel BillTackerLabel => new Label(this.ComponentLocator, BillTackerLabelLocator);

        /// <summary>
        /// Chunk Button
        /// </summary>
        public IButton ChunkButton => new Button(this.ComponentLocator, ChunkButtonLocator);

        /// <summary>
        /// ChunkCount Label  
        /// </summary>
        public ILabel ChunkCountLabel => new Label(this.ComponentLocator, ChunkCountLabelLocator);

        /// <summary>
        /// FooterNote   
        /// </summary>
        public ILabel FooterSectionLabel => new Label(this.ComponentLocator, FooterSectionLocator);

        /// <summary>
        /// Navigate To Footnotes 
        /// </summary>
        public void NavigateToFootnotesOfDocument()
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator, ChunkButtonLocator);
            int numberOfChunks = Convert.ToInt32(ChunkCountLabel.Text);
            for (int i = 1; i < numberOfChunks; i++)
            {
                if (FooterSectionLabel.Displayed)
                {
                    continue;
                }
                ChunkButton.Click();
            }
        }

        /// <summary>
        /// Citation Label
        /// </summary>
        public ILabel CitationLabel => new Label(this.ComponentLocator, CitationLocator);
    }
}
