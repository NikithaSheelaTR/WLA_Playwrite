namespace Framework.Core.Utils.Execution
{
    using System;
    using System.Diagnostics;
    using System.IO;

    /// <summary>
    /// A wrapper class to execute OS commands.
    /// </summary>
    public static class CommandExecutor
    {
        /// <summary>
        /// Executes a windowless command synchronously.
        /// </summary>
        /// <param name="programExecutable">
        /// Executable to run.
        /// </param>
        /// <param name="arguments">
        /// The arguments for the executable.
        /// </param>
        /// <returns>
        /// The output of the executable.
        /// </returns>
        public static string ExecuteCommandSync(string programExecutable, string arguments)
        {
            string result;

            // create the ProcessStartInfo using 'programExecutable' as the program to run,
            // and 'arguments' as the parameters.
            var procStartInfo = new ProcessStartInfo(programExecutable, arguments)
                                    {
                                        RedirectStandardOutput = true,
                                        UseShellExecute = false,
                                        CreateNoWindow = true
                                    };

            // Now we create a process, assign its ProcessStartInfo and start it
            using (var p = new Process { StartInfo = procStartInfo })
            {
                try
                {
                    p.Start();
                    p.WaitForExit();
                }
                catch (Exception e)
                {
                    Logger.LogError(e.ToString());
                    throw;
                }

                using (StreamReader reader = p.StandardOutput)
                {
                    result = reader.ReadToEnd();
                }
            }

            return result;
        }

        /// <summary>
        /// Executes a Windows shell command synchronously.
        /// </summary>
        /// <param name="command">string command</param>
        public static void ExecuteCommandSync(string command)
        {
            // create the ProcessStartInfo using "cmd" as the program to run,
            // and "/c " as the parameters.
            // Incidentally, /c tells cmd that we want it to execute the command that follows, and then exit.
            var procStartInfo = new ProcessStartInfo("cmd", "/c " + command)
                                    {
                                        RedirectStandardOutput = false,
                                        UseShellExecute = true,
                                        CreateNoWindow = true
                                    };

            // Now we create a process, assign its ProcessStartInfo and start it
            var p = new Process { StartInfo = procStartInfo };

            try
            {
                p.Start();
                p.WaitForExit();
            }
            catch (Exception e)
            {
                Logger.LogError(e.ToString());
                throw;
            }
        }

        /// <summary>
        /// Terminates a process or a process tree by the process ID.
        /// </summary>
        /// <param name="id">
        /// The ID of the process.
        /// </param>
        /// <param name="killTree">
        /// Indicates, whether to terminate the process tree.
        /// </param>
        public static void KillProcess(int id, bool killTree = true)
        {
            string args = "/pid " + id + " /f";

            if (killTree)
            {
                args += " /t";
            }

            CommandExecutor.ExecuteCommandSync("taskkill", args);
        }
    }
}