namespace Framework.Common.UI.Products.Shared.Dialogs.Document.Notes
{
    using System.Drawing;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// ViewNoteDialog
    /// </summary>
    public class ViewNoteDialog : BaseModuleRegressionDialog
    {
        private static readonly By HeaderLocator = By.XPath(".//*[@class='co_noteHeader_createdBy']");

        private static readonly By NoteTextLocator = By.XPath(".//*[contains(@class,'co_viewNoteText')]");

        private static readonly By SharedIconLocator = By.XPath(".//div[@class='icon25 icon_people']");

        private EnumPropertyMapper<HighlightColor, WebElementInfo> highlightColor;

        /// <summary>
        /// Gets the Color to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<HighlightColor, WebElementInfo> HighlightingColor =>
            this.highlightColor = this.highlightColor
                                      ?? EnumPropertyModelCache.GetMap<HighlightColor, WebElementInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewNoteDialog"/> class. 
        /// </summary>
        /// <param name="container"> Container </param>
        public ViewNoteDialog(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Owner
        /// </summary>
        public string Owner => DriverExtensions.WaitForElement(this.Container, HeaderLocator).Text;

        /// <summary>
        /// NoteText
        /// </summary>
        public string NoteText => DriverExtensions.WaitForElement(this.Container, NoteTextLocator).Text;

        /// <summary>
        /// NoteColor
        /// </summary>
        public HighlightColor NoteColor =>
             this.HighlightingColor.First(e => e.Value.ClassName.Equals(DriverExtensions.GetAttribute("class", this.Container, By.XPath("./div")).Replace("co_notes ", ""))).Key;
        
        internal IWebElement Container { get; set; }

        /// <summary>
        /// Click on note
        /// </summary>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public EditNoteDialog ClickNote()
        {
            this.Container.ScrollToElement();
            this.Container.Click();
            return new EditNoteDialog();
        }

        /// <summary>
        /// Verify that shared icon is displayed for note
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSharedIconDisplayed() => DriverExtensions.IsDisplayed(SharedIconLocator);

        /// <summary>
        /// Get Note Location
        /// </summary>
        /// <returns> Location </returns>
        public Point GetNoteLocation() => this.Container.Location;

        /// <summary>
        /// Verify that dialog is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsNoteDisplayed() => this.Container.Displayed;

        /// <summary>
        /// Click on note
        /// </summary>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public EditNoteDialog ClickOnNoteText()
        {
            DriverExtensions.Click(NoteTextLocator);
            return new EditNoteDialog();
        }
    }
}
