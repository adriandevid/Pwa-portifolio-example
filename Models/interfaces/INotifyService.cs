using System.Threading.Tasks;

namespace AdrianP.Models.interfaces
{
    public interface INotifyService
    {
        Task<ApiResponseList> Listar();
        Task<ApiResponseNotify> ListarNotify();
        Task Register(UserNotify entity);
    }
}
