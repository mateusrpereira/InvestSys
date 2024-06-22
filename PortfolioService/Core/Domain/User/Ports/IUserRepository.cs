using Domain.Entities;

namespace Domain.Ports
{
    public interface IUserRepository
    {
        Task<User> Get(int Id);
        Task<int> Create(User user);
    }
}
