using System.Linq.Expressions;

namespace JendStore.Cart.Service.API.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAll(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);

        Task<T> Get(Expression<Func<T, bool>> expression);
        Task Insert(T entity);
        Task InsertRamge(IEnumerable<T> entity);
        Task Delete(int id);
        void DeleteRamge(IEnumerable<T> entity);
        void Update(T entity);
    }
}
