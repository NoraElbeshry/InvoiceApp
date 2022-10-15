using InvoiceApp.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Repository
{
    public class Repository<T> : IRepository<T>, IDisposable where T : class
    {
        DbContext _dbContext;
        public DbSet<T> _dbset;

        public Repository(DbContext context)
        {
            _dbContext = context;
            _dbset = _dbContext.Set<T>();
        }

        public T Add(T entity)
        {
            _dbset.Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
        public void AddRange(IEnumerable<T> entities)
        {
            _dbset.AddRange(entities);
        }
        public void Delete(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
            _dbset.Remove(entity);
        }
        public void DeleteRange(IEnumerable<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }

        public IQueryable<T> GetAll()
        {
            return _dbset;
        }

        public T GetById(int id)
        {
            return _dbset.Find(id);
        }

        public virtual IQueryable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "", int page = 0, int pageSize = 0)
        {
            _dbContext.Set<T>().Local.ToList().ForEach(x =>
            {
                _dbContext.Entry(x).State = EntityState.Detached;

            });

            IQueryable<T> query = _dbset;

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter == null)
            {
                if (page != 0 && pageSize != 0)
                    query = query.Skip((page - 1) * pageSize).Take(pageSize);

            }


            if (filter != null)
            {
                if (page != 0 && pageSize != 0)
                    query = query.Where(filter).Skip((page - 1) * pageSize).Take(pageSize);
                else
                    query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty.Trim());
            }

            return query;
        }


        public void Dispose()
        {
            _dbContext.Dispose();
        }

    }
}
