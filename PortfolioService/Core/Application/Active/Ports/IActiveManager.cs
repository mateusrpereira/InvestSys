using Application.Active.Requests;
using Application.Active.Responses;

namespace Application.Active.Ports
{
    public interface IActiveManager
    {
        Task<ActiveResponse> CreateActive(CreateActiveRequest request);
        Task<ActiveResponse> GetActive(int activeId);
    }
}
