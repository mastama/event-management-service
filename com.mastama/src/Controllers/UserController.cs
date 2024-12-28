using System.Net;
using com.mastama.DTOs;
using com.mastama.Helper;
using com.mastama.Services.Interfaces;
using com.mastama.Utils;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace com.mastama.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("create")]
    public async Task<BaseResponse> CreateUser([FromBody] UserDto userDto)
    {
        Log.Information("Incoming Create User {phoneNumber}", userDto.PhoneNumber);
        BaseResponse responseService = await _userService.CreateUserAsync(userDto);
        Log.Information("Outgoing Create User {phoneNumber}", userDto.PhoneNumber);
        return responseService;
    }

    [HttpGet]
    public async Task<BaseResponse> GetAllUser()
    {
        Log.Information("Incoming GetAll User");
        BaseResponse responseService = await _userService.GetAllUsersAsync();
        Log.Information("Outgoing GetAll User");
        return responseService;
    }
}