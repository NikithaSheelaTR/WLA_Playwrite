namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.KeyCiteFlagDialog
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge KeyCite flag dialog
    /// </summary>
    public class KeyCiteFlagDialog : BaseModuleRegressionDialog
    {
        private static readonly By ViewAllLinkLocator = By.ClassName("co_kcFlagPopup_viewAll");
        private static readonly By HeaderTitleLocator = By.ClassName("co_kcFlagPopup_header");
        private static readonly By KeyCiteItemLocator = By.XPath("//div[contains(@id,'co_kcFlagPopup')]//img[contains(@title, 'KeyCite')]//parent::div");
        private static readonly By LinkLocator = By.XPath("//div[@class='co_kcFlagPopup_doc']//a");
        private static readonly By DialogContainerLocator = By.ClassName("co_kcFlagPopup");
        private static readonly By CloseButtonLocator = By.ClassName("co_kcFlagPopup_closeButton");

        /// <summary>
        /// Click View all link 
        /// </summary>
        /// <typeparam name="T">TabPage</typeparam>
        /// <returns>Returns RelatedInfoPage</returns>
        public T ClickViewAllLink<T>() where T : TabPage => this.ClickElement<T>(ViewAllLinkLocator);

        /// <summary>
        /// Click Close Button
        /// </summary>
        /// <typeparam name="T">EdgeCommonSearchResultPage</typeparam>
        /// <returns>EdgeCommonSearchResultPage instance</returns>
        public T ClickCloseButton<T>()
            where T : ICreatablePageObject =>
            this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Get title
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
       public string GetHeaderTitle() => DriverExtensions.GetElement(HeaderTitleLocator).Text.RetainText();

        /// <summary>
        /// Get negative treatment flags
        /// </summary>
        /// <returns>List of flags</returns>
        public List<KeyCiteFlag> GetFlags() =>
            DriverExtensions.GetElements(KeyCiteItemLocator).Select(i => i.GetAttribute("class")).Select(
                                a => a.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(mod => mod.ClassName))
                            .ToList();

        /// <summary>
        /// Get text from dialog - cuts off text from header and returns text only for negative history items.
        /// </summary>
        /// <returns>Text</returns>
        public string GetNegativeTreatmentText(int TextCount) =>
            DriverExtensions.GetText(DialogContainerLocator).Substring(
                DriverExtensions.GetText(DialogContainerLocator).IndexOf(
                    "View All",
                    StringComparison.Ordinal) + TextCount);

        /// <summary>
        /// Get documents titles
        /// </summary>
        /// <returns>List with links in the popup</returns>
        public List<string> GetDocumentsTitles() =>
            DriverExtensions.GetElements(LinkLocator).Select(element => element.Text).ToList();
    }
}