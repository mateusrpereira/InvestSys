using Domain.Active.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Active
{
    public class ActiveRepository : IActiveRepository
    {
        private PortfolioDbContext _portfolioDbContext;
        public ActiveRepository(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }
        public async Task<int> Create(Domain.Entities.Active active)
        {
            _portfolioDbContext.Active.Add(active);
            await _portfolioDbContext.SaveChangesAsync();
            return active.Id;
        }
        public Task<Domain.Entities.Active> Get(int id)
        {
            return _portfolioDbContext.Active.Where(a => a.Id == id).FirstOrDefaultAsync();
        }
        public async Task<int> Update(Domain.Entities.Active active)
        {
            _portfolioDbContext.Active.Update(active);
            await _portfolioDbContext.SaveChangesAsync();
            return active.Id;
        }
        public async Task Delete(int id)
        {
            var activeId = await Get(id);
            _portfolioDbContext.Active.Remove(activeId);
            await _portfolioDbContext.SaveChangesAsync();
        }
    }
}
