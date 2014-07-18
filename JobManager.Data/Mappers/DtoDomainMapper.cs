using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Mappers
{
    public abstract class DtoDomainMapper<TDto, TDomain>
    {
        public abstract TDto DomainToDto(TDomain domain);
        public abstract TDomain DtoToDomain(TDto dto);
    }
}
