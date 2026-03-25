namespace Framework.Core.Utils
{
    using System;
    using System.IO;

    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Provides file related utility methods.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// 
        /// </summary>
        public static string RootDir = Directory.GetCurrentDirectory();

        /// <summary>
        /// Determines whether the contents of two files are equal.
        /// </summary>
        /// <param name="filePath1">A path to a file.</param>
        /// <param name="filePath2">A path to a file.</param>
        /// <returns>An indication whether the contents of two files are equal.</returns>
        public static bool ContentEquals(string filePath1, string filePath2)
        {
            // validate input
            if (filePath1 == null)
            {
                throw new ArgumentNullException("filePath1", "The first file path is required.");
            }
            if (filePath2 == null)
            {
                throw new ArgumentNullException("filePath2", "The second file path is required.");
            }
            if (!File.Exists(filePath1))
            {
                throw new FileNotFoundException("filePath1 not found.", filePath1);
            }
            if (!File.Exists(filePath2))
            {
                throw new FileNotFoundException("filePath2 not found.", filePath2);
            }

            // determine if the same file was referenced twice
            if (filePath1 == filePath2)
            {
                return true;
            }

            // open the two files
            int fileByte1;
            int fileByte2;
            using (FileStream fileStream1 = new FileStream(filePath1, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), fileStream2 = new FileStream(filePath2, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                // check the file sizes, and if not same, the files are not the same
                if (fileStream1.Length != fileStream2.Length)
                {
                    return false;
                }

                // read and compare a byte from each file until either a non-matching set of bytes is found or until the end of file is reached
                do
                {
                    fileByte1 = fileStream1.ReadByte();
                    fileByte2 = fileStream2.ReadByte();
                }
                while ((fileByte1 == fileByte2) && (fileByte1 != -1));
            }

            // fileByte1 is equal to fileByte2 at this point only if the files are the same
            return ((fileByte1 - fileByte2) == 0);
        }

        /// <summary>
        /// Asserts that the file with the specified name exists and is accessible.
        /// </summary>
        /// <param name="fileName">The relative or absolution path to the file.</param>
        public static void FileExists(string fileName)
        {
            if (!File.Exists(fileName))
            {
                if (string.IsNullOrWhiteSpace(fileName))
                {
                    fileName = "NULL";
                }
                else
                {
                    SafeMethodExecutor.Execute(() => fileName = new FileInfo(fileName).FullName);
                }

                throw new FileNotFoundException("The file name is invalid or the file does not exist.", fileName);
            }
        }
    }
}