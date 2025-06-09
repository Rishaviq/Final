using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Final.Repositories.Interfaces.User;
using Final.Services.DTOs.User;
using Final.Services.Interfaces.User;

namespace Final.Services.Implementations.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<GetUserResponse> GetUser(int userId)
        {
            try
            {
                Models.User user = await _userRepository.RetrieveAsync(userId);
                return new GetUserResponse
                {
                    User =
                          new UserDTO
                          {
                              UserId = user.UserId,
                              Username = user.Username,
                              Email = user.Fullname
                          }
                };
            }
            catch (Exception ex)
            {
                return new GetUserResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }

        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                UserFilter filter = new UserFilter();
                filter.Username = loginRequest.Username;
                //pretend we hash the password somewhere here
                await foreach (var user in _userRepository.RetrieveCollectionAsync(filter))
                {
                    if (user.Password == loginRequest.Password) {
                        return new LoginResponse
                        {
                            IsSuccesful=true,
                            UserId = user.UserId
                        };
                    }
                }
                return new LoginResponse
                {
                    IsSuccesful = false,
                    Message = "Invalid username or password"
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    IsSuccesful = false,
                    Message = ex.Message
                };
            }
            
        }
    }
}
