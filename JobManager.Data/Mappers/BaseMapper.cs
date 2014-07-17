using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Mappers
{
    public abstract class BaseMapper<TDto, TDomain, TDb>
    {
        public abstract TDomain DbToDomain(TDb db);

        public abstract TDb DomainToDb(TDomain domain);
        public abstract TDto DomainToDto(TDomain domain);

        public abstract TDb DtoToDb(TDto dto);
        public abstract TDomain DtoToDomain(TDto dto);
    }
}
