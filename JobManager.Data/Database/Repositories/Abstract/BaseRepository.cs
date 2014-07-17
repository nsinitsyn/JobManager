using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobManager.Data.Database.Entities;
using JobManager.Data.Database.Repositories.Abstract.Interfaces;

namespace JobManager.Data.Database.Repositories.Abstract
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : BaseEntity
    {
        private readonly IDbSet<T> _dbset;
        private readonly DataContext _dataContext;

        public BaseRepository()
        {
            _dataContext = DataContext.DefaultContext;
            _dbset = _dataContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbset.Add(entity);
        }

        public void Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        public T GetById(Guid id)
        {
            return _dbset.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }
    }
}
