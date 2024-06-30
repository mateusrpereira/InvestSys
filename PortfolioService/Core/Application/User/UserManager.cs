using Application.User.Dtos;
using Application.User.Ports;
using Application.User.Requests;
using Application.User.Responses;
using Domain.Ports;

namespace Application.User
{
    public class UserManager : IUserManager
    {
        private IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse> CreateUser(CreateUserRequest request)
        {
            try
            {
                var user = UserDto.MapToEntity(request.Data);

                //request.Data.Id = await _userRepository.Create(user);
                await user.Save(_userRepository);
                request.Data.Id = user.Id;

                return new UserResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new UserResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }
        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var user = await _userRepository.Get(userId);

            if (user == null)
            {
                return new UserResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.USER_NOT_FOUND,
                    Message = "No User record was found with the given Id"
                };
            }

            return new UserResponse
            {
                Data = UserDto.MapToDto(user),
                Success = true,
            };
        }
        public async Task<UserResponse> UpdateUser(UpdateUserRequest request)
        {
            try
            {
                var user = UserDto.MapToEntity(request.Data);

                await user.Save(_userRepository);
                request.Data.Id = user.Id;
                
                return new UserResponse
                {
                    Data = request.Data,
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new UserResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.USER_UPDATE_FAILED,
                    Message = "There was an error when updating to DB"
                };
            }
        }
        public async Task<UserResponse> DeleteUser(int userId)
        {
            try
            {
                await _userRepository.Delete(userId);

                return new UserResponse 
                {
                    Success = true,
                };
            }
            catch (Exception)
            {
                return new UserResponse()
                {
                    Success = false,
                    ErrorCode = ErrorCodes.USER_DELETE_FAILED,
                    Message = "There was an error when deleting to DB"
                };
            }
        }
    }
}
