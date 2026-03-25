namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Table of contents left panel component.
    /// </summary>
    public class TableOfContentsLeftPanelComponent : BaseModuleRegressionComponent
    {
        private const string RandomSectionLctMask = ".//*[contains(@id, '{0}')]//a";
        private static readonly By TocComponentLocator = By.XPath("//div[@class='co_genericDocumentTOC']");
        private static readonly By AddToCompareButtonLocator = By.Id("co_internalToc_AddToCompareButton");
        private static readonly By TocSectionsLocator = By.XPath("//label[contains(@for,'co_internalToc')]");
        private static readonly By TocCheckboxLocator = By.XPath("//input[contains(@id,'co_internalToc_checkbox')]");
        private static readonly By TocLinkLocator = By.XPath("//li[contains(@id, 'co_internalToc') and ./input[contains(@id,'co_internalToc_checkbox')]]//a");

        /// <summary>
        /// AddToCompareDropdown
        /// </summary>
        public AddToCompareDropdown AddToCompareDropdown { get; } = new AddToCompareDropdown(TocComponentLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TocComponentLocator;

        /// <summary>
        /// Gets the Add to Compare link.
        /// </summary>
        public ILink AddToCompareLink { get; } = new AddToCompareLink(AddToCompareButtonLocator);

        /// <summary>
        /// Get random section name
        /// </summary>
        /// <returns> Section name </returns>
        public string GetRandomSectionId() =>
             DriverExtensions.GetElements(TocSectionsLocator)
                            .ToArray()[new Random().Next(1, DriverExtensions.GetElements(TocSectionsLocator).Count)].GetAttribute("for").Replace("checkbox_", string.Empty);

        /// <summary>
        /// Click on random document
        /// </summary>
        /// <param name="id"> The link Text. </param>
        /// <returns> New instance of the page
        /// </returns>
        public CommonDocumentPage ClickOnRandomDocumentSectionById(string id)
        {
            DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(TocComponentLocator), By.XPath(string.Format(RandomSectionLctMask, id))).Click();
            return DriverExtensions.CreatePageInstance<CommonDocumentPage>();
        }

        /// <summary>
        /// Set specified checkbox
        /// </summary>
        /// <param name="number">Check box number.</param>
        public void SetCheckbox(int number) => DriverExtensions.GetElements(TocCheckboxLocator).ToList()[number].SetCheckbox(true);

        /// <summary>
        /// Get specified checkbox text
        /// </summary>
        /// <param name="number">
        /// Check box number.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetCheckboxText(int number)
            => DriverExtensions.GetElements(TocLinkLocator).ToList()[number].GetText();
    }
}