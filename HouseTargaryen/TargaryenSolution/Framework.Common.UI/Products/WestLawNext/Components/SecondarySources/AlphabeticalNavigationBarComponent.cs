namespace Framework.Common.UI.Products.WestLawNext.Components.SecondarySources
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alphabetical navigation bar on Secondary Sources page
    /// </summary>
    public class AlphabeticalNavigationBarComponent : BaseModuleRegressionComponent
    {
        private const string AlphabeticElementLctMsk = "a[data-item={0}]";

        private static readonly By ContainerLocator = By.ClassName("co_alphaSelector_filter");

        private static readonly By DisabledAlphabeticalsElementsLocator =
            By.XPath("//li[@class='co_search_alphabetical_navigation_label co_disabled']/a");

        private static readonly By EnabledAlphabeticalsElementsLocator =
            By.XPath("//li[@class='co_search_alphabetical_navigation_label']/a");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the letter on the alphabetical navigation bar.
        /// </summary>
        /// <param name="letter">A letter.</param>
        /// <returns>New instance of Secondary sources page.</returns>
        public SecondarySourcesPage ClickOnTheLetter(string letter)
        {
            DriverExtensions.Click(By.CssSelector(string.Format(AlphabeticElementLctMsk, letter)));
            return new SecondarySourcesPage();
        }

        /// <summary>
        /// Get list of disabled letters from the alphabetical navigation bar
        /// </summary>
        /// <returns>List of disabled letters.</returns>
        public List<string> GetListOfDisabledLetters() => DriverExtensions
            .GetElements(DisabledAlphabeticalsElementsLocator).Select(el => el.Text.Trim()).ToList();

        /// <summary>
        /// Get list of enabled letters from the alphabetical navigation bar
        /// </summary>
        /// <returns>List of enabled letters.</returns>
        public List<string> GetListOfEnabledLetters() => DriverExtensions
            .GetElements(EnabledAlphabeticalsElementsLocator).Select(el => el.Text.Trim()).ToList();

        /// <summary>
        /// Is alphabetical navigation bar displayed.
        /// </summary>
        /// <returns>True if displayed, false otherwise.</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);
    }
}