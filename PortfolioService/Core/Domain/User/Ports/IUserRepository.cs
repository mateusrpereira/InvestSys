using Domain.Entities;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<int> Create(User user);
        Task<User> Get(int Id);
        Task<int> Update(User user);
        Task Delete(int Id);
        Task<User> FindByLogin(string email);
    }
}
