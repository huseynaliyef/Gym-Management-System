using Data.DAL;
using Logic.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Logic.Services.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly GymManagementDbContext _context;
        public GenericRepository(GymManagementDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public async Task Add(T entity)
        {
            await Table.AddAsync(entity);
        }

        public async Task AddAndCommit(T entity)
        {
            await Add(entity);
            await Commit();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            Table.Remove(entity);
        }

        public async Task<T> Find(int id)
        {
            return await Table.FindAsync(id);
        }

        public async Task<List<T>> GetAll()
        {
            return await Table.ToListAsync();
        }

        public async Task<List<T>> GetByExperssion(Expression<Func<T, bool>> expression)
        {
            return await Table.Where(expression).ToListAsync();
        }

        public IQueryable<T> GetTableWithRelation(Expression<Func<T, object>> expression)
        {
            return Table.Include(expression);
        }

        public void Update(T entity)
        {
            Table.Entry(entity).State = EntityState.Modified;
        }
    }
}
