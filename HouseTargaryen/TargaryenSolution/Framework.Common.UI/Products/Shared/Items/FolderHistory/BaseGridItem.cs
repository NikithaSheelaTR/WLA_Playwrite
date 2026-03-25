namespace Framework.Common.UI.Products.Shared.Items.FolderHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Base Grid Item for Folder and History pages
    /// </summary>
    public class BaseGridItem : BaseItem
    {
        private static readonly By CitationsLocator = By.XPath(".//div[@class = 'cobalt_ro_documentDescription']/span");

        private static readonly By FlagImageLocator = By.XPath(".//span[contains(@class, 'keyciteContainer')]/a");

        private EnumPropertyMapper<Columns, WebElementInfo> columnsMap;

        private EnumPropertyMapper<KeyCiteFlag, WebElementInfo> keyCiteFlagsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllHistoryTableItem"/> class. 
        /// History Table Item
        /// </summary>
        /// <param name="tableEntryContainer">
        /// The table Entry Container.
        /// </param>
        public BaseGridItem(IWebElement tableEntryContainer) : base(tableEntryContainer)
        {
        }

        /// <summary>
        /// The ClientId column
        /// </summary>
        public string ClientId =>
            DriverExtensions.SafeGetElement(this.Container, By.XPath(this.ColumnsMap[Columns.ClientId].LocatorString))?.Text;


        /// <summary>
        /// Gets the text of 'Content' section
        /// </summary>
        public string Content => DriverExtensions.SafeGetElement(this.Container, By.XPath(this.ColumnsMap[Columns.Content].LocatorString))?.Text;

        /// <summary>
        /// Gets citations list
        /// </summary>
        /// <returns>
        /// The list of citations.
        /// </returns>
        public List<string> Citations =>
            DriverExtensions.GetElements(this.DetailsElement, CitationsLocator).Select(c => c.Text).ToList();

        /// <summary>
        /// Gets the Date Time from the column
        /// </summary>
        /// <returns> The DateTime from the Date/Time column </returns>
        public DateTime Date
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, By.XPath(this.ColumnsMap[Columns.DateTime].LocatorString)))
                {
                    string dateText = DriverExtensions.WaitForElement(
                        this.Container,
                        By.XPath(this.ColumnsMap[Columns.DateTime].LocatorString)).Text;
                    if (dateText.Contains("a.m.") || dateText.Contains("p.m."))
                    {
                        dateText = dateText.Replace("p.m.", "PM").Replace("a.m.", "AM");
                    }

                    return DateTime.Parse(dateText);
                }
                return new DateTime();
            }
        }

        /// <summary>
        /// Gets the details from the document header
        /// </summary>
        /// <returns> The details from the Document header </returns>
        public string Details
        {
            get
            {
                string[] delimElement = this.DetailsElement.Text.Split(new[] { "\n" }, StringSplitOptions.None);
                return delimElement.Length <= 2 || delimElement[2].Trim().ToLower().Contains("add description to") ? string.Empty : delimElement[2].Trim();
            }
        }

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public KeyCiteFlag Flag
        {
            get
            {
                if (DriverExtensions.IsDisplayed(this.Container, FlagImageLocator))
                {
                    string flagClass = this.KeyCiteFlagElement.GetAttribute("class");
                    return this.KeyCiteFlagsMap.Single(
                                   map => !string.IsNullOrEmpty(map.Value.ClassName) 
                                          && flagClass.Contains(map.Value.ClassName))
                               .Key;
                }

                return KeyCiteFlag.NoFlag;
            }
        }

        /// <summary>
        /// Gets the link of the title
        /// </summary>
        /// <returns> The Link from the title</returns>
        public string Link => DriverExtensions.SafeGetElement(this.DetailsElement, By.TagName("a"))?.GetAttribute("href");

        /// <summary>
        /// Gets the title from the document header
        /// </summary>
        /// <returns> The title from the Document header </returns>
        public string Title => this.DetailsElement.Text.Split(
            new[]
            {
                "\n", "\r"
            },
            StringSplitOptions.None)[0];

        /// <summary>
        /// Gets the summary from the document header
        /// </summary>
        /// <returns> The summary from the Document header </returns>
        public string Summary
        {
            get
            {
                string[] delimElement = this.DetailsElement.Text.Split(new[] { "\n" }, StringSplitOptions.None);
                return delimElement.Length == 1 || delimElement[1].Trim().ToLower().Contains("add description to") ? string.Empty : delimElement[1].Trim();
            }
        }

        /// <summary>
        /// Gets the Columns enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<Columns, WebElementInfo> ColumnsMap
            => this.columnsMap = this.columnsMap ?? EnumPropertyModelCache.GetMap<Columns, WebElementInfo>();

        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected virtual EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap
            => this.keyCiteFlagsMap = this.keyCiteFlagsMap ?? EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>();

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement => DriverExtensions.SafeGetElement(this.Container, FlagImageLocator);

        /// <summary>
        /// Description/Title column
        /// </summary>
        private IWebElement DetailsElement =>
            DriverExtensions.WaitForElement(this.Container, By.XPath(this.ColumnsMap[Columns.Title].LocatorString));
    }
}