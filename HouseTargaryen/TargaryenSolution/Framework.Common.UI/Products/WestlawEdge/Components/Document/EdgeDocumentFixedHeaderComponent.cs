namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Components.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Document;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Document Fixed Header Component
    /// </summary>
    public class EdgeDocumentFixedHeaderComponent : DocumentFixedHeaderComponent
    {
        private static readonly By AddToKeepListButtonLocator = By.XPath("//input[contains(@class, 'Keeplist-checkbox')]");
        private static readonly By FlagImageContainerLocator = By.Id("co_docHeaderCitatorFlag");
        private static readonly By FlagImageLocator = By.CssSelector("#co_docFixedHeader .co_citatorFlag");
        private static readonly By ImpliedOverrulingImageLocator = By.CssSelector("#co_docFixedHeader .co_citatorFlag img[alt*='Overruling Risk']");
        private static readonly By ReturnToListLocator = By.CssSelector("#co_docHeaderReturnTo > a > span");
        private static readonly By ReturnToListTextLocator = By.CssSelector("#co_documentFooterBreadcrumb > a > span");
        private static readonly By RelatedDocumentsButtonContainerLocator = By.XPath("//a[contains(@class, 'RelatedDocsJumpButton')]");
        private static readonly By RelatedDocumentsButtonArrowLocator = By.XPath("./span[contains(@class, 'icon_downArrow')]");
        private static readonly By ReturnToPreviousDocumentLocator = By.CssSelector("a.DocumentBackToButton");
        private static readonly By PLDocReturnToReportButtonLocator = By.Id("coid_backToResultsLink");

        private EnumPropertyMapper<EdgeDocumentIcon, WebElementInfo> edgeDocIconsMap;

        /// <summary>
        /// Add to Keep List button
        /// </summary>
        public IButton AddToKeepListButton => new Button(AddToKeepListButtonLocator);

        /// <summary>
        /// Practical Law Document Return to Report button
        /// </summary>
        public IButton PLDocReturnToReportButton => new Button(PLDocReturnToReportButtonLocator);

        /// <summary>
        /// EdgeDocIconsMap
        /// </summary>
        protected EnumPropertyMapper<EdgeDocumentIcon, WebElementInfo> EdgeDocIconsMap =>
            this.edgeDocIconsMap = this.edgeDocIconsMap
                                   ?? EnumPropertyModelCache.GetMap<EdgeDocumentIcon, WebElementInfo>(
                                       string.Empty,
                                       @"Resources/EnumPropertyMaps/WestlawEdge/Document");
        /// <summary>
        /// Gets the Flag enumeration to WebElementInfo map.
        /// </summary>
        protected new EnumPropertyMapper<KeyCiteFlag, WebElementInfo> KeyCiteFlagsMap =>
            EnumPropertyModelCache.GetMap<KeyCiteFlag, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Document");        

        /// <summary>
        /// Gets the KeyCite Flag from the document header
        /// </summary>
        /// <returns> The KeyCite Flag from the Document header </returns>
        public new KeyCiteFlag GetKeyCiteFlag()
        {
            if (DriverExtensions.IsDisplayed(FlagImageLocator))
            {
                string flagClass = DriverExtensions.GetElement(FlagImageLocator).GetAttribute("class");
              
                return KeyCiteFlagsMap.FirstOrDefault(
                    map => !string.IsNullOrEmpty(map.Value.ClassName)
                           && flagClass.Contains(map.Value.ClassName)).Key;
            }

            return KeyCiteFlag.NoFlag;
        }

        /// <summary>
        /// Check if Key Cite Flag present in the document
        /// </summary>
        /// <returns>true if flag present</returns>
        public new bool IsKeyCiteFlagDisplayed()
            =>
                DriverExtensions.IsDisplayed(FlagImageContainerLocator) && DriverExtensions.IsElementPresent(FlagImageLocator);

        /// <summary>
        /// Is ReturnToList button displayed
        /// </summary>
        /// <returns>true if flag present</returns>
        public bool IsReturnToListButtonDisplayed() => DriverExtensions.IsDisplayed(ReturnToListLocator);

        /// <summary>
        /// Click ReturnToList button 
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns> 
        /// </summary>
        public T ClickReturnToListButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnToListLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is ReturnToList Text button displayed
        /// </summary>
        /// <returns>true if flag present</returns>
        public bool IsReturnToListTextButtonDisplayed() => DriverExtensions.IsDisplayed(ReturnToListTextLocator);

        /// <summary>
        /// Click ReturnToList Text button 
        /// <typeparam name="T">T</typeparam>
        /// <returns>The new instance of T page</returns> 
        /// </summary>
        public T ClickReturnToListTextButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnToListTextLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verifies that the related documents button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the related documents button is displayed. </returns>
        public bool IsRelatedDocumentsButtonDisplayed() => DriverExtensions.IsDisplayed(
            RelatedDocumentsButtonContainerLocator);

        /// <summary>
        /// Gets related documents button name.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>. Related documents button name. </returns>
        public string GetRelatedDocumentsButtonName() => DriverExtensions.GetText(
            RelatedDocumentsButtonContainerLocator);

        /// <summary>
        /// Verifies that the related documents button Down Arrow is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if the related documents button Down Arrow is displayed. </returns>
        public bool IsRelatedDocumentsButtonArrowDisplayed() => DriverExtensions.IsDisplayed(
            RelatedDocumentsButtonContainerLocator, RelatedDocumentsButtonArrowLocator);

        /// <summary>
        /// Clicks related documents button. </summary>
        /// <returns>
        /// The <see cref="EdgeCommonDocumentPage"/>. </returns>
        public EdgeCommonDocumentPage ClickRelatedDocumentsButton()
        {
            DriverExtensions.Click(RelatedDocumentsButtonContainerLocator);
            return new EdgeCommonDocumentPage();
        }

        /// <summary>
        /// Clicks on the document status icon
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="iconType">IconType</param>
        /// <returns>The new instance of T page</returns>
        public T ClickEdgeStatusIcon<T>(EdgeDocumentIcon iconType) where T : ICreatablePageObject
        {
            DriverExtensions.Click(By.XPath(this.EdgeDocIconsMap[iconType].LocatorString));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check if Implied Overruling Flag present in the document
        /// </summary>
        /// <returns>true if flag present</returns>
       
        public bool IsImpliedOverrulingDisplayed() => DriverExtensions.IsDisplayed(ImpliedOverrulingImageLocator);

        /// <summary>
        /// Checks status icon presence
        /// </summary>
        /// <param name="iconType">icon type</param>
        /// <returns>true if icon displayed</returns>
        public bool IsStatusIconDisplayed(EdgeDocumentIcon iconType) => DriverExtensions.IsDisplayed(By.XPath(this.EdgeDocIconsMap[iconType].LocatorString));
        
        /// <summary>
        /// Clicks on previous button in the document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ClickReturnToPreviousDocumentButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnToPreviousDocumentLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check if Implied Overruling Flag present in the document
        /// </summary>
        /// <returns>true if flag present</returns>
        public bool IsImpliedOverrulingPresent() => DriverExtensions.IsEnabled(ImpliedOverrulingImageLocator);

    }
}