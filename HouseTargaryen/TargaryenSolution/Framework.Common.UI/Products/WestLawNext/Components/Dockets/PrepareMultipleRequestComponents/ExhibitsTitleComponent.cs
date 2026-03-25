namespace Framework.Common.UI.Products.WestLawNext.Components.Dockets.PrepareMultipleRequestComponents
{
    using System;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Exhibits Title  Row Component.
    /// </summary>
    public class ExhibitsTitleComponent : BaseModuleRegressionComponent
    {
        private const int Timeout = 5000;

        private static readonly By ExpandExhibitsLocator = By.XPath("./td[@class='co_detailsTable_toggleIcon']/a[@class='co_widget_toggleIcon co_widget_expandIcon']");

        private static readonly By CollapseExhibitsLocator = By.XPath(".//td[@class='co_detailsTable_toggleIcon']/a[contains(@class,'co_widget_toggleIcon')] | /a[contains(@class,'co_widget_collapseIcon')]");

        private readonly IWebElement exhibitsTitleContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExhibitsTitleComponent"/> class. 
        /// The constructor.
        /// </summary>
        /// <param name="exhibitsTitleContainer"> Container </param>
        public ExhibitsTitleComponent(IWebElement exhibitsTitleContainer)
        {
            this.exhibitsTitleContainer = exhibitsTitleContainer;
        }

        /// <summary>
        /// Is not implemented
        /// </summary>
        protected override By ComponentLocator
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Is exhibits expanded.
        /// </summary>
        /// <returns>true if expanded</returns>
        public bool IsExhibitsExpanded() => DriverExtensions.IsDisplayed(this.exhibitsTitleContainer, CollapseExhibitsLocator);

        /// <summary>
        /// Expands Exhibits rows.
        /// </summary>
        /// <returns>this component</returns>
        public ExhibitsTitleComponent CollapseExhibits()
        {
            if (DriverExtensions.IsElementPresent(this.exhibitsTitleContainer, CollapseExhibitsLocator, Timeout))
            {
                DriverExtensions.Click(this.exhibitsTitleContainer, CollapseExhibitsLocator);
            }

            return this;
        }

        /// <summary>
        /// Expands Exhibits rows.
        /// </summary>
        /// <returns>this component</returns>
        public ExhibitsTitleComponent ExpandExhibits()
        {
            if (DriverExtensions.IsElementPresent(this.exhibitsTitleContainer, ExpandExhibitsLocator, Timeout))
            {
                DriverExtensions.Click(this.exhibitsTitleContainer, ExpandExhibitsLocator);
            }

            return this;
        }

        /// <summary>
        /// Verify that component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.exhibitsTitleContainer);
    }
}