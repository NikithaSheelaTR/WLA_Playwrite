namespace Framework.Core.CommonTypes.Enums.TestCapture
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Imaging;

    /// <summary>
    /// ImageFileType enum equivalent
    /// </summary>
    public class ImageFileType
    {
        /// <summary>
        /// JPEG - Joint Photographic Experts Group
        /// </summary>
        public static ImageFileType JPEG = new ImageFileType("JPEG", "jpg", "Joint Photographic Experts Group", ImageFormat.Jpeg);

        /// <summary>
        /// TIFF - Tagged Image File Format
        /// </summary>
        public static ImageFileType TIFF = new ImageFileType("TIFF", "tif", "Tagged Image File Format", ImageFormat.Tiff);

        /// <summary>
        /// RAW - Raw image format
        /// </summary>
        public static ImageFileType RAW = new ImageFileType("RAW", "raw", "Raw image format", null);

        /// <summary>
        /// GIF - Graphics Interchange Format
        /// </summary>
        public static ImageFileType GIF = new ImageFileType("GIF", "gif", "Graphics Interchange Format", ImageFormat.Gif);

        /// <summary>
        /// BMP - Bitmap
        /// </summary>
        public static ImageFileType BMP = new ImageFileType("BMP", "bmp", "Bitmap", ImageFormat.Bmp);

        /// <summary>
        /// PNG - Portable Network Graphics
        /// </summary>
        public static ImageFileType PNG = new ImageFileType("PNG", "png", "Portable Network Graphics", ImageFormat.Png);

        /// <summary>
        /// Collection containing all supported image filetypes
        /// </summary>
        public static List<ImageFileType> Values = new List<ImageFileType>
        {
            JPEG,
            TIFF,
            RAW,
            GIF,
            BMP,
            PNG
        };

        private readonly string _name;
        private readonly string _description;
        private readonly string _enumId;
        private readonly ImageFormat _format;

        /// <summary>
        /// Returns the enum id of the ImageFileType, which is represented by the name of the enum declaration
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="name">The name of the ImageFileType</param>
        /// <param name="description">A description of the ImageFileType</param>
        /// <param name="format"></param>
        private ImageFileType(string enumId, string name, string description, ImageFormat format)
        {
            this._enumId = enumId;
            this._name = name;
            this._description = description;
            this._format = format;
        }

        /// <summary>
        /// Returns the enum id of the ImageFileType, which is represented by the name of the enum declaration
        /// </summary>
        /// <returns>The enum id of the ImageFileType, which is represented by the name of the enum declaration</returns>
        public string GetEnumId()
        {
            return this._enumId;
        }

        /// <summary>
        /// Returns the name of the ImageFileType
        /// </summary>
        /// <returns>The name of the ImageFileType</returns>
        public String GetName()
        {
            return this._name;
        }

        /// <summary>
        /// Returns the file extension of the ImageFileType
        /// </summary>
        /// <returns>The file extension of the ImageFileType</returns>
        public String GetExtension()
        {
            return "." + this._name;
        }

        /// <summary>
        /// Returns a description of the ImageFileType format
        /// </summary>
        /// <returns>A description of the ImageFileType format</returns>
        public String GetDescription()
        {
            return this._description;
        }

        /// <summary>
        /// Returns a description of the ImageFileType format
        /// </summary>
        /// <returns>A description of the ImageFileType format</returns>
        public ImageFormat GetFormat()
        {
            return this._format;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static ImageFileType FromFilename(string filename)
        {
            // Check if the filename argument is null
            if (filename == null)
            {
                throw new NullReferenceException("The filename argument cannot be null.");
            }

            // Loop through all of the ImageFileTypes and check for matches
            foreach (var fileType in Values)
            {
                string fileExtension = filename.Substring(filename.LastIndexOf('.'));
                if (fileExtension.Contains(fileType.GetExtension()))
                {
                    return fileType;
                }
            }
            throw new ArgumentException("Cannot be parsed into a ImageFileType: '" + filename + "'.");
        }
    }
}
