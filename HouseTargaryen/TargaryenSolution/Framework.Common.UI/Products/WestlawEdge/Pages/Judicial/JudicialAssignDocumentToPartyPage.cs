namespace Framework.Common.UI.Products.WestlawEdge.Pages.Judicial
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial;
    using Framework.Common.UI.Products.WestlawEdge.Items.Judicial;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Assign Document To Party Page
    /// </summary>
    public class JudicialAssignDocumentToPartyPage : JudicialBaseUploadPage
    {
        private static readonly By UploadedFileLocator =  By.XPath("//ul[@class='DA-JudicialFilesList']//li[@draggable]");

        /// <summary>
        /// Assign document to party component
        /// </summary>
        public AssignDocumentsToPartiesComponent AssignDocumentsToPartiesComponent =>
            new AssignDocumentsToPartiesComponent();

        /// <summary>
        /// Uploaded files items
        /// </summary>
        public List<JudicialUploadedDocumentItem> UploadedFileItems =>
            DriverExtensions.GetElements(UploadedFileLocator).Select(el => new JudicialUploadedDocumentItem(el))
                            .ToList();
    }
}
