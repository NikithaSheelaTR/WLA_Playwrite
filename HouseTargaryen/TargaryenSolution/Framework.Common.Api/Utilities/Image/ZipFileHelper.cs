namespace Framework.Common.Api.Utilities.Image
{
    using System.IO;

    using Ionic.Zip;

    /// <summary>
    /// The zip file helper.
    /// </summary>
    public static class ZipFileHelper
    {
        /// <summary>
        /// Extracts a zipped file.
        /// </summary>
        /// <param name="directoryName">The name of the target directory.</param>
        /// <param name="zipFileName">The name of a ZIP file in the target directory.</param>
        public static void ExtractZipFile(string directoryName, string zipFileName)
        {
            string path = Path.Combine(directoryName, zipFileName);

            using (ZipFile zip = ZipFile.Read(path))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(directoryName, ExtractExistingFileAction.OverwriteSilently);
                }
            }
        }
    }
}