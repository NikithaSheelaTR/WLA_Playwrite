namespace Framework.Common.UI.Utils.Screenshot
{
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    using Framework.Core.CommonTypes.Enums.TestCapture;

    using OpenQA.Selenium;

    /// <summary>
    /// ImageFileTypeExtensions
    /// </summary>
    public static class ImageFileTypeExtensions
    {
        private static readonly Dictionary<ImageFormat, ScreenshotImageFormat> Map =
            new Dictionary<ImageFormat, ScreenshotImageFormat> { { ImageFormat.Jpeg, ScreenshotImageFormat.Jpeg } };

        /// <summary>
        /// GetScreenshotImageFormat
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The <see cref="ScreenshotImageFormat"/>.</returns>
        public static ScreenshotImageFormat GetScreenshotImageFormat(this ImageFileType source)
        {
            return Map[source.GetFormat()];
        }
    }
}
