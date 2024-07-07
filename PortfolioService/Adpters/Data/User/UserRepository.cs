using Domain.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.User
{
    public class UserRepository : IUserRepository
    {
        private PortfolioDbContext _portfolioDbContext;
        public UserRepository(PortfolioDbContext portfolioDbContext)
        {
            _portfolioDbContext = portfolioDbContext;
        }
        public async Task<int> Create(Domain.Entities.User user)
        {
            _portfolioDbContext.User.Add(user);
            await _portfolioDbContext.SaveChangesAsync();
            return user.Id;
        }
        public Task<Domain.Entities.User> Get(int Id)
        {
            return _portfolioDbContext.User.Where(u => u.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<int> Update(Domain.Entities.User user)
        {
            _portfolioDbContext.User.Update(user);
            await _portfolioDbContext.SaveChangesAsync();
            return user.Id;
        }
        public async Task Delete(int Id)
        {
            var userId = await Get(Id);
            _portfolioDbContext.User.Remove(userId);
            await _portfolioDbContext.SaveChangesAsync();
        }
        public async Task<Domain.Entities.User> FindByLogin(string email)
        {
            return await _portfolioDbContext.User.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
