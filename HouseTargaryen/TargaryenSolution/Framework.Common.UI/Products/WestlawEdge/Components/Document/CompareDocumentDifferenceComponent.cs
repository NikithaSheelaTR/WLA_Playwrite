namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.StatutesCompare;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Documents compare next/prev difference component
    /// </summary>
    public class CompareDocumentDifferenceComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class = 'co_statutesCompare_navigation']");

        private static readonly By NextDifferenceLocator = By.XPath("//button[@class = 'co_next co_tbButton' and @oldtitle='Jump to Next Difference']");

        private static readonly By PrevDifferenceLocator = By.XPath("//button[@class = 'co_prev co_tbButton' and @oldtitle='Jump to Previous Difference']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Next difference button
        /// </summary>
        /// <returns> The <see cref="CompareVersionsDialog"/>. </returns>
        public CompareVersionsDialog ClickNextDifference()
        {
           DriverExtensions.GetElement(NextDifferenceLocator).Click();
           return  new CompareVersionsDialog();
        }

        /// <summary>
        /// Click Prev difference button
        /// </summary>
        /// <returns> The <see cref="CompareVersionsDialog"/>. </returns>
        public CompareVersionsDialog ClickPrevDifference()
        {
            DriverExtensions.GetElement(PrevDifferenceLocator).Click();
            return new CompareVersionsDialog();
        }

        /// <summary>
        /// Verifies that the key line is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>.True if key line is displayed. </returns>
        public bool IsNextDifferenceDisplayed() => DriverExtensions.IsDisplayed(NextDifferenceLocator);

        /// <summary>
        /// Verifies that the key line is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>.True if key line is displayed. </returns>
        public bool IsPrevDifferenceDisplayed() => DriverExtensions.IsDisplayed(PrevDifferenceLocator);
    }
}
