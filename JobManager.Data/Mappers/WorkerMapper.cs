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
    public class WorkerMapper : DtoDomainMapper<WorkerDto, Worker>
    {
        static WorkerMapper()
        {
            Mapper = new WorkerMapper();
        }

        public static WorkerMapper Mapper { get; set; }

        public override WorkerDto DomainToDto(Worker worker)
        {
            if (worker == null)
            {
                return null;
            }

            var workerDto = new WorkerDto
                                {
                                    Id = worker.Id,
                                    JobId = worker.Job.Id
                                };
            return workerDto;
        }

        public override Worker DtoToDomain(WorkerDto workerDto)
        {
            if (workerDto == null)
            {
                return null;
            }

            return null;
        }
    }
}
