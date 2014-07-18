using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Domain;
using JobManager.JobManagerService.DTO;

namespace JobManager.JobManagerService.Mappers
{
    public class TriggerDtoMapper : BaseDtoMapper<TriggerDto, Trigger>
    {
        static TriggerDtoMapper()
        {
            Mapper = new TriggerDtoMapper();
        }

        public static TriggerDtoMapper Mapper { get; set; }

        public override TriggerDto DomainToDto(Trigger trigger)
        {
            if (trigger == null)
            {
                return null;
            }

            var triggerDto = new TriggerDto
            {
                Cron = trigger.Cron
            };
            return triggerDto;
        }

        public override Trigger DtoToDomain(TriggerDto triggerDto)
        {
            if (triggerDto == null)
            {
                return null;
            }

            var trigger = new Trigger
            {
                Cron = triggerDto.Cron
            };
            return trigger;
        }
    }
}
