using JendStore.Cart.Service.API.Data;
using JendStore.Cart.Service.API.Models;
using JendStore.Cart.Service.API.Repository.Interface;

namespace JendStore.Cart.Service.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<CartDetail> _cartDetail;
        private IGenericRepository<CartHeader> _cartHeader;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<CartDetail> CartDetails => _cartDetail ??= new GenericRopository<CartDetail>(_context);
        public IGenericRepository<CartHeader> CartHeaders => _cartHeader ??= new GenericRopository<CartHeader>(_context);

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
