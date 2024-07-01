namespace Domain.Portfolio.Ports
{
    public interface IPortfolioRepository
    {
        Task<Entities.Portfolio> Get(int id);
        Task<int> Create(Entities.Portfolio portfolio);
        Task<int> Update(Entities.Portfolio portfolio);
        Task Delete(int id);
    }
}
