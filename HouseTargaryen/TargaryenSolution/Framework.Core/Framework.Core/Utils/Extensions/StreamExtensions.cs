namespace Framework.Core.Utils.Extensions
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Extends Stream classes.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Downloads a resource from the remote URI and stores it in the specified file.
        /// </summary>
        /// <param name="resourceUri">URI to the remote resource.</param>
        /// <param name="outputFileName">The name of the file to copy stream to.</param>
        /// <param name="bufferSize">Size of the buffer to copy at once. Defaults to 32KiB.</param>
        public static void DownloadRemoteResource(string resourceUri, string outputFileName, int bufferSize = 2 << 14)
        {
            if (string.IsNullOrWhiteSpace(resourceUri))
            {
                throw new ArgumentException(
                    string.Format("'{0}' is not a valid remote URI name.", resourceUri),
                    "resourceUri");
            }

            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                throw new ArgumentException(
                    string.Format("'{0}' is not a valid file name.", outputFileName),
                    "outputFileName");
            }

            resourceUri = resourceUri.ToLower(CultureInfo.InvariantCulture);

            if (resourceUri.StartsWith("http"))
            {
                try
                {
                    Stream response = WebRequest.Create(resourceUri).GetResponse().GetResponseStream();

                    if (response != null)
                    {
                        response.FastCopyToFile(outputFileName);
                        response.Close();
                    }
                }
                catch (Exception e)
                {
                    throw new InvalidOperationException(
                        string.Format("Unable to download image '{0}' to file.", resourceUri),
                        e);
                }
            }
        }

        /// <summary>
        /// Performs fast copying of data from the specified stream to the specified file.
        /// </summary>
        /// <param name="inputStream">The stream whose data to copy.</param>
        /// <param name="outputFileName">The name of the file to copy stream to.</param>
        /// <param name="bufferSize">Size of the buffer to copy at once. Defaults to 32KiB.</param>
        public static void FastCopyToFile(this Stream inputStream, string outputFileName, int bufferSize = 2 << 14)
        {
            if (inputStream == null)
            {
                throw new ArgumentNullException("inputStream");
            }

            if (string.IsNullOrWhiteSpace(outputFileName))
            {
                throw new ArgumentException(
                    string.Format("'{0}' is not a valid file name.", outputFileName),
                    "outputFileName");
            }

            var buffer = new byte[bufferSize];

            using (FileStream fs = File.Create(outputFileName))
            {
                int read;

                while ((read = inputStream.Read(buffer, 0, bufferSize)) > 0)
                {
                    fs.Write(buffer, 0, read);
                }

                fs.Close();
            }
        }
    }
}