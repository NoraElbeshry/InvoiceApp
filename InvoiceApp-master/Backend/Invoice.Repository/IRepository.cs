using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        IQueryable<T> Get(Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "",
           int page = 0,
           int pageSize = 0);
        IQueryable<T> GetAll();
        
        T Add(T entity);
        void Delete(T entity);
        void AddRange(IEnumerable<T> entities);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);

    }
}
