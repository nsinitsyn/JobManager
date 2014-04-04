using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JobManager.Data.DTO;

namespace JobManager.JobManagerService
{
    [ServiceContract(CallbackContract = typeof(IJobManagerServiceCallback), SessionMode = SessionMode.Required)]
    public interface IJobManagerService
    {
        [OperationContract]
        JobOutputDataBase RunJob(JobInputDataBase jobInputData);

        // Установить периодическую джобу(Id, когда первый запуск, триггеры)
        // При старте JobManager выбирает все повт. джобы и заново устанавливает их для запуска

        // Удалить периодическую джобу по Id
    }
}
