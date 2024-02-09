using JendStore.Products.Service.API.Models;

namespace JendStore.Products.Service.API.Repository.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Product> Products { get; }

        Task Save();
    }
}
