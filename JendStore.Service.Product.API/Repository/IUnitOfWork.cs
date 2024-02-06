using JendStore.Service.Product.API.Models;

namespace JendStore.Service.Product.API.Repository
{
    public interface IUnitOfWork
    {
        IGenericRepository<Products> Products { get; }

        Task Save();
    }
}
