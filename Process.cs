using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;


namespace Ram_Cleaner
{
    class ProcessUtil
    {

        public static Process[] GetAllProccesses()
        {
            Process[] processes = Process.GetProcesses();
            return processes;

        }

        public static void EmptyWorkingSet(Process process)
        {
            [DllImport("psapi.dll")]
            static extern int EmptyWorkingSet(IntPtr hwProc);
            EmptyWorkingSet(process.Handle);
        }

        public static double GetRAMUsage(Process process)
        {
            return (double)process.WorkingSet64 * 1e-09d;
        }

        public static double GetTotalRAMUsage()
        {
            double totalRAM = 0;
            Process[] processes = Process.GetProcesses();
            foreach (Process process in processes)
            {
                totalRAM += GetRAMUsage(process);
            }
            return totalRAM;
        }

        public static float GetSystemRAM()
        {
            return GC.GetGCMemoryInfo().TotalAvailableMemoryBytes * 1e-09f;
        }

        public static double GetAvailableRAM()
        {
            return GetSystemRAM() - GetTotalRAMUsage();
        }

        public static double GetUsedRAMPercentage()
        {
            return Math.Round((GetTotalRAMUsage() / GetSystemRAM()) * 100, 2);
        }

    }
}