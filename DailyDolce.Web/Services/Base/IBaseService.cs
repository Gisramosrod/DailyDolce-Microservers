using DailyDolce.Web.Models;

namespace DailyDolce.Web.Services.Base {
    public interface IBaseService : IDisposable {
        Task<T> SendRequest<T>(ApiRequest apiRequest);
    }
}
