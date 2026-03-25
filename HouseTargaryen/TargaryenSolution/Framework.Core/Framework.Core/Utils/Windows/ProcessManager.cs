namespace Framework.Core.Utils.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Management;
    using System.Runtime.InteropServices;
    using System.Threading;

    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Wraps OS Windows' routines in dealing with the window subsystem and processes.
    /// </summary>
    public static class ProcessManager
    {
        /// <summary>
        /// Brings the window for the given process to the process
        /// </summary>
        /// <param name="process">The process for the windows to bring to the front</param>
        public static void ActivateProcessWindows(Process process)
        {
            ProcessManager.SwitchToThisWindow(process.MainWindowHandle, true);
            IntPtr forground = ProcessManager.GetForegroundWindow();
            ProcessManager.SetForegroundWindow(forground);
        }

        /// <summary>
        /// Closes print dialog if it appears
        /// </summary>
        /// <param name="imageName">The Name of a process family to capture.</param>
        /// <param name="processInitiator"> A set of steps to cause a new process to appear in the family.</param>
        /// <returns> If the process appeared and was terminated.</returns>
        public static bool CloseLastAddedProcess(string imageName, Action processInitiator)
        {
            const int DefaultTimeout = 3000;
            int[] processIdsBefore = Process.GetProcessesByName(imageName).Select(p => p.Id).ToArray();
            int[] processIdsAfter = { };
            int initialProcessCount = processIdsBefore.Length;

            processInitiator();

            for (int i = 0; i < 15000 && processIdsAfter.Length < initialProcessCount + 1; i += DefaultTimeout)
            {
                Thread.Sleep(DefaultTimeout);
                processIdsAfter = Process.GetProcessesByName(imageName).Select(p => p.Id).ToArray();
            }

            int[] newIds = processIdsAfter.Except(processIdsBefore).ToArray();
            bool result = newIds.Any();

            if (result)
            {
                foreach (int newId in newIds)
                {
                    SafeMethodExecutor.Execute(Process.GetProcessById(newId).Kill);
                }
            }

            return result;
        }

        /// <summary>
        /// The find matching host process id for.
        /// </summary>
        /// <param name="processId">
        /// The process id.
        /// </param>
        /// <param name="processesToSearch">
        /// The processes to search.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int FindMatchingHostProcessIdFor(int processId, IEnumerable<Process> processesToSearch)
        {
            foreach (Process process in processesToSearch)
            {
                if (ProcessManager.IsDescendantProcess(processId, process.Id))
                {
                    Process parentProcess = Process.GetProcessById(ProcessManager.GetParentProcessId(process.Id));

                    return parentProcess.ProcessName == process.ProcessName ? parentProcess.Id : process.Id;
                }
            }

            return -1;
        }

        /// <summary>
        /// Gets the parent process for the process with the given id
        /// </summary>
        /// <param name="childPid">The id of the child process</param>
        /// <returns>The id of the parent process</returns>
        public static int GetParentProcessId(int childPid)
        {
            string query = string.Format("SELECT ParentProcessId FROM Win32_Process WHERE ProcessId = {0}", childPid);
            var search = new ManagementObjectSearcher("root\\CIMV2", query);
            ManagementObjectCollection.ManagementObjectEnumerator results = search.Get().GetEnumerator();

            if (results.MoveNext())
            {
                ManagementBaseObject queryObj = results.Current;
                uint parentPid = (uint)queryObj["ParentProcessId"];
                return (int)parentPid;
            }

            return -1;
        }

        /// <summary>
        /// Determines if a process within the process true of a given parent process
        /// </summary>
        /// <param name="expectedParentPid">The process id of the potential parent</param>
        /// <param name="childPid">The process id of the  potential child</param>
        /// <returns>True if the child process is a child of the specified parent process</returns>
        public static bool IsDescendantProcess(int expectedParentPid, int childPid)
        {
            int parentId;
            bool found;

            do
            {
                parentId = ProcessManager.GetParentProcessId(childPid);
                found = parentId == expectedParentPid;
                childPid = parentId;
            }
            while (parentId != -1 && !found);

            return found;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern void SwitchToThisWindow(IntPtr hWnd, bool turnOn);
    }
}