namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The web service extensions.
    /// </summary>
    public static class WebServiceExtensions
    {
        /// <summary>
        /// The get lines from response.
        /// </summary>
        /// <param name="responseStream"> The response stream. </param>
        /// <returns> The List. </returns>
        public static List<string> GetLinesFromResponse(Stream responseStream)
        {
            var list = new List<string>();
            using (var streamReader = new StreamReader(responseStream))
            {
                while (!streamReader.EndOfStream)
                {
                    list.Add(streamReader.ReadLine().Trim());
                }

                streamReader.Close();
                responseStream.Close();
            }

            return list;
        }

        /// <summary>
        /// Get the string out of the response's stream
        /// </summary>
        /// <param name="response">HttpWebResponse to get the stream in the response</param>
        /// <returns>A string</returns>
        public static string GetStringFromResponse(HttpWebResponse response)
        {
            return WebServiceExtensions.GetStringFromResponse(response.GetResponseStream());
        }

        /// <summary>
        /// Convert a stream into a string
        /// Author: PTQ 04.2008
        /// </summary>
        /// <param name="responseStream">Stream to convert into a string</param>
        /// <returns>A string</returns>
        public static string GetStringFromResponse(Stream responseStream)
        {
            // Open the stream using a StreamReader for easy access.    
            var reader = new StreamReader(responseStream);
            string response = reader.ReadToEnd();

            // Cleanup the streams.    
            reader.Close();
            responseStream.Close();

            return response;
        }
    }
}