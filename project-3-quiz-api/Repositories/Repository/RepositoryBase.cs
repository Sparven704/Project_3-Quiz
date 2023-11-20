using Microsoft.EntityFrameworkCore;
using project_3_quiz_api.Data;
using project_3_quiz_api.Repositories.Interfaces;
using System.Linq.Expressions;

namespace project_3_quiz_api.Repositories.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RepositoryBase(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IQueryable<T?>> GetAllAsync()
        {
            var result = await _applicationDbContext.Set<T>().AsNoTracking().ToListAsync();
            return result.AsQueryable();
        }

        public async Task<IQueryable<T?>> GetByConditionAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _applicationDbContext.Set<T>().Where(expression).ToListAsync();
            return result.AsQueryable();
        }

        public async Task CreateAsync(T entity)
        {
            await _applicationDbContext.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _applicationDbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            _applicationDbContext.Set<T>().Remove(entity);
        }
        
        public async Task SaveAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
