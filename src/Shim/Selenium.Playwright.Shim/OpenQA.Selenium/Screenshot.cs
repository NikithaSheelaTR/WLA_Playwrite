using System;
using System.IO;

namespace OpenQA.Selenium
{
    public class Screenshot
    {
        private readonly byte[] _byteArray;

        public Screenshot(byte[] screenshotData)
        {
            _byteArray = screenshotData;
        }

        public string AsBase64EncodedString => Convert.ToBase64String(_byteArray);

        public byte[] AsByteArray => _byteArray;

        public void SaveAsFile(string fileName, ScreenshotImageFormat format)
        {
            var directory = Path.GetDirectoryName(fileName);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            File.WriteAllBytes(fileName, _byteArray);
        }
    }

    public enum ScreenshotImageFormat
    {
        Png,
        Jpeg,
        Gif,
        Tiff,
        Bmp
    }
}
