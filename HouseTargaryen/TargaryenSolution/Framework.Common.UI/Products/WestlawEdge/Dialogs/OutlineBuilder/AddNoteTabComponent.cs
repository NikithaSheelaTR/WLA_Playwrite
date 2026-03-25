namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;

    /// <summary>
    /// Add note Tab Panel
    /// </summary>
    public class AddNoteTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_addNoteTab");
        private static readonly By ErrorMessageLocator = By.ClassName("InlineError-message");
        private static readonly By AddNoteTextAreaLocator = By.Id("coid_OutlineBuilderModalAddNote");

        /// <summary>
        /// Layout And Limits Tab Component
        /// </summary>
        protected override string TabName => "Add note";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Textbox changes current Outline's title
        /// </summary>
        public ITextbox NoteTextbox => new CustomTextbox(this.ComponentLocator, AddNoteTextAreaLocator);        

        /// <summary>
        /// Verify that error message is displayed
        /// </summary>
        public bool IsErrorMessageDisplayed(string title)
        {
            NoteTextbox.SetText(title);
            return DriverExtensions.IsDisplayed(ErrorMessageLocator);
        }
    }
}
