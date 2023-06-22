using Microsoft.EntityFrameworkCore;
using SurveyApp.Domain.RepositoryContracts;
using SurveyApp.Infrastructure.DatabaseContexts;
using System.Linq.Expressions;

namespace SurveyApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            this.db = db;
            this.dbSet = db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool includeProperties = false, bool tracked = false) // Comma separated, Case Sensitive (e.g. "Category,Product") include properties
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();
            
            if(!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            if (filter != null)
                dbSetQuery = dbSetQuery.Where(filter);

            return await dbSetQuery.ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> filter, bool includeProperties = true, bool tracked = false)
        {
            IQueryable<T> dbSetQuery = tracked ? dbSet : dbSet.AsNoTracking();

            if(!includeProperties)
                dbSetQuery = dbSetQuery.IgnoreAutoIncludes();

            return await dbSetQuery.FirstOrDefaultAsync(filter);
        }

        public async Task DeleteAsync(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            dbSet.Update(entity);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
