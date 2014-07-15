﻿using System;
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
        TransferData RunJob(JobInputData jobInputData);

        //Worker RunJob(Job job);
        //Job GetJob(Guid jobId);
        //List<Worker> GetRunningJobs();
        ////StopJobToken StopJob(); // Запрос на остановку задачи. Каждая задача сама определяет этот метод
        //List<Job> GetJobs(); // Все задачи. Каждая задача имеет статус (Running, ...) и т.д.
        //void RegisterJob(Job job); // Клиент может установить периодическую задачу
        //void SetJobToNonExecutable(Job job); // Пометить ее как неисполняемую с сохранением всех настроек
        //void DeleteJob(); // Удаляет джобу из базы
        //JobOutputDataBase SendSignal(Worker worker, Signal signal); // Вызвать метод Job'ы прямо во время выполнения (это общий случай метода остановки джобы)

        //// События
        //void JobStarted(Job job);
        //void JobFailed(Job job);
        //void JobFinished(Job job);

        // Установить периодическую джобу(Id, когда первый запуск, триггеры)
        // При старте JobManager выбирает все повт. джобы и заново устанавливает их для запуска

        // Удалить периодическую джобу по Id (удалить можно только динамически назначенные джобы)
    }
}
