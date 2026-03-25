namespace Framework.Core.Utils.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;

    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;

    /// <summary>
    /// The bitmap extensions.
    /// </summary>
    public static class BitmapExtensions
    {
        /// <summary>
        /// Compare 2 pictures (only brightness)
        /// </summary>
        /// <param name="firstFile">firstFile</param>
        /// <param name="secondFile">secondFile</param>
        /// <param name="epsilon">error</param>
        /// <returns>true if equals, false otherwise</returns>
        public static bool AreImagesEqualByPixelsArgbColor(this Bitmap firstFile, Bitmap secondFile, int epsilon = 5)
        {
            if (firstFile.Height != secondFile.Height || firstFile.Width != secondFile.Width)
            {
                return false;
            }
            
            for (int i = 0; i < firstFile.Width; i++)
            {
                for (int j = 0; j < firstFile.Height; j++)
                {
                    Color firstImagePixel = firstFile.GetPixel(i, j);
                    Color secondImagePixel = secondFile.GetPixel(i, j);

                    if (Math.Abs((int)firstImagePixel.A - (int)secondImagePixel.A) > epsilon)
                    {
                        return false;
                    }

                    if (Math.Abs((int)firstImagePixel.B - (int)secondImagePixel.B) > epsilon)
                    {
                        return false;
                    }

                    if (Math.Abs((int)firstImagePixel.R - (int)secondImagePixel.R) > epsilon)
                    {
                        return false;
                    }

                    if (Math.Abs((int)firstImagePixel.G - (int)secondImagePixel.G) > epsilon)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        /// <summary>Compares two images as byte arrays.</summary>
        /// <param name="firstImage">The first image.</param>
        /// <param name="secondImage">The second image.</param>
        /// <returns>The result of comparison.</returns>
        public static bool AreImagesEqual(Bitmap firstImage, Bitmap secondImage)
        {
            byte[] firstSerialisedImage = BitmapExtensions.BitmapToArray(firstImage),
                   secondSerialisedImage = BitmapExtensions.BitmapToArray(secondImage);

            return ArrayExtensions.AreEqual(firstSerialisedImage, secondSerialisedImage);
        }

        /// <summary>
        /// Compares two images by pixels
        /// </summary>
        /// <param name="expected">Expected image.</param>
        /// <param name="actual">Actual image.</param>
        /// <returns>The result of comparison.</returns>
        public static bool AreImagesEqualByPixels(Bitmap expected, Bitmap actual)
        {
            if (expected.Size != actual.Size)
            {
                return false;
            }

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    if (expected.GetPixel(i, j) != actual.GetPixel(i, j))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Transforms a bitmap to a byte array.
        /// </summary>
        /// <param name="image">A bitmap object to transform.</param>
        /// <returns>The byte array that corresponds to the image.</returns>
        public static byte[] BitmapToArray(this Bitmap image)
        {
            var result = new byte[0];

            SafeMethodExecutor.Execute(
                () =>
                    {
                        using (var ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Png);
                            result = ms.ToArray();
                            ms.Close();
                        }
                    });

            return result;
        }

        /// <summary>
        /// Converts an image defined by its URI to a bitmap using JPEG decoder.
        /// </summary>
        /// <param name="imageName">Image URI.</param>
        /// <returns>The decoded bitmap object.</returns>
        public static Bitmap ConvertJpegImageToBitmap(string imageName)
        {
            return BitmapExtensions.ConvertToBitmap<PngBitmapEncoder>(imageName);
        }

        /// <summary>
        /// Converts an image defined by its URI to a bitmap using TIFF decoder.
        /// </summary>
        /// <param name="imageName">Image URI.</param>
        /// <returns>The decoded bitmap object.</returns>
        public static Bitmap ConvertTiffImageToBitmap(string imageName)
        {
            return BitmapExtensions.ConvertToBitmap<PngBitmapEncoder>(imageName);
        }

        /// <summary>
        /// Converts an image defined by its URI to the bitmap using the specified encoder.
        /// </summary>
        /// <typeparam name="TEncoder">Encoder to use to generate the converted image.</typeparam>
        /// <param name="imageName">Image URI.</param>
        /// <returns>The decoded bitmap object.</returns>
        public static Bitmap ConvertToBitmap<TEncoder>(string imageName) where TEncoder : BitmapEncoder
        {
            Bitmap result;

            try
            {
                using (var imageStreamSource = new FileStream(imageName, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    BitmapDecoder decoder = BitmapDecoder.Create(
                        imageStreamSource,
                        BitmapCreateOptions.PreservePixelFormat,
                        BitmapCacheOption.Default);
                    BitmapSource bitmapSource = decoder.Frames[0];

                    result = BitmapExtensions.ToBitmap<TEncoder>(bitmapSource);
                }
            }
            catch (Exception e)
            {
                throw new BadImageFormatException(
                    string.Format(
                        "Unable to convert image '{0}' to bitmap, using {1} encoder.",
                        imageName,
                        typeof(TEncoder).Name),
                    e);
            }

            return result;
        }

        /// <summary>Retrieves a bitmap object from URI.</summary>
        /// <param name="imageName">The remote or local URI that points to an image.</param>
        /// <returns>The <see cref="Bitmap"/>.</returns>
        public static Bitmap RemoteUriToBitmap(string imageName)
        {
            if (string.IsNullOrWhiteSpace(imageName))
            {
                throw new ArgumentException("Image URI is invalid.", "imageName");
            }

            string[] splitName = imageName.Split('/', '.');
            string newImageName = splitName[splitName.Length - 2] + "." + splitName[splitName.Length - 1];

            StreamExtensions.DownloadRemoteResource(imageName, newImageName);

            return BitmapExtensions.ConvertToBitmap<PngBitmapEncoder>("./" + newImageName);
        }

        private static Bitmap ToBitmap<TEncoder>(BitmapSource bitmapSource) where TEncoder : BitmapEncoder
        {
            Bitmap bitmap;

            using (var stream = new MemoryStream())
            {
                var encoder = (TEncoder)Activator.CreateInstance(typeof(TEncoder));

                encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                encoder.Save(stream);
                bitmap = new Bitmap(stream);
                stream.Close();
            }

            return bitmap;
        }
    }
}