using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Mappers
{
    public abstract class BaseDomainDbMapper<TDomain, TDb>
    {
        public abstract TDomain DbToDomain(TDb db);
        public abstract TDb DomainToDb(TDomain domain);
    }
}
