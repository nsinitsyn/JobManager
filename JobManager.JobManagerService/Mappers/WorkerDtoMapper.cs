using JobManager.Data.Domain;
using JobManager.JobManagerService.DTO;

namespace JobManager.JobManagerService.Mappers
{
    public class WorkerDtoMapper : BaseDtoMapper<WorkerDto, Worker>
    {
        static WorkerDtoMapper()
        {
            Mapper = new WorkerDtoMapper();
        }

        public static WorkerDtoMapper Mapper { get; set; }

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
