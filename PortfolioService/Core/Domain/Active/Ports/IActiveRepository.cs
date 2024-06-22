namespace Domain.Active.Ports
{
    public interface IActiveRepository
    {
        Task<Entities.Active> Get(int id);
        Task<int> Create(Entities.Active active);
    }
}
