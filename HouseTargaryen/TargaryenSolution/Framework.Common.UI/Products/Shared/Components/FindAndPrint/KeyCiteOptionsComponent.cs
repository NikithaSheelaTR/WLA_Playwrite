namespace Framework.Common.UI.Products.Shared.Components.FindAndPrint
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;

    using OpenQA.Selenium;

    /// <summary>
    /// KeyCiteOptionsComponent
    /// </summary>
    public class KeyCiteOptionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator
            = By.XPath("//div[@class='co_search_advancedSearchFieldBox' and ./ul[@id='keyCiteOptions']]");

        private static readonly By ListOfAllHistoryCheckboxLocator = By.Id("co_allHistoryTreatmentsOption");

        private static readonly By ListOfAllNegativeCheckboxLocator = By.Id("co_allNegativeTreatmentsOption");

        private static readonly By ListOfTheFirstFiveHundredsCheckboxLocator = By.Id("co_citingReferencesOption");

        private static readonly By ExcludeRelatedFilingsLocator = By.Id("excludeRelatedFilings");

        private static readonly By OutOfPlanWarningLocator = By.Id("outOfPlanWarning");

        /// <summary>
        /// ListOfAllHistoryCheckbox
        /// </summary>
        public ICheckBox ListOfAllHistoryCheckbox => new CheckBox(ListOfAllHistoryCheckboxLocator);

        /// <summary>
        /// ListOfAllNegativeCheckbox
        /// </summary>
        public ICheckBox ListOfAllNegativeCheckbox => new CheckBox(ListOfAllNegativeCheckboxLocator);

        /// <summary>
        /// ListOfAllFiveHundredsCheckbox
        /// </summary>
        public ICheckBox ListOfAllFiveHundredsCheckbox => new CheckBox(ListOfTheFirstFiveHundredsCheckboxLocator);

        /// <summary>
        /// excludeRelatedFilingsCheckbox
        /// </summary>
        public ICheckBox ExcludeRelatedFilingsCheckbox => new CheckBox(ExcludeRelatedFilingsLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Keycite warning component
        /// </summary>
        public IInfoBox OutOfPlanKeyciteWarningComponent => new InfoBox(OutOfPlanWarningLocator);
    }
}
