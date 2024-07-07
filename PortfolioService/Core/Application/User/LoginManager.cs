using Application.User.Dtos;
using Application.User.Ports;
using Domain.Ports;

namespace Application.User
{
    public class LoginManager : ILoginManager
    {
        private IUserRepository _userRepository;

        public LoginManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<object> FindByLogin(LoginDto user)
        {

            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                return await _userRepository.FindByLogin(user.Email);
            }
            else
            {
                return null;
            }
        }
    }
}
