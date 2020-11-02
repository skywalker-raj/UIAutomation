using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Zakipoint.Framework.Driver;
namespace Zakipoint.Tests.Base
{
    public abstract class AbstractBase
    {
        [OneTimeSetUp]
        public virtual void Init()
        {
            Browser.Open(Browser.Config["url"]);           
        }
        [OneTimeTearDown]
        public virtual void Dispose()
        {
            CleanupBrowserDriver();
            Browser.Dispose();
        }
        private static void CleanupBrowserDriver()
        {
            IList<string> processes = new List<string>() { "IEDRIVERSERVER", "CHROMEDRIVER", "IEXPLORE", "CHROME" };
            try
            {
                foreach (var processName in processes)
                {
                    KillProcess(processName);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
        private static void KillProcess(string processName)
        {
            var process = Process.GetProcessesByName(processName);
            foreach (var proc in
                process.Where(proc => string.Compare(proc.ProcessName, processName, StringComparison.OrdinalIgnoreCase) == 0))
            {
                proc.Kill();
            }
        }
    }
}