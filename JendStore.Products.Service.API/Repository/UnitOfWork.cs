using JendStore.Products.Service.API.Data;
using JendStore.Products.Service.API.Models;
using JendStore.Products.Service.API.Repository.Interface;

namespace JendStore.Products.Service.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Product> _products;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Product> Products => _products ??= new GenericRopository<Product>(_context);

        void IDisposable.Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
