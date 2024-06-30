using Domain.Ports;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public async Task Save(IUserRepository userRepository)
        {
            if (Id == 0)
            {
                Id = await userRepository.Create(this);
            }
            else
            {
                await userRepository.Update(this);
            }
        }
    }
}
