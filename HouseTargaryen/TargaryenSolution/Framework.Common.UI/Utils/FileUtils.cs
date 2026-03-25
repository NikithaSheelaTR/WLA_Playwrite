namespace Framework.Common.UI.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading;

    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Newtonsoft.Json;

    /// <summary>
    /// The file utilities.
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// Gets the download folder path.
        /// </summary>
        public static string DownloadFolderPath => Path.Combine(Environment.GetEnvironmentVariable("USERPROFILE"), "Downloads");

        /// <summary>
        /// Gets the Git download folder path.
        /// </summary>
        public static string GitDownloadFolderPath => Path.Combine("C:\\Users\\Administrator\\", "Downloads");

        /// <summary>
        /// The delete files in folder by mask.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <param name="mask">
        /// The mask.
        /// </param>
        public static void DeleteFilesInFolderByMask(string folderPath, string mask)
        {
            var info = new ProcessStartInfo
                           {
                               Arguments = $"/C del /s /q \"{folderPath}\\{mask}\"",
                               WindowStyle = ProcessWindowStyle.Hidden,
                               CreateNoWindow = true,
                               FileName = "cmd.exe"
                           };

            Process.Start(info);
        }

        /// <summary>
        /// Wait for file download.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        /// <param name="fileName">
        /// The file name.
        /// </param>
        public static void WaitForFileDownload(string folderPath, string fileName)
        {
            // Set timeout to the time you want to quit
            DateTime timeout = DateTime.Now.Add(TimeSpan.FromSeconds(60));

            while (!File.Exists(Path.Combine(folderPath, fileName)))
            {
                if (DateTime.Now > timeout)
                {
                    Console.WriteLine("Application timeout; app_boxed could not be created; try again");
                    break;
                }

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }

            var watcher = new FileSystemWatcher(folderPath, fileName);
            watcher.Changed += (sender, e) =>
                {
                    try
                    {
                        Thread.Sleep(100); // hack for timing issues
                        File.Open(e.FullPath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    }
                    catch (IOException)
                    {
                        // we couldn't open the file
                        // this is probably because the copy operation is not done
                        // just swallow the exception
                    }

                    // now we have a handle to the file
                };
        }

        /// <summary>
        /// The file contains lines.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <param name="lines">
        /// The lines.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool FileContainsLines(string filePath, List<string> lines)
        {
            try
            {
                string fileText = File.ReadAllText(filePath);
                return lines.All(line => fileText.Contains(line));
            }
            catch (ArgumentException)
            {
                Console.Write("File content can not be verified, file path is not specified");
            }
            catch (PathTooLongException)
            {
                Console.Write("The specified path, file name, or both exceed the system-defined maximum length.");
            }
            catch (UnauthorizedAccessException)
            {
                Console.Write("Path specified a file that is read-only.");
            }
            catch (NotSupportedException)
            {
                Console.Write("Path is in an invalid format.");
            }
            catch (FileNotFoundException)
            {
                Console.Write("The file specified in path was not found.");
            }
            catch (IOException)
            {
                Console.Write("An I/O error occurred while opening the file.");
            }

            return false;
        }

        /// <summary>
        /// The rename westlaw files in downloads folder.
        /// </summary>
        /// <param name="westlawPrefix">
        /// The westlaw prefix.
        /// </param>
        public static void RenameWestlawFilesInDownloadsFolder(string westlawPrefix = "Westlaw")
            => Directory.GetFiles(GitDownloadFolderPath)
               .Where(fileName => fileName.Contains(westlawPrefix))
               .Select(file => new FileInfo(file))
               .Where(fileInfo => fileInfo.CreationTime.Day.Equals(DateTime.Now.Day) && fileInfo.Name.Contains(westlawPrefix)).ToList()
               .ForEach(fileInfo => SafeMethodExecutor.Execute(() => File.Move(fileInfo.FullName, Path.Combine(fileInfo.DirectoryName, StringExtensions.CreateRandomName("Downloaded File") + fileInfo.Name.Replace(westlawPrefix, string.Empty)))));

        /// <summary>
        /// The get all lines from file.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetAllLinesFromFile(string path) => File.ReadAllLines(path).ToList();

        /// <summary>
        /// Read json file and return deserialized model of json file
        /// </summary>
        /// <typeparam name="T">Json model</typeparam>
        /// <param name="sourcePath"> The file path </param>
        /// <returns> Deserialized file. </returns>
        public static T GetJsonData<T>(string sourcePath)
        {
            string content;
            using (var reader = new StreamReader(sourcePath))
            {
                content = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}