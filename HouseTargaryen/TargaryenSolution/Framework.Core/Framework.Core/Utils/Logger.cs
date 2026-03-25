namespace Framework.Core.Utils
{
    using System;
    using System.IO;
    using System.Linq;

    using log4net;
    using log4net.Appender;

    using ReportPortal.Log4Net;

    /// <summary>
    /// Logs formatted information.
    /// </summary>
    public static class Logger
    {
        private static ILog LogInstance { get; set; }
       
        /// <summary>
        /// Initialize Logger
        /// </summary>
        public static void InitializeLogger(string loggerName) => LogInstance = LogManager.GetLogger(loggerName);
        
        /// <summary>
        /// Logs a formatted message.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogDebug(string message, params object[] args) =>
           LogInstance.Debug(message);

        /// <summary>
        /// Logs a formatted message as an error.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogError(string message, params object[] args) =>
            LogInstance.Error(message);

        /// <summary>
        /// Logs a formatted message as an informational record.
        /// </summary>
        /// <param name="message">A message or message template. A message template is considered as a message if no arguments.</param>
        /// <param name="args">Arguments to specify in the message template.</param>
        public static void LogInfo(string message, params object[] args) =>
            LogInstance.Info(message);

        /// <summary>
        /// Delete Log folder if log is empty
        /// </summary>
        public static void DeleteLogFolderIfEmpty()
        {
            try
            {
                FileAppender fileAppender =
                    LogManager.GetRepository().GetAppenders().OfType<FileAppender>().FirstOrDefault();
                string folderPath = Path.GetDirectoryName(fileAppender.File);

                if (Directory.GetFiles(folderPath).All(file => new FileInfo(file).Length == 0))
                {
                    fileAppender.Close();

                    Directory.Delete(folderPath, true);
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                Logger.LogError("Error while deleting folder", ex);
            }
            catch (NullReferenceException ex)
            {
                Logger.LogError("Error while closing appender", ex);
            }
        }

        private static ReportPortalAppender InitializeAppender() =>
            new ReportPortalAppender();
    }
}