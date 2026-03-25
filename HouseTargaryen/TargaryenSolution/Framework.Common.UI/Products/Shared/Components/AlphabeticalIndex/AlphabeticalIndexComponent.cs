namespace Framework.Common.UI.Products.Shared.Components.AlphabeticalIndex
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alphabetical index component
    /// </summary>
    public class AlphabeticalIndexComponent : BaseModuleRegressionComponent
    {
        private static readonly By SelectedLetterLocator = By.ClassName("co_alphabetIndexSelected");

        private static readonly By AlphabeticalNavigationLocator = By.XPath("//ul[@class='co_alphabetIndex']/li[not(@id='coid_indexDeliveryWidgetContainer')]");

        private static readonly By IndexContentLocator = By.XPath("//div[@id='co_indexContent']//li");

        private static readonly By ContainerLocator = By.Id("coid_website_indexCharacterLinks");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the Alphabetical link by given index.
        /// </summary>
        /// <param name="alphabeticalIndex">Alphabetical index to click on</param>
        public void ClickOnAlphabeticalIndex(string alphabeticalIndex) => DriverExtensions
            .GetElements(AlphabeticalNavigationLocator).FirstOrDefault(x => x.Text.Equals(alphabeticalIndex)).Click();

        /// <summary>
        /// Retrieve List of indexes.
        /// </summary>
        /// <returns>List of index page content</returns>
        public List<string> GetIndexContentList() =>
            DriverExtensions.GetElements(IndexContentLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Retrieve List of alphabetical links.
        /// </summary>
        /// <returns>List of alphabetical links names</returns>
        public List<string> GetAlphabeticalNavigationList() =>
            DriverExtensions.GetElements(AlphabeticalNavigationLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Get selected letter
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSelectedLetter() => DriverExtensions.GetText(SelectedLetterLocator);

        /// <summary>
        /// Is alphabetical navigation displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAlphabeticalNavigationDisplayed() => DriverExtensions.IsDisplayed(DriverExtensions.GetElement(AlphabeticalNavigationLocator));
    }
}