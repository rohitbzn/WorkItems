using System;
using System.ServiceProcess;
using Microsoft.Owin.Hosting;

namespace WorkItems.Service
{
    static class Program
    {
        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                try
                {
                    using (WebApp.Start<Startup>("http://127.0.0.1:9876"))
                    {
                        Console.WriteLine("Service running... Press Enter to exit.");
                        Console.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to start service: " + ex.Message);
                    System.IO.File.WriteAllText("startup-error.log", ex.ToString());
                }
            }
            else
            {
                // For running as a Windows Service
                ServiceBase.Run(new WorkItemsService());
            }
        }
    }
}