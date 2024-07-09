using Application.User.Dtos;
using Application.User.Ports;
using Domain.Ports;
<<<<<<< HEAD
=======
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)

namespace Application.User
{
    public class LoginManager : ILoginManager
    {
        private IUserRepository _userRepository;
<<<<<<< HEAD

        public LoginManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
=======
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        public IConfiguration _configuration { get; }

        public LoginManager(IUserRepository userRepository, 
                            SigningConfigurations signingConfigurations,
                            TokenConfigurations tokenConfigurations,
                            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _configuration = configuration;
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)
        }

        public async Task<object> FindByLogin(LoginDto user)
        {
<<<<<<< HEAD

            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                return await _userRepository.FindByLogin(user.Email);
            }
            else
            {
                return null;
            }
        }
=======
            var baseUser = new Domain.Entities.User();
            
            if(user != null && !string.IsNullOrWhiteSpace(user.Email))
            {
                baseUser = await _userRepository.FindByLogin(user.Email);
                if (baseUser == null)
                {
                    return new
                    {
                        authenticated = false,
                        message = "Falha ao autenticar"
                    };
                }
                else
                {
                    var identity = new ClaimsIdentity(
                        new GenericIdentity(baseUser.Email),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.Email),
                        }
                    );

                    DateTime createDate = DateTime.Now;
                    DateTime expirationDate = createDate + TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

                    var handler = new JwtSecurityTokenHandler();
                    string token = CreateToken(identity, createDate, expirationDate, handler);
                    return SuccessObject(createDate, expirationDate, token, baseUser);
                }
            }
            else
            {
                return new
                {
                    authenticated = false,
                    message = "Falha ao autenticar"
                };
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate,
            });

            var token = handler.WriteToken(securityToken);
            return token;
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token, Domain.Entities.User user)
        {
            return new
            {
                authenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expirationDate = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                acessToken = token,
                userName = user.Email,
                name = user.Name,
                message = "Usuário Logado com sucesso"
            };
        }
>>>>>>> 6c3766f (Implementação da autenticação - JWT Token)
    }
}
