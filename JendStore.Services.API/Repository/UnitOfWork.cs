using JendStore.Services.API.Data;
using JendStore.Services.API.IRepository;
using JendStore.Services.API.Models;
using System.Diagnostics.Metrics;

namespace JendStore.Services.API.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IGenericRepository<Coupon> _coupons;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IGenericRepository<Coupon> Coupons => _coupons ??= new GenericRopository<Coupon>(_context);

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
