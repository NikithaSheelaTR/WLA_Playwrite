namespace Framework.Common.UI.Products.Shared.Components.FindAndPrint
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// ResultOptionsComponent
    /// </summary>
    public class DocumentOptionsComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("documentOptions");

        private static readonly By FullTextDocumentCheckboxLocator = By.Id("fullTextDocumentsOption");

        private static readonly By FullTextDocumentRadiobuttonLocator = By.Id("co_casesFullTextDocuments");

        private static readonly By SubstituteCheckboxLocator = By.Id("co_substituteRptrImages");

        private static readonly By CasesOpinionRadiobuttonLocator = By.Id("co_casesCaseOpinionOnly");

        private static readonly By SingleFolderReporterImageLocator = By.Id("co_mergeRptrImages");

        /// <summary>
        /// FullTextDocumentsCheckbox
        /// </summary>
        public ICheckBox FullTextDocumentsCheckbox => new CheckBox(FullTextDocumentCheckboxLocator);

        /// <summary>
        /// SubstituteCheckBox
        /// </summary>
        public ICheckBox SubstituteCheckBox => new CheckBox(SubstituteCheckboxLocator);

        /// <summary>
        /// Create Single reporter image folder checkbox
        /// </summary>
        public ICheckBox SingleFolderReporterImageCheckBox => new CheckBox(SingleFolderReporterImageLocator);

        /// <summary>
        /// FullTextDocumentsRadiobutton
        /// </summary>
        public IButton FullTextDocumentsRadiobutton => new Button(FullTextDocumentRadiobuttonLocator);

        /// <summary>
        /// CasesOpinionRadiobutton
        /// </summary>
        public IButton CasesOpinionRadiobutton => new Button(CasesOpinionRadiobuttonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
