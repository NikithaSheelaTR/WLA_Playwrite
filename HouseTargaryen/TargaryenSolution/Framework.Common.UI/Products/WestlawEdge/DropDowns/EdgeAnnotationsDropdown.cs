namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    using EnumExtension = Framework.Core.CommonTypes.Extensions.EnumExtension;

    /// <summary>
    /// EdgeAnnotationsDropdown
    /// </summary>
    public class EdgeAnnotationsDropdown : BaseAnnotationsDropdown<EdgeAnnotationsOption>
    {
        private static readonly By DropdownOptionLocator
            = By.XPath("//li[@id='co_docToolbarAddNoteWidget']//div[@aria-label='Open Annotations menu']/ul/li[not(contains(@class, 'disabled'))]");

        private static readonly By SelectedOptionLocator = By.XPath("//li[@id='co_docToolbarAddNoteWidget']//div[@id='coid_annotationsMenu']//button[@class='active']");

        private static readonly By IconLocator = By.XPath(".//span[contains (@class, 'icon')]");

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        protected EnumPropertyMapper<EdgeAnnotationsOption, WebElementInfo> AnnotationsMap =>
            this.annotationsMap = this.annotationsMap
                                  ?? EnumPropertyModelCache.GetMap<EdgeAnnotationsOption, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Selected Annotations option</returns>
        public override EdgeAnnotationsOption SelectedOption
        {
            get
            {
                string activeOption = DriverExtensions.GetHiddenText(DriverExtensions.GetElement(SelectedOptionLocator)).Split('(')[0].TrimEnd();
                return activeOption.GetEnumValueByText<EdgeAnnotationsOption>(
                    string.Empty,
                    @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars");
            }
        }

        /// <summary>
        /// Verify that annotations option is selected
        /// </summary>
        /// <param name="option"> Annotations option </param>
        /// <returns> True if option is selected, false otherwise </returns>
        public override bool IsSelected(EdgeAnnotationsOption option)
        {
            if (option == EdgeAnnotationsOption.ShowAllAnnotations || option == EdgeAnnotationsOption.ShowMyAnnotations
                                                               || option == EdgeAnnotationsOption.HideAllAnnotations)
            {
                return
                    DriverExtensions.WaitForElement(By.XPath(this.AnnotationsMap[option].LocatorString))
                                    .GetAttribute("class")
                                    .Contains("checked");
            }

            throw new Exception($" Option {option} can't be selected.");
        }

        /// <summary>
        /// Get 'All Annotations' count from Annotation dropdown
        /// </summary>
        /// <returns> Count of All Annotations </returns>
        public int GetAllAnnotationCount() => this.GetAnnotationCount(EdgeAnnotationsOption.ShowAllAnnotations);

        /// <summary>
        /// get 'All My Annotations' count from Annotation dropdown
        /// </summary>
        /// <returns> Count of 'All My Annotations' </returns>
        public int GetMyAnnotationCount() => this.GetAnnotationCount(EdgeAnnotationsOption.ShowMyAnnotations);

        /// <summary>
        /// Verify that annotations option is displayed
        /// </summary>
        /// <param name="option"> Annotations option </param>
        /// <returns> True if the option is displayed, false otherwise </returns>
        public bool IsAnnotationsOptionDisplayed(EdgeAnnotationsOption option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.IsDisplayed(By.XPath(this.AnnotationsMap[option].LocatorString), 5);
        }

        /// <summary>
        /// Check if 'Stop Sharing My Annotations' from Annotation dropdown is active
        /// </summary>
        /// <returns>True if 'Stop Sharing My Annotations' is active, false otherwise</returns>
        public bool IsUnShareAllMyNotesLinkActive()
        {
            this.ExpandIfNotExpanded();

            By optionLocator =
                By.XPath(this.AnnotationsMap[EdgeAnnotationsOption.StopSharingMyAnnotations].LocatorString);
            return
                !DriverExtensions.WaitForElementDisplayed(optionLocator)
                                 .GetAttribute("class")
                                 .Contains("disabled");
        }

        /// <summary>
        /// Is annotations icon displayed
        /// </summary>
        /// <returns>True if accompanying icon is displayed</returns>
        public bool IsAnnotationsIconDisplayed(EdgeAnnotationsOption option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.IsDisplayed(By.XPath(this.AnnotationsMap[option].LocatorString), IconLocator);
        }

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(EdgeAnnotationsOption option)
        {
            DriverExtensions.Click(By.XPath(this.AnnotationsMap[option].LocatorString));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<EdgeAnnotationsOption> OptionsFromExpandedDropdown
        {
            get
            {
                return DriverExtensions.GetElements(DropdownOptionLocator)
                                       .Select(
                                           elem => EnumExtension.GetEnumValueByPropertyModel<EdgeAnnotationsOption, WebElementInfo>(
                                               elem.GetAttribute("id"),
                                               info => info.Id,
                                               string.Empty,
                                               @"Resources/EnumPropertyMaps/WestlawEdge/Toolbars"))
                                       .ToList();
            }
        }


        /// <summary>
        /// Returns option count
        /// </summary>
        /// <param name="option"></param>
        private int GetAnnotationCount(EdgeAnnotationsOption option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.WaitForElementDisplayed(By.XPath(this.AnnotationsMap[option].LocatorString)).Text.ConvertCountToInt();
        }


    }
}