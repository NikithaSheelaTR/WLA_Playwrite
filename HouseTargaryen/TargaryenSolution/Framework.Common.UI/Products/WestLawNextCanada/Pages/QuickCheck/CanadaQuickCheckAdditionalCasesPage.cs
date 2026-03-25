namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// The Canada Quick Check Additional Cases page.
    /// </summary>
    public class CanadaQuickCheckAdditionalCasesPage : QuickCheckAdditionalCasesPage
    {
        private static readonly By ToolbarLocator = By.ClassName("co_navTools");
        private static readonly By DateResultListLocator = By.XPath("//div[@class='DA-AdditionalCases']//div[contains(@class,'co_searchResults_citation')]/span[4]");
        private static readonly By SpinnerLocator = By.XPath("//div[@class='co_loading']");

        /// <summary>
        /// The toolbar.
        /// </summary>
        public new CanadaQuickCheckToolbar Toolbar => new CanadaQuickCheckToolbar(DriverExtensions.WaitForElement(ToolbarLocator));

        /// <summary>
        /// List of Datelist
        /// </summary>
        public IReadOnlyCollection<ILabel> DateResultList => new ElementsCollection<Label>(DateResultListLocator);

        /// <summary>
        /// Waits for results to show on the page
        /// </summary>
        public void WaitForSpinnerLocatorDisappear() => DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
    }
}