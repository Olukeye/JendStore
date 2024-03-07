
using JendStore.Cart.Service.API.Models;

namespace JendStore.Cart.Service.API.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<CartDetail> CartDetails { get; }
        IGenericRepository<CartHeader> CartHeaders { get; }

        Task Save();
    }
}
