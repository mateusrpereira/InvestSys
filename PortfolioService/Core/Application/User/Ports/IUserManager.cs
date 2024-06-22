﻿using Application.User.Requests;
using Application.User.Responses;

namespace Application.User.Ports
{
    public interface IUserManager
    {
        Task<UserResponse> CreateUser(CreateUserRequest request);
        Task<UserResponse> GetUser(int userId);

    }
}
