using System.Diagnostics;

namespace Ram_Cleaner
{
    class MainClass
    {
        public static void Main()
        {
            int cleanerThreshold = 0;

            try
            {
                Console.WriteLine("Enter the threshold in percentage for the cleaner to run: ");
                cleanerThreshold = int.Parse(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            while (true)
            {
                try
                {
                    if (ProcessUtil.GetUsedRAMPercentage() > cleanerThreshold)
                    {
                        Console.WriteLine("Cleaning RAM...");
                        Process[] processes = ProcessUtil.GetAllProccesses();
                        foreach (Process process in processes)
                        {
                            try
                            {
                                if (!process.HasExited)
                                {
                                    ProcessUtil.EmptyWorkingSet(process);
                                    Console.WriteLine("Cleaned: " + process.ProcessName);
                                }
                            }
                            catch (Exception)
                            {
                                 
                            }
                            System.Threading.Thread.Sleep(5);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
                Console.WriteLine("Used RAM: " + ProcessUtil.GetUsedRAMPercentage() + "%");
                System.Threading.Thread.Sleep(5000); // Sleep for 5 seconds
            }
        }
    }
}