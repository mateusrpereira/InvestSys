using Domain.Entities;

namespace Domain.Active.Ports
{
    public interface IActiveRepository
    {
        Task<Entities.Active> Get(int id);
        Task<int> Create(Entities.Active active);
        Task<int> Update(Entities.Active active);
        Task Delete(int Id);
    }
}
