using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;
using JobManager.Data.Database.Entities;
using JobManager.Data.Domain;

namespace JobManager.Data.Mappers
{
    public class WorkerMapper : BaseMapper<WorkerDto, Worker, WorkerDb>
    {
        static WorkerMapper()
        {
            Mapper = new WorkerMapper();
        }

        public static WorkerMapper Mapper { get; set; }

        public override Worker DbToDomain(WorkerDb workerDb)
        {
            throw new NotImplementedException();
        }

        public override WorkerDb DomainToDb(Worker worker)
        {
            throw new NotImplementedException();
        }

        public override WorkerDto DomainToDto(Worker worker)
        {
            var workerDto = new WorkerDto
                                {
                                    Id = worker.Id,
                                    JobId = worker.Job.Id
                                };
            return workerDto;
        }

        public override WorkerDb DtoToDb(WorkerDto workerDto)
        {
            throw new NotImplementedException();
        }

        public override Worker DtoToDomain(WorkerDto workerDto)
        {
            throw new NotImplementedException();
        }
    }
}
