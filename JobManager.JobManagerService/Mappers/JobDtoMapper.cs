using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Business.Domain;
using JobManager.Transfer.Data.DTO;

namespace JobManager.JobManagerService.Mappers
{
    public class JobDtoMapper : BaseDtoMapper<JobDto, Job>
    {
        static JobDtoMapper()
        {
            Mapper = new JobDtoMapper();
        }

        public static JobDtoMapper Mapper { get; set; }

        public override JobDto DomainToDto(Job job)
        {
            if (job == null)
            {
                return null;
            }

            var jobDto = new JobDto
            {
                Id = job.Id,
                ClassName = job.ClassName,
                Data = new TransferData(job.Data),
                Triggers = job.Triggers.Select(TriggerDtoMapper.Mapper.DomainToDto).ToList(),
                Scheduled = job.Scheduled
            };

            return jobDto;
        }

        public override Job DtoToDomain(JobDto jobDto)
        {
            if (jobDto == null)
            {
                return null;
            }

            var job = new Job
            {
                Id = jobDto.Id,
                ClassName = jobDto.ClassName,
                Data = jobDto.Data.GetData(),
                Triggers = jobDto.Triggers.Select(TriggerDtoMapper.Mapper.DtoToDomain).ToList()
            };

            return job;
        }
    }
}
