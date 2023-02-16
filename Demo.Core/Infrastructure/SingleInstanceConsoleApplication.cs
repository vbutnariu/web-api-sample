using System;
using System.Threading;

namespace Demo.Core.Infrastructure
{

    public class SingleInstanceConsoleApplication
    {

        private const string MUTEX_NAME = "Global\\{8F6F0AC4-B9A1-45fd-A8CF-72F04E6BDE8F}";
        private bool firstApplcationInstance;
        private Mutex _mutexApplication;
        private readonly string[] args;
        public SingleInstanceConsoleApplication(string[] args)
        {
            this.args = args;
        }

        private bool IsApplicationFirstInstance()
        {
            // Allow for multiple runs but only try and get the mutex once
            if (_mutexApplication == null)
            {
                _mutexApplication = new Mutex(true, MUTEX_NAME, out firstApplcationInstance);
            }

            return firstApplcationInstance;
        }

        public void Run(Action<string[]> function)
        {
            // First instance
            if (IsApplicationFirstInstance())
            {
                function(args);
            }
            else
            {
                Console.WriteLine("another instance already running!");
            }
        }







    }
}
