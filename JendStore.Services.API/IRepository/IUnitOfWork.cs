using JendStore.Services.API.Models;

namespace JendStore.Services.API.IRepository
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<Coupon> Coupons { get; }

        Task Save();
    }
}
