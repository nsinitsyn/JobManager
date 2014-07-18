using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Business
{
    public static class JobManagerSettings
    {
        private const string JobsLibraryAssemblyNameKey = "JobsLibraryAssemblyName";
        private static string _jobsLibraryAssemblyName;

        public static string JobsLibraryAssemblyName
        {
            get
            {
                return _jobsLibraryAssemblyName ?? (_jobsLibraryAssemblyName = ConfigurationManager.AppSettings[JobsLibraryAssemblyNameKey]);
            }
        }
    }
}
