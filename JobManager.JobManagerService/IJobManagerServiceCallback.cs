using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;

namespace JobManager.JobManagerService
{
    public interface IJobManagerServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnEvent(JobEventDto eventDto);

        [OperationContract(IsOneWay = false)]
        TransferData OnEventSync(JobEventDto eventDto);

        [OperationContract(IsOneWay = true)]
        void WorkerWasStarted(WorkerDto worker);
    }
}
