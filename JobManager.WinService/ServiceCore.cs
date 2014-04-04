using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.WinService
{
    public class ServiceCore
    {
        private ServiceHost _serviceHost = null;

        public void Start(string[] args)
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
            }

            _serviceHost = new ServiceHost(typeof(JobManagerService.JobManagerService));
            _serviceHost.Open();
        }

        public void Stop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
            }
        }
    }
}
