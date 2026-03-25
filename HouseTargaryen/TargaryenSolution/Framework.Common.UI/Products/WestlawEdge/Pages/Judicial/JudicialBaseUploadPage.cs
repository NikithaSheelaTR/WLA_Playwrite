namespace Framework.Common.UI.Products.WestlawEdge.Pages.Judicial
{
    using System.IO;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial base upload page
    /// </summary>
    public class JudicialBaseUploadPage : QuickCheckBasePage
    {
        private static readonly By FileUploadInput = By.XPath("//input[@name='file']");

        private static readonly By FileUploadButtonLocator = By.XPath("//button[contains(@id, 'documentUploadButton')]");

        /// <summary>
        /// Choose files button
        /// </summary>
        public IButton FileUploadButton { get; } = new Button(FileUploadButtonLocator);

        /// <summary>
        /// Upload documents to Judicial
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <param name="fileNames">file names</param>
        /// <returns>JudicialSelectPartyPage</returns>
        public JudicialAssignDocumentToPartyPage UploadFiles(string filePath, params string[] fileNames)
        {
            this.UploadFiles(fileNames.Select(el => Path.Combine(filePath, el)).ToArray());
            return new JudicialAssignDocumentToPartyPage();
        }

        /// <summary>
        /// Upload documents to Judicial
        /// </summary>
        /// <param name="filePaths">file path</param>
        /// <returns>Judicial Select Party Page</returns>
        public JudicialAssignDocumentToPartyPage UploadFiles(params string[] filePaths)
        {
            foreach (string file in filePaths)
            {
                DriverExtensions.GetElement(FileUploadInput).SendKeys(@file);
            }
            return new JudicialAssignDocumentToPartyPage();
        }
    }
}
