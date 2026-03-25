namespace Framework.Core.Utils.Verification
{
    using System.IO;

    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Custom assertions.
    /// </summary>
    public class Assertion
    {
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