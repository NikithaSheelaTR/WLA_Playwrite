namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;
    using OpenQA.Selenium;

    /// <summary>
    /// Edit document details dialog
    /// </summary>
    public class EditDocumentDetailsDialog: BaseModuleRegressionDialog
    {
        private static readonly By ApplyChangesButtonLocator = By.XPath("//button[contains(text(), 'Apply changes')]");
        private static readonly By AppellateLevelOutcomeContainerLocator = By.XPath("//div[@class='DA-Category']//legend[text()='Appellate level outcome:']");
        private static readonly By MotionTypeContainerLocator = By.XPath("//div[@class='DA-Category']//legend[text()='Motion type:']");
        private static readonly By MovantAtTrialLevelContainerLocator = By.XPath("//div[@class='DA-Category']//legend[text()='Movant at trial level:']");
        private static readonly By ReliefRequestedInDocumentContainerLocator = By.XPath("//div[@class='DA-Category']//legend[text()='Relief requested in document:']");
        private static readonly By TrialLevelOutcomeContainerLocator = By.XPath("//div[@class='DA-Category']//legend[text()='Trial level outcome:']");
        private static readonly By CategoryItemLocator = By.XPath(".//following-sibling::div");
        private static readonly By CancelButtonLocator = By.XPath("//button[contains(text(), 'Cancel')]");
        
        /// <summary>
        /// The Court level dropdown.
        /// </summary>
        public QuickCheckDocumentInformationCourtLevelDropdown CourtLevelDropdown =>
            new QuickCheckDocumentInformationCourtLevelDropdown();

        /// <summary>
        /// Motion types
        /// </summary>
        public ItemsCollection<CategoryItem> AppellateLevelOutcomes =>
            new ItemsCollection<CategoryItem>(AppellateLevelOutcomeContainerLocator, CategoryItemLocator);

        /// <summary>
        /// Motion types
        /// </summary>
        public ItemsCollection<CategoryItem> MotionTypes =>
            new ItemsCollection<CategoryItem>(MotionTypeContainerLocator, CategoryItemLocator);

        /// <summary>
        /// Movant at trial levels
        /// </summary>
        public ItemsCollection<CategoryItem> MovantAtTrialLevels =>
            new ItemsCollection<CategoryItem>(MovantAtTrialLevelContainerLocator, CategoryItemLocator);

        /// <summary>
        /// Reliefs requested in document
        /// </summary>
        public ItemsCollection<CategoryItem> ReliefsRequestedInDocument =>
            new ItemsCollection<CategoryItem>(ReliefRequestedInDocumentContainerLocator, CategoryItemLocator);

        /// <summary>
        /// Trial level outcomes
        /// </summary>
        public ItemsCollection<CategoryItem> TrialLevelOutcomes =>
            new ItemsCollection<CategoryItem>(TrialLevelOutcomeContainerLocator, CategoryItemLocator);

        /// <summary>
        /// Apply changes button
        /// </summary>
        public IButton ApplyChangesButton => new JsClickButton(ApplyChangesButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(CancelButtonLocator);
    }
}
