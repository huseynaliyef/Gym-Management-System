using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Services.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task Add(T entity);
        Task Commit();
        Task AddAndCommit(T entity);
        Task<List<T>> GetAll();
        Task<T> Find(int id);
        Task<List<T>> GetByExperssion(Expression<Func<T, bool>> expression);
        IQueryable<T> GetTableWithRelation(Expression<Func<T, object>> expression);
        void Delete(T entity);
        void Update(T entity);

    }
}
