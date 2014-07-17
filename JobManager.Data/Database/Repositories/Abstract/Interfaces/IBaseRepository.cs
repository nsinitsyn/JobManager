using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobManager.Data.Database.Repositories.Abstract.Interfaces
{
    public interface IBaseRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(Guid id);
        IQueryable<T> GetAll();
    }
}
