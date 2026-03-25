namespace Framework.Common.UI.Products.WestLawNextCanada.Dialogs.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog that appears after clicking on highlight text on the Document page
    /// </summary>
    public class CanadaManageHighlightDialog : ManageHighlightDialog
    {
        private static readonly By HighlightColorBoxContainerLocator = By.Id("co_highlightMenuEditHighlightColor");

        private EnumPropertyMapper<HighlightColor, WebElementInfo> manageHighlightColorMap;
        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> ManageHighlightColorMap =>
            this.manageHighlightColorMap = this.manageHighlightColorMap
                                           ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>(
                                               "Update", @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Update the color for highlighted text from the highlight dialog
        /// </summary>
        /// <param name="color">color to be highlighted</param>
        /// <returns> Page instance of the document </returns>
        public T UpdateColorForHighlightedText<T>(HighlightColor color)
            where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(
                DriverExtensions.GetElement(HighlightColorBoxContainerLocator),
                By.XPath(this.ManageHighlightColorMap[color].LocatorString)).CustomClick();

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
