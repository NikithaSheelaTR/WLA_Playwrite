namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo.StatuteHistoryPages
{
    using System;

    using Framework.Common.UI.Enums.RI;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// GraphicalStatutePage
    /// </summary>
    public class GraphicalStatutePage : TabPage
    {
        private static readonly By EarlierLegislationLocator = 
            By.Id("co_relatedInfo_resultList_statuteDocument_prior");

        private static readonly By GraphicalStatuteCaseTreatmentsAreaCheckboxLocator =
            By.XPath("//input[contains(@id, 'co_relatedInfo_caseTreatment_selectAll')]");

        private static readonly By GraphicalStatuteCaseTreatmentsExpandButtonLocator =
            By.Id("coid_relatedInfo_collapselink_caseTreatment");

        private static readonly By GraphicalStatuteCurrentTextAreaCheckboxLocator =
            By.XPath("//input[contains(@id, 'co_selectAll_statuteHeader')]");

        private static readonly By GraphicalStatuteCurrentTextAreaLocator =
            By.XPath("//div[./div/span/label[contains(text(), 'Current Text')]]");

        private static readonly By GraphicalStatuteCurrentTextExpandButtonLocator =
            By.XPath("//a[contains(@id, 'coid_relatedInfo_collapselink_20')]");

        private static readonly By GraphicalStatuteInfoContainerLocator = By.Id("co_graphicalStatute_ContainerDiv");
       
        private static readonly By SeeCreditsLinkLocator = 
            By.Id("coid_relatedInfo_subCategory_link_credits");

        private static readonly By StatuteDocumentContainerLocator =
            By.XPath("./div[contains(@id, 'co_container_statuteDocument')]");

        private static readonly By TimelineContainerLocator = By.Id("co_relatedinfo_timelineContainer");

        private static readonly string KeyCiteFlagLocator = "//span[./a[text()={0}]]/a";

        private EnumPropertyMapper<GraphicalStatuteBox, WebElementInfo> graphicalStatuteBoxMap;

        /// <summary>
        /// Gets the key cite flag element.
        /// </summary>
        private IWebElement KeyCiteFlagElement(string title) => DriverExtensions.SafeGetElement(SafeXpath.BySafeXpath(KeyCiteFlagLocator, title));

        /// <summary>
        /// GraphicalStatuteBoxMap
        /// </summary>
        protected EnumPropertyMapper<GraphicalStatuteBox, WebElementInfo> GraphicalStatuteBoxMap
            => this.graphicalStatuteBoxMap = this.graphicalStatuteBoxMap ?? EnumPropertyModelCache.GetMap<GraphicalStatuteBox, WebElementInfo>();

        /// <summary>
        /// Click GraphicalStatuteCaseTreatmentsExpandButton
        /// </summary>
        public void ClickGraphicalStatuteCaseTreatmentsExpandButton()
            => DriverExtensions.WaitForElement(GraphicalStatuteCaseTreatmentsExpandButtonLocator).Click();

        /// <summary>
        /// Click GraphicalStatuteCurrentTextExpandButton
        /// </summary>
        public void ClickGraphicalStatuteCurrentTextExpandButton()
            => DriverExtensions.WaitForElement(GraphicalStatuteCurrentTextExpandButtonLocator).Click();

        /// <summary>
        /// Get Earlier Legislation text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetEarlierLegislationText() 
            => DriverExtensions.WaitForElement(EarlierLegislationLocator).Text;

        /// <summary>
        /// Get GraphicalStatuteCurrentText
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetGraphicalStatuteCurrentText()
            => DriverExtensions.WaitForElement(GraphicalStatuteCurrentTextAreaLocator).Text;

        /// <summary>
        /// Determine if the Graphical Statute area in the body is collapsed
        /// </summary>
        /// <param name="area">element for the area that is either collapsed or not</param>
        /// <returns>True if collapsed</returns>
        public bool IsAreaCollapsed(GraphicalStatuteBox area)
            => DriverExtensions.GetElement(
               By.XPath(this.GraphicalStatuteBoxMap[area].LocatorString),
               StatuteDocumentContainerLocator).GetAttribute("class").Contains("co_hideState");

        /// <summary>
        /// Determine if the area in the body is in view
        /// </summary>
        /// <param name="area">element for the area that will be checked</param>
        /// <returns>True if in view</returns>
        public bool IsAreaScrolledIntoView(GraphicalStatuteBox area)
            => DriverExtensions.GetElement(By.XPath(this.GraphicalStatuteBoxMap[area].LocatorString)).IsElementInView();

        /// <summary>
        /// Is GraphicalStatuteCaseTreatmentsAreaCheckbox Selected
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGraphicalStatuteCaseTreatmentsAreaCheckboxSelected()
            => DriverExtensions.IsCheckboxSelected(GraphicalStatuteCaseTreatmentsAreaCheckboxLocator);

        /// <summary>
        /// Is GraphicalStatuteCurrentTextAreaCheckbox Selected
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGraphicalStatuteCurrentTextAreaCheckboxSelected()
            => DriverExtensions.IsCheckboxSelected(GraphicalStatuteCurrentTextAreaCheckboxLocator);

        /// <summary>
        /// Is GraphicalStatuteInfoContainer Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsGraphicalStatuteInfoContainerDisplayed()
            => DriverExtensions.IsDisplayed(GraphicalStatuteInfoContainerLocator);

        /// <summary>
        /// Is 'See Credits' link displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSeeCreditsLinkDisplayed() 
            => DriverExtensions.IsDisplayed(SeeCreditsLinkLocator);

        /// <summary>
        /// Is Timeline Container Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTimelineContainerDisplayed() => DriverExtensions.IsDisplayed(TimelineContainerLocator);

        /// <summary>
        /// Gets the key cite flag.
        /// </summary>
        public KeyCiteFlag GetKeyCiteFlag(string title)
        {
            if (DriverExtensions.IsDisplayed(this.KeyCiteFlagElement(title)))
            {
                string flagClass = DriverExtensions.GetAttribute("class", this.KeyCiteFlagElement(title));
                return flagClass.GetEnumValueByPropertyModel<KeyCiteFlag, WebElementInfo>(
                    model => model.ClassName,
                    String.Empty);
            }

            return KeyCiteFlag.NoFlag;
        }
    }
}