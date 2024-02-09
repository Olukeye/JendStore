using JendStore.Products.Service.API.Data;
using JendStore.Products.Service.API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace JendStore.Products.Service.API.Repository
{
    public class GenericRopository<T> : IGenericRepository<T> where T : class
    {
        private readonly DatabaseContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRopository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task Delete(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Remove(entity);
        }

        public async void DeleteRamge(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet;

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            IQueryable<T> query = _dbSet;

            if (expression != null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity)
        {

            await _dbSet.AddAsync(entity);
        }

        public async Task InsertRamge(IEnumerable<T> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
