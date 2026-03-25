namespace Framework.Common.UI.Products.WestlawEdge.Pages.Judicial
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial omitted by both page.
    /// </summary>
    public class JudicialOmittedByBothPage : QuickCheckBasePage
    {
        private static readonly By PageTitleLocator = By.ClassName("DA-OmittedByBothHeader");

        private static readonly By ToolbarLocator = By.XPath("//div[@class='DA-HeaderToolbar']");

        private static readonly By ResultItemLocator = By.XPath(".//li[@class='DA-OmittedByBothCase']");

        /// <summary>
        /// Gets the page title.
        /// </summary>
        public ILabel PageTitle { get; } = new Label(PageTitleLocator);

        /// <summary>
        /// Gets the toolbar.
        /// </summary>
        public QuickCheckToolbar Toolbar => new QuickCheckToolbar(DriverExtensions.WaitForElement(ToolbarLocator));

        /// <summary>
        /// Gets a list of all cases.
        /// </summary>
        public List<OmittedByBothItem> AllCases =>
            DriverExtensions.GetElements(ResultItemLocator).Select(elem => new OmittedByBothItem(elem)).ToList();

        /// <summary>
        /// Gets a list of all headings from each case.
        /// </summary>
        public List<OmittedByBothHeadingComponent> AllHeadings => this.AllCases.SelectMany(elem => elem.Headings).ToList();
    }
}
