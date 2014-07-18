
namespace JobManager.Business.Mappers
{
    public abstract class BaseDomainDbMapper<TDomain, TDb>
    {
        public abstract TDomain DbToDomain(TDb db);
        public abstract TDb DomainToDb(TDomain domain);
    }
}
