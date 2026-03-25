namespace Framework.Common.UI.Products.Shared.Components.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Key Number Hierarchy Component for Headnotes Documents
    /// </summary>
    public class KeyNumberHierarchyComponent : BaseModuleRegressionComponent
    {
        private static readonly By KeyNumberHierarchyBlockContentLocator =
            By.XPath("//div[@class='co_topicKeyContentTable']");

        private static readonly By KeyNumberHierarchyBlockLastHeadnoteLocator =
            By.XPath("(//div[@class='co_headnoteKeyPair'])[last()]");

        private static readonly By KeyNumberHierarchyBlockLocator =
            By.XPath("//div[@class='co_headnotePublicationBlock' and h3[starts-with(normalize-space(text()),'Key')]]");

        private static readonly By KeyNumberHierarchyTitleLocator =
            By.XPath("//h3[contains(@class, 'co_headnotePublicationBlockTitle') and text() = 'Key Number Hierarchy']");

        private static readonly By ContainerLocator = By.Id("co_headnotePublicationBlock");

        /// <summary>
        /// Key Number Hierarchy Title
        /// </summary>
        public string KeyNumberHierarchyTitle => DriverExtensions.WaitForElement(KeyNumberHierarchyTitleLocator).Text;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get Key Number Hierarchy Headnotes List
        /// </summary>
        /// <returns> List of the Key Number Hierarchy Headnotes </returns>
        public List<string> GetKeyNumberHierarchyHeadnotesList()
            => DriverExtensions.GetText(KeyNumberHierarchyBlockContentLocator)
                               .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

        /// <summary>
        /// Verify Key Number Hierarchy Block is displayed
        /// </summary>
        /// <returns> True if block is displayed, false otherwise </returns>
        public bool IsKeyNumberHierarchyBlockDisplayed() => DriverExtensions.IsDisplayed(KeyNumberHierarchyBlockLocator);

        /// <summary>
        /// Is Last Line In Key Number Hierarchy Bold
        /// </summary>
        /// <returns> True if style is bold, false otherwise</returns>
        public bool IsLastHeadnoteInKeyNumberHierarchyBold()
            => DriverExtensions.GetElement(KeyNumberHierarchyBlockLastHeadnoteLocator).GetCssValue("font-weight").Equals("700");
    }
}