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
            throw new NotImplementedException();
        }

        public override TriggerDb DomainToDb(Trigger trigger)
        {
            throw new NotImplementedException();
        }

        public override TriggerDto DomainToDto(Trigger trigger)
        {
            throw new NotImplementedException();
        }

        public override TriggerDb DtoToDb(TriggerDto triggerDto)
        {
            var triggerDb = new TriggerDb
                                {
                                    Cron = triggerDto.Cron
                                };
            return triggerDb;
        }

        public override Trigger DtoToDomain(TriggerDto triggerDto)
        {
            throw new NotImplementedException();
        }
    }
}
