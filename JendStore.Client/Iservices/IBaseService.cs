using JendStore.Client.Models;

namespace JendStore.Client.Iservices
{
    public interface IBaseService
    {
        Task<ResponseDTOStatus?> SendAsync(RequestDTOModel requestDTOModel);
    }
}
