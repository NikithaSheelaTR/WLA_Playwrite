namespace Framework.Common.UI.Utils
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Work with window object
    /// </summary>
    public static class WindowUtils
    {
        /// <summary>
        /// Retrieves guid from URL
        /// </summary>
        /// <returns>Document Guid</returns>
        public static string GetDocumentGuid() => (string)DriverExtensions.ExecuteScript("return window[\"Server/ViewModelData\"].DocumentGuid");

        /// <summary>
        /// The get y offset.
        /// </summary>
        /// <returns> Y offset </returns>
        public static long GetYOffset() => (long)DriverExtensions.ExecuteScript("return window.pageYOffset;");
    }
}
