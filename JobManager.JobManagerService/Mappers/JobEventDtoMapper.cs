using System;
using JobManager.Data.Domain;
using JobManager.JobManagerService.DTO;

namespace JobManager.JobManagerService.Mappers
{
    public class JobEventDtoMapper : BaseDtoMapper<JobEventDto, JobEvent>
    {
        static JobEventDtoMapper()
        {
            Mapper = new JobEventDtoMapper();
        }

        public static JobEventDtoMapper Mapper { get; set; }

        public override JobEventDto DomainToDto(JobEvent jobEvent)
        {
            if (jobEvent == null)
            {
                return null;
            }

            var eventDto = new JobEventDto
                               {
                                   WorkerId = jobEvent.WorkerId,
                                   IsReturnResult = jobEvent.IsReturnResult,
                                   TransferData = new TransferData(jobEvent.Data)
                               };
            return eventDto;
        }

        public override JobEvent DtoToDomain(JobEventDto jobEventDto)
        {
            throw new NotImplementedException();
        }
    }
}
