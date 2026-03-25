namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the Annotations dropdown on the toolbar
    /// </summary>
    public class AnnotationsDropdown : BaseAnnotationsDropdown<AnnotationsOption>
    {
        private static readonly By DropDownOptionLocator = By.XPath("//li[@id='co_docToolbarAddNoteWidget']//div[@class='co_dropdownBoxContentRight']/ul/li | //li[@id='co_docToolbarAddNoteWidget']//ul/li");

        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Selected Annotations option</returns>
        public override AnnotationsOption SelectedOption
        {
            get
            {
                string checkedElementId =
                    DriverExtensions.GetElements(DropDownOptionLocator)
                                    .FirstOrDefault(elem => elem.GetAttribute("class").Equals("checked"))?
                                    .GetAttribute("id");
                return checkedElementId.GetEnumValueByPropertyModel<AnnotationsOption, WebElementInfo>(info => info.Id);
            }
        }

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        protected EnumPropertyMapper<AnnotationsOption, WebElementInfo> AnnotationsMap
            =>
                this.annotationsMap =
                    this.annotationsMap ?? EnumPropertyModelCache.GetMap<AnnotationsOption, WebElementInfo>();

        /// <summary>
        /// Get 'All Annotations' count from Annotation dropdown
        /// </summary>
        /// <returns> Count of All Annotations </returns>
        public int GetAllAnnotationCount() => this.GetAnnotationCount(AnnotationsOption.ShowAllAnnotations);

        /// <summary>
        /// get 'All My Annotations' count from Annotation dropdown
        /// </summary>
        /// <returns> Count of 'All My Annotations' </returns>
        public int GetMyAnnotationCount() => this.GetAnnotationCount(AnnotationsOption.ShowAllMyAnnotations);

        /// <summary>
        /// Verify that annotations option is displayed
        /// </summary>
        /// <param name="option"> Annotations option </param>
        /// <returns> True if the option is displayed, false otherwise </returns>
        public bool IsAnnotationsOptionDisplayed(AnnotationsOption option)
        {
            this.ExpandIfNotExpanded();

            return DriverExtensions.IsDisplayed(By.XPath(this.AnnotationsMap[option].LocatorString), 5);
        }

        /// <summary>
        /// Verify that annotations option is selected
        /// </summary>
        /// <param name="option"> Annotations option </param>
        /// <returns> True if option is selected, false otherwise </returns>
        public override bool IsSelected(AnnotationsOption option)
        {
            if (option == AnnotationsOption.ShowAllAnnotations || option == AnnotationsOption.ShowAllMyAnnotations
                || option == AnnotationsOption.HideAllAnnotations)
            {
                return
                    DriverExtensions.WaitForElement(By.Id(this.AnnotationsMap[option].Id))
                                    .GetAttribute("class")
                                    .Equals("checked");
            }

            throw new Exception($" Option {option} can't be selected.");
        }

        /// <summary>
        /// Check if an option from Annotation dropdown is active
        /// </summary>
        /// <returns>True if option is active, false otherwise</returns>
        public bool IsOptionActive(AnnotationsOption option)
        {
            this.ExpandIfNotExpanded();

            By optionLocator =
                By.Id(this.AnnotationsMap[option].Id);
            return
                !DriverExtensions.WaitForElementDisplayed(optionLocator)
                                 .GetAttribute("class")
                                 .Contains("disabled");
        }

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(AnnotationsOption option)
        {
            DriverExtensions.Click(By.Id(this.AnnotationsMap[option].Id));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<AnnotationsOption> OptionsFromExpandedDropdown
        {
            get
            {
                return
                    DriverExtensions.GetElements(DropDownOptionLocator)
                                    .Select(
                                        elem =>
                                            elem.GetAttribute("id")
                                                .GetEnumValueByPropertyModel<AnnotationsOption, WebElementInfo>(
                                                    info => info.Id))
                                    .ToList();
            }
        }

        private int GetAnnotationCount(AnnotationsOption option)
        {
            this.ExpandIfNotExpanded();

            By locator = By.XPath(this.AnnotationsMap[option].LocatorString);
            return DriverExtensions.WaitForElementDisplayed(locator).Text.ConvertCountToInt();
        }
    }
}