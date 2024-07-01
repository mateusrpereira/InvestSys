using Domain.Portfolio.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Portfolio
{
    public class PortfolioRepository : IPortfolioRepository
    {
        private PortfolioDbContext _portfolioDbContext;
        public PortfolioRepository(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }
        public async Task<int> Create(Domain.Entities.Portfolio portfolio)
        {
            _portfolioDbContext.Portfolio.Add(portfolio);
            await _portfolioDbContext.SaveChangesAsync();
            return portfolio.Id;
        }
        public async Task<Domain.Entities.Portfolio> Get(int id)
        {
            //Da forma abaixo carrega apenas o Portfolio e não o User
            //return _portfolioDbContext.Portfolio.Where(p => p.Id == id).FirstOrDefaultAsync();

            return await _portfolioDbContext.Portfolio.Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<int> Update(Domain.Entities.Portfolio portfolio)
        {
            _portfolioDbContext.Portfolio.Update(portfolio);
            await _portfolioDbContext.SaveChangesAsync();
            return portfolio.Id;
        }
        public async Task Delete(int id)
        {
            var portfolioId = await Get(id);
            _portfolioDbContext.Portfolio.Remove(portfolioId);
            await _portfolioDbContext.SaveChangesAsync();
        }
    }
}
