namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Object representing a Table of Contents browse page, with checkboxes
    /// </summary>
    public class TableOfContentsBrowsePage : CheckboxBrowsePage
    {
        private static readonly By BrowsePageTitleLocator = By.Id("co_contentColumn");

        private static readonly By BrowseTocLocator = By.Id("coid_browseToc");

        private static readonly By KeyCiteFlagLocator = By.XPath("//a[@class='co_tocKeyCiteFlagLink']");

        /// <summary>
        /// Initializes a new instance of the <see cref="TableOfContentsBrowsePage"/> class. 
        /// </summary>
        public TableOfContentsBrowsePage()
        {
            DriverExtensions.WaitForElement(BrowsePageTitleLocator);
        }

        /// <summary> 
        /// Effective Date component 
        /// </summary>
        public EffectiveDateComponent EffectiveDateComponent { get; } = new EffectiveDateComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Set Specified TOK Areas
        /// </summary>
        /// <param name="selectedArea"> selected Area </param>
        /// <returns> <see cref="TableOfContentsBrowsePage"/>. </returns>
        public TableOfContentsBrowsePage SetSpecifiedTocAreas(List<string> selectedArea)
        {
            this.CheckboxComponent.SetShowCheckboxes(true);
            selectedArea.ForEach(area => this.CheckboxComponent.SelectTableOfContentsCheckbox<TableOfContentsBrowsePage>(area));
            return this;
        }

        /// <summary>
        /// Verify if KeyCite flag displayed on the page
        /// </summary>
        /// <returns>true if displayed, false otherwise</returns>
        public bool IsKeyCiteFlagDisplayed() => DriverExtensions.IsDisplayed(KeyCiteFlagLocator);

        /// <summary>
        /// Checks to see if the BrowseTOC div is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsBrowseTocDisplayed() => DriverExtensions.IsDisplayed(BrowseTocLocator, 5);

        /// <summary>
        /// Click on a KeyCite Flag in the doc by flag name (case-insensitive).
        /// </summary>
        /// <param name="flagName">The display name or description of the flag (e.g., "Red Flag", "Yellow Flag").</param>
        /// <typeparam name="T">The page object to return after click.</typeparam>
        /// <returns>The new page object after clicking the flag.</returns>
        public T ClickKeyCiteFlagByName<T>(string flagName) where T : ICreatablePageObject
        {
            var flagElements = DriverExtensions.GetElements(By.XPath("//a[@class='co_tocKeyCiteFlagLink']"));
            var flagElement = flagElements
                .FirstOrDefault(el =>
                {
                    // Check anchor text
                    if (el.Text.Trim().Equals(flagName, StringComparison.OrdinalIgnoreCase))
                        return true;

                    // Check <img> child attributes
                    var img = el.FindElements(By.TagName("img")).FirstOrDefault();
                    if (img != null)
                    {
                        var title = img.GetAttribute("title");
                        var alt = img.GetAttribute("alt");
                        if ((title != null && title.IndexOf(flagName, StringComparison.OrdinalIgnoreCase) >= 0) ||
                            (alt != null && alt.IndexOf(flagName, StringComparison.OrdinalIgnoreCase) >= 0))
                            return true;
                    }
                    return false;
                });

            if (flagElement == null)
                throw new ArgumentException($"No KeyCite flag found with name or tooltip '{flagName}'", nameof(flagName));

            flagElement.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
    }