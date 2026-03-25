namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using System.Collections.Generic;
    using System.Linq;

  using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Dialogs;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;
 
    /// <summary>
    /// The unable to find document page.
    /// </summary>
    public class PracticalLawDocumentPage : CommonDocumentPage
    {
        private static readonly By MaximizeNoteIconLocator = By.XPath("//a[contains(@title,'Maximize') and not(contains(@class, 'co_hideState'))]");
        private static readonly By InlineNotesLocator = By.XPath("//span[contains(@id,'co_noteHolder_') and .//div[@class='co_noteContainer']]");

        /// <summary>
        /// Get list of inline notes
        /// </summary>
        /// <returns> Inline notes list </returns>
        public new List<PracticalLawViewNoteDialog> GetInlineNotesList()
            => DriverExtensions.GetElements(InlineNotesLocator).Select(elem => new PracticalLawViewNoteDialog(elem)).ToList();

        /// <summary>
        /// Verifies that icon is in expected color
        /// </summary>
        public HighlightColor GetMaximizeNoteIconColor(int index = 0)
        {
            string elementClass = DriverExtensions.GetElements(MaximizeNoteIconLocator).ElementAt(index).GetAttribute("class");
            return elementClass.Contains("NoteIcon")
                       ? this.HighlightingColor.First(e => elementClass.Contains(e.Value.ClassName)).Key
                       : HighlightColor.None;
        }

        /// <summary>
        /// Is Minimize Note Displayed
        /// </summary>
        /// <returns></returns>
        public bool IsMinimizeNoteDisplayed() => DriverExtensions.IsDisplayed(MaximizeNoteIconLocator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public PracticalLawViewNoteDialog ClickMinimizeNoteIcon(int index = 0)
        {
            DriverExtensions.GetElements(MaximizeNoteIconLocator).ElementAt(index).CustomClick();
            return this.GetInlineNotesList().ElementAt(index);
        }
    }
}