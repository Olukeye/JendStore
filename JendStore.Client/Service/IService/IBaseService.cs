using JendStore.Client.Models;

namespace JendStore.Client.Sevice.IService
{
    public interface IBaseService
    {
        Task<ResponsDto?> SendAsync(RequestDto requestDTOModel);
    }
}
