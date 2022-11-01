using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_rpg.DTOs.User;

namespace dotnet_rpg.Services.AuthService
{
    public interface IAuthRepository
    {
        Task<int> Register(User user, string password);
        Task<AuthToken> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
