using System.Collections;
using com.mastama.DTOs;

namespace com.mastama.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponse> GetAllUsersAsync();
    Task<BaseResponse> GetUserByIdAsync(Guid id);
    Task<BaseResponse> CreateUserAsync(UserDto userDto);
    Task<BaseResponse> UpdateUserAsync(Guid id, UserDto userDto);
    Task<BaseResponse> DeleteUserAsync(Guid id);
    Task<BaseResponse> AuthenticateAsync(string phoneNumber, string password);
}