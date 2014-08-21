using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using JobManager.Transfer.Data.DTO;

namespace JobManager.JobManagerService
{
    [ServiceContract(CallbackContract = typeof(IJobManagerServiceCallback), SessionMode = SessionMode.Required)]
    public interface IJobManagerService
    {
        [OperationContract]
        WorkerDto RunJob(JobDto jobDto);

        [OperationContract]
        TransferData Signal(Guid workerId, TransferData data);

        [OperationContract]
        JobDto GetJob(Guid jobId);

        [OperationContract]
        List<WorkerDto> GetWorkers(); // возвращает все воркеры, созданные за сеанс  !!!Нужно подумать о чистке этого списка, т. к. служба может работать очень долго

        [OperationContract]
        Guid ScheduleJob(JobDto jobDto); // регит джобу в нашей базе и в quartz

        [OperationContract]
        void RescheduleJob(JobDto jobDto);

        [OperationContract]
        void UnscheduleJob(Guid jobId); // удаляет джобу из базы quartz (делает ее невыполняющейся), в нашей оставляет

        [OperationContract]
        void DeleteJob(Guid jobId); // удаляет джобу из нашей базы и из quartz

        #region Working on client contexts

        [OperationContract]
        void SubscribeClientContext();

        [OperationContract]
        void UnsubscribeClientContext();

        [OperationContract]
        void SetClientContextToWorker(Guid workerId);

        #endregion

        // void ChangeJob(); // Изменить настройки (например триггеры джобе; в т.ч. той, которая регилась через конфиг - в этом случае нужно и в конфиге поменять триггеры)

        //// События
        //void JobStarted(Job job); // Была запущена периодическая задача. Тут должна быть возможность подписаться на события этой джобы и слать сигналы
        //void JobFailed(Job job);
        //void JobFinished(Job job);

        // Установить периодическую джобу(Id, когда первый запуск, триггеры)
        // При старте JobManager выбирает все повт. джобы и заново устанавливает их для запуска

        // Удалить периодическую джобу по Id (удалить можно только динамически назначенные джобы)
    }
}
