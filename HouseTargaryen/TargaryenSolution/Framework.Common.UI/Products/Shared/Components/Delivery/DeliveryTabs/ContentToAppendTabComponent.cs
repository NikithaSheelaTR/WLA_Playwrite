namespace Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Content To Append Tab
    /// </summary>
    public class ContentToAppendTabComponent : BaseTabComponent
    {
        private static readonly By CorrespondingDocumentCheckboxLocator = By.Id("coid_chkDdcLayoutIncludeCorrespondingDocOption");

        private static readonly By SelectAllCheckboxLocator = By.Id("co_deliverySelectAllContentToAppendCheckbox");

        private static readonly By ContainerLocator = By.Id("co_deliveryOptionsTabPanel3");

        private EnumPropertyMapper<ContentToAppendKeyCiteLists, WebElementInfo> contentMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Content to Append";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the ContentToAppendKeyCiteLists enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<ContentToAppendKeyCiteLists, WebElementInfo> ContentMap
            => this.contentMap = this.contentMap ?? EnumPropertyModelCache.GetMap<ContentToAppendKeyCiteLists, WebElementInfo>();

        /// <summary>
        /// Verify Corresponding Document Checkbox is selected
        /// </summary>
        /// <returns> True if Corresponding Document Checkbox is selected, false otherwise</returns>
        public bool IsCorrespondingDocumentCheckboxChecked()
            => DriverExtensions.IsCheckboxSelected(CorrespondingDocumentCheckboxLocator);

        /// <summary>
        /// Verify KeyCite Lists option is displayed
        /// </summary>
        /// <param name="expectedTabOption"> The option to look for </param>
        /// <returns> True if KeyCite Lists Option is displayed, false otherwise </returns>
        public bool IsOptionOfKeyCiteListsDisplayed(ContentToAppendKeyCiteLists expectedTabOption)
            => DriverExtensions.IsDisplayed(By.Id(this.ContentMap[expectedTabOption].Id));

        /// <summary>
        /// Verify KeyCite Lists option on the Content To Append tab is selected
        /// </summary>
        /// <param name="expectedTabOption"> KeyCite Lists Option </param>
        /// <returns> True if KeyCite Lists Option is selected, false otherwise </returns>
        public bool IsOptionOfKeyCiteListsSelected(ContentToAppendKeyCiteLists expectedTabOption) =>
            DriverExtensions.IsCheckboxSelected(By.Id(this.ContentMap[expectedTabOption].Id));

        /// <summary>
        /// Click Select All checkbox
        /// </summary>
        public void SelectAllCheckbox() => DriverExtensions.SetCheckbox(SelectAllCheckboxLocator, true);

        /// <summary>
        /// Select KeyCite Lists option on the Content To Append tab
        /// </summary>
        /// <param name="expectedTabOption"> KeyCite Lists Option to select </param>
        public void SelectContentToAppendTabKeyCiteListsOption(ContentToAppendKeyCiteLists expectedTabOption)
            => DriverExtensions.SetCheckbox(By.Id(this.ContentMap[expectedTabOption].Id), true);

        /// <summary>
        /// Select Corresponding Document Checkbox
        /// </summary>
        public void SelectCorrespondingDocumentCheckbox() => DriverExtensions.SetCheckbox(CorrespondingDocumentCheckboxLocator, true);

        /// <summary>
        /// Returns KeyCite no result labels list
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetZeroKeyCiteLabelsList()
            => this.GetZeroKeyCiteResultsWebElements().Select(x => DriverExtensions.GetText(By.XPath(x.LocatorString))).ToList();

        /// <summary>
        /// Returns list of keycite zero results webelements
        /// </summary>
        /// <returns></returns>
        private List<WebElementInfo> GetZeroKeyCiteResultsWebElements()
        {
            var result = new List<WebElementInfo>();
            foreach (var item in this.ContentMap)
            {
                if (!string.IsNullOrEmpty(item.Value.TagName) && item.Value.TagName.Equals("Zero results notification") && DriverExtensions.IsDisplayed(By.XPath(item.Value.LocatorString)))
                {
                    result.Add(item.Value);
                }
            }

            return result;
        }
    }
}