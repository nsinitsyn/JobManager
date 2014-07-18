using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.DTO;
using JobManager.Data.Domain;

namespace JobManager.Data.Mappers
{
    public class JobEventMapper
    {
        static JobEventMapper()
        {
            Mapper = new JobEventMapper();
        }

        public static JobEventMapper Mapper { get; set; }

        public JobEventDto DomainToDto(JobEvent jobEvent)
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

        public JobEvent DtoToDomain(JobEventDto jobEventDto)
        {
            throw new NotImplementedException();
        }
    }
}
