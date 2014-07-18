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
    public class TriggerMapper : BaseMapper<TriggerDto, Trigger, TriggerDb>
    {
        static TriggerMapper()
        {
            Mapper = new TriggerMapper();
        }

        public static TriggerMapper Mapper { get; set; }

        public override Trigger DbToDomain(TriggerDb triggerDb)
        {
            if (triggerDb == null)
            {
                return null;
            }

            var trigger = new Trigger
                              {
                                  Cron = triggerDb.Cron
                              };
            return trigger;
        }

        public override TriggerDb DomainToDb(Trigger trigger)
        {
            if (trigger == null)
            {
                return null;
            }

            var triggerDb = new TriggerDb
                                {
                                    Cron = trigger.Cron
                                };
            return triggerDb;
        }

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
