
namespace JobManager.JobManagerService.Mappers
{
    public abstract class BaseDtoMapper<TDto, TDomain>
    {
        public abstract TDto DomainToDto(TDomain domain);
        public abstract TDomain DtoToDomain(TDto dto);
    }
}
