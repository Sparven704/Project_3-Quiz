using System.Linq.Expressions;

namespace project_3_quiz_api.Repositories.Interfaces
{
    public interface IRepositoryBase<T>
    {
        Task<IQueryable<T?>> GetAllAsync();
        Task<IQueryable<T?>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveAsync();
    }
}
