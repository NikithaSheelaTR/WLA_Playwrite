namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Internal Users Only Component for Documents with Headnotes
    /// </summary>
    public class InternalUsersOnlyComponent : BaseModuleRegressionComponent
    {
        private static readonly By InternalUsersOnlyAllElementsLocator =
            By.XPath("//div[@class='co_headnotePublicationBlockContainer']/div[contains(@class, 'co_headnotePublicationBlock')]");

        private static readonly By InternalUsersOnlyBlockLocator =
            By.XPath("//div[@class='co_headnote']/div[@class='co_headnotePublicationBlock']");

        private static readonly By InternalUsersOnlyBlockTitleLocator = By.XPath("//h3[text()='Internal users only']");

        private static readonly By InternalUsersOnlySearchTermsLocator =
            By.XPath("//div[@class= 'co_headnotePublicationBlock_wordsAndPhrasesItem'] /span[@class='co_searchTerm']");

        private static readonly By InternalUsersOnlyWordAndPhrasesLocator =
            By.ClassName("co_headnotePublicationBlock_wordsAndPhrases");

        private static readonly By NoPriorAndClassNumberIdElementsLocator =
            By.XPath("//div[contains(@class,'co_headnotePublicationBlock_notPriorItem')]|//div[contains(@class,'co_headnotePublicationBlock_classNumberID')]");

        private static readonly By ContainerLocator = By.XPath(
            "//div[@class='co_headnotePublicationBlock' and .//div[contains(@class,'co_headnotePublicationBlock_notPriorItem')]]");

        /// <summary>
        /// Word and Phrases in Internal Users Only Component 
        /// </summary>
        public string InternalUsersOnlyWordAndPhrases => DriverExtensions.WaitForElement(InternalUsersOnlyWordAndPhrasesLocator).Text;

        /// <summary>
        /// Internal Users Only Component Title
        /// </summary>
        public string InternalUsersOnlyСomponentTitle => DriverExtensions.WaitForElement(InternalUsersOnlyBlockTitleLocator).Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify the layout of elements in Internal Users Only Plan. 
        /// No Prior and Class Number Id Elements should have not null margin-top CSS values.
        /// </summary>
        /// <returns> True if element grouped correctly, false otherwise </returns>
        public bool AreAllElementsInTheBlockGroupedCorrectly() =>
            DriverExtensions.GetElements(NoPriorAndClassNumberIdElementsLocator)
                            .ToList()
                            .TrueForAll(el => el.GetCssValue("margin-top") != string.Empty);

        /// <summary>
        /// Are Search Terms In Internal Users Only component bold
        /// </summary>
        /// <returns>true if search terms are bold</returns>
        public bool AreSearchTermsBoldInInternalUsersOnlyComponent() =>
            DriverExtensions.GetElements(InternalUsersOnlySearchTermsLocator)
                            .ToList()
                            .TrueForAll(el => Convert.ToInt32(el.GetCssValue("font-weight")) >= 600);

        /// <summary>
        /// Gets all elements text from Internal Users Only Component
        /// </summary>
        /// <returns> List of elements </returns>
        public List<string> GetAllInternalUsersOnlyComponentElements() => DriverExtensions.GetElements(InternalUsersOnlyAllElementsLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Get Highlighted Terms from Internal Users Only component
        /// </summary>
        /// <returns> List of highlighted elements </returns>
        public List<string> GetHighlightedInternalUsersOnlyComponentElements() => DriverExtensions.GetElements(InternalUsersOnlySearchTermsLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Is Internal Users Only component present
        /// </summary>
        /// <returns> True if component is displayed, false otherwise </returns>
        public bool IsInternalUsersOnlyComponentDisplayed() => DriverExtensions.IsDisplayed(InternalUsersOnlyBlockLocator);
    }
}