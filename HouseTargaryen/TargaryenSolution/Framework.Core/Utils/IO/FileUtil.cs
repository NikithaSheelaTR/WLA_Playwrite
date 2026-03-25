namespace Framework.Core.Utils.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Framework.Core.Utils.Execution;
    using ICSharpCode.SharpZipLib.Core;
    using ICSharpCode.SharpZipLib.Zip;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;

    /// <summary>
    /// This is a static class used to help work with files
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        /// Creates file of the specified size and returns file path.
        /// </summary>
        /// <param name="size">Size of file in bytes.</param>
        /// <param name="testContext">Test context.</param>
        public static string CreateFileOfSize(int size, TestContext testContext)
        {
            char[] chq =
                {
                    'A',
                    'B',
                    'C',
                    'D',
                    'E',
                    'F',
                    'G',
                    'K',
                    'L',
                    'M',
                    'N',
                    'O',
                    'P',
                    'R',
                    'S',
                    'T',
                    'U',
                    'V',
                    'W',
                    'X',
                    'Y',
                    'Z',
                    ' ',
                    '\n'
                };
            var rd = new Random();
            string filePath = Path.Combine(
                testContext.TestRunResultsDirectory,
                testContext.TestName + DateTime.Now.ToString("yy_MM_dd_hh_mm_ss") + ".txt");
            int byteSize = 0;

            SafeMethodExecutor.Execute(
                () =>
                    {
                        using (var fileStream = new FileStream(filePath, FileMode.CreateNew))
                        {
                            while (byteSize < size)
                            {
                                char ch = chq[rd.Next(0, chq.Length)];
                                fileStream.WriteByte((byte)ch);
                                byteSize++;
                            }

                            fileStream.Close();
                        }
                    }).LogDetails();

            return filePath;
        }

        /// <summary>
        /// Extracts zip file in the output directory
        /// </summary>
        /// <param name="outFolder">Folder to extract the files</param>
        /// <param name="archiveFilenameIn">Archive filename to extract</param>
        /// <param name="password">Password to decrypt the archive</param>
        public static void ExtractZipFile(string outFolder, string archiveFilenameIn, string password = null)
        {
            using (FileStream fs = File.OpenRead(archiveFilenameIn))
            {
                using (ZipFile zf = new ZipFile(fs))
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        zf.Password = password; // AES encrypted entries are handled automatically
                    }

                    foreach (ZipEntry zipEntry in zf)
                    {
                        if (!zipEntry.IsFile)
                        {
                            continue; // Ignore directories
                        }

                        string entryFileName = zipEntry.Name;
                        var buffer = new byte[4096]; // 4K is optimum
                        Stream zipStream = zf.GetInputStream(zipEntry);

                        string fullZipToPath = Path.Combine(outFolder, entryFileName);
                        string directoryName = Path.GetDirectoryName(fullZipToPath);

                        if (directoryName?.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }

                        using (FileStream streamWriter = File.Create(fullZipToPath))
                        {
                            StreamUtils.Copy(zipStream, streamWriter, buffer);
                        }
                    }
                }
            }
        }

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

        /// <summary>
        /// Delete files in folder.
        /// </summary>
        /// <param name="folderPath">
        /// The folder path.
        /// </param>
        public static void DeleteFilesInFolder(string folderPath)
        {
            var info = new ProcessStartInfo
            {
                Arguments = string.Format("/C rd /s /q \"{0}\"", folderPath),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };

            Process.Start(info);
        }

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
                Arguments = string.Format("/C del /s /q \"{0}\\{1}\"", folderPath, mask),
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
            };

            Process.Start(info);
        }

        /// <summary>
        /// Wait for certain file to be present
        /// </summary>
        /// <param name="regexPattern">Regex to filter certain file</param>
        /// <param name="folderToSave">Folder where to get the file</param>
        /// <returns>File Name</returns>
        public static string GetTheDownloadedFileName(string regexPattern, string folderToSave)
        {
            string fileName = null;
            for (int i = 0; i < 6; i++)
            {
                if (Directory.Exists(folderToSave))
                {
                    fileName = Directory.GetFiles(folderToSave).FirstOrDefault(x => Regex.IsMatch(x, regexPattern))
                               ?? string.Empty;
                    if (string.IsNullOrEmpty(fileName) || fileName.Contains("crdownload"))
                    {
                        System.Threading.Thread.Sleep(5000);
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(5000);
                }
            }

            return fileName;
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
                    File.Open(
                        e.FullPath,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.Read);
                }
                catch (IOException)
                {
                    // we couldn't open the file
                    // this is probably because the copy operation is not done
                    // just swallow the exception
                    return;
                }

                // now we have a handle to the file
            };
        }
    }
}