using JendStore.Client.Models;

namespace JendStore.Client.Sevice.IService
{
    public interface IBaseService
    {
        Task<ResponseDTOStatus?> SendAsync(RequestDTOModel requestDTOModel);
    }
}
