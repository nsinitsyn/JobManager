using System.Configuration;

namespace JobManager.Business
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
