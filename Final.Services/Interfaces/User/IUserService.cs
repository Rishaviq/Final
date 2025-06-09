using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using final.Services.DTOs.User;
using Final.Services.DTOs.User;

namespace Final.Services.Interfaces.User
{
    public interface IUserService
    {
        public Task<LoginResponse> Login(LoginRequest loginRequest);
        public Task<GetUserResponse> GetUser(int userId);
    }
}
