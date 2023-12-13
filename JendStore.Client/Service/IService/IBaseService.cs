using JendStore.Client.Models;

namespace JendStore.Client.Sevice.IServices
{
    public interface IBaseService
    {
        Task<ResponseDTOStatus?> SendAsync(RequestDTOModel requestDTOModel);
    }
}
