using System.Net;
using com.mastama.Configuration;
using com.mastama.DTOs;
using com.mastama.Helper;
using com.mastama.Repositories.Interfaces;
using com.mastama.Services.Interfaces;
using com.mastama.Models;
using com.mastama.Utils;
using Microsoft.Extensions.Options;
using Serilog;

namespace com.mastama.Services.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly string _serviceId;
    
    //constructor
    public UserService(IUserRepository userRepository, IOptions<ServiceIdConfig> serviceIdConfig)
    {
        _userRepository = userRepository;
        _serviceId = serviceIdConfig.Value.ServiceId;
    }
    
    public async Task<BaseResponse> GetAllUsersAsync()
    {
        Log.Information("Start Getting all users");
        try
        {
            // Ambil semua data pengguna dari repository
            var users = await _userRepository.GetAllAsync();

            // Mapping entitas Users ke DTO
            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Nik = user.Nik,
                Role = user.Role,
                Status = user.Status
            }).ToList();

            // Jika data kosong, kembalikan respons dengan pesan "No users found"
            if (!userDtos.Any())
            {
                return new BaseResponse
                {
                    ResponseCode = "4041234500", // Contoh response code untuk tidak ditemukan
                    ResponseDesc = "No users found.",
                    Data = null
                };
            }

            // Kembalikan hasil dengan respons sukses
            Log.Information("End Getting all users successfully");
            return new BaseResponse
            {
                ResponseCode = "2001234500", // Contoh response code untuk sukses
                ResponseDesc = "Users retrieved successfully.",
                Data = userDtos
            };
        }
        catch (Exception ex)
        {
            // Logging jika ada kesalahan
            Log.Error("Error occurred while retrieving users: {ErrorMessage}", ex.Message);

            // Kembalikan respons dengan kode kesalahan
            return new BaseResponse
            {
                ResponseCode = "5001234500", // Contoh response code untuk server error
                ResponseDesc = "An error occurred while retrieving users.",
                Data = null
            };
        }
    }


    public Task<BaseResponse> GetUserByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<BaseResponse> CreateUserAsync(UserDto userDto)
    {
        Log.Information("Start creating user with PhoneNumber: {PhoneNumber}", userDto.PhoneNumber);

        // Periksa apakah pengguna dengan phoneNumber, email, atau NIK sudah ada
        var existingUser = await _userRepository.FindByPhoneNumberOrEmailOrNik(userDto.PhoneNumber, userDto.Email, userDto.Nik);
        if (existingUser != null)
        {
            Log.Warning("User already exists with PhoneNumber: {PhoneNumber}", userDto.PhoneNumber);

            return ResponseHelper.BuildResponse(
                HttpStatusCode.BadRequest,
                _serviceId,
                Constans.ResponseCodes.DATA_EXISTS.Code,
                "User already exists with PhoneNumber: {PhoneNumber}",
                null
            );
        }

        // Hash password
        var hashedPassword = HashPassword(userDto.Password);

        // Mapping dari UserDto ke Users
        var user = new Users
        {
            Username = userDto.Username,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            Password = hashedPassword,
            Nik = userDto.Nik,
            Role = userDto.Role.ToLower() == "admin" ? "Admin" : "User",
            Status = 1 // Default aktif
        };

        // Simpan pengguna ke database
        var createdUser = await _userRepository.AddAsync(user);

        // Mapping dari Users ke UserDto untuk response
        var responseData = new UserDto
        {
            Id = createdUser.Id,
            Username = createdUser.Username,
            Email = createdUser.Email,
            Nik = createdUser.Nik,
            PhoneNumber = createdUser.PhoneNumber
        };

        Log.Information("End User created successfully with PhoneNumber: {PhoneNumber}", userDto.PhoneNumber);

        return ResponseHelper.BuildResponse(
            HttpStatusCode.Accepted,
            _serviceId,
            Constans.ResponseCodes.APPROVED.Code,
            Constans.ResponseCodes.APPROVED.Description,
            responseData
        );
    }


    public Task<BaseResponse> UpdateUserAsync(Guid id, UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> DeleteUserAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<BaseResponse> AuthenticateAsync(string phoneNumber, string password)
    {
        throw new NotImplementedException();
    }
    
    //method untuk hashing password
    private string HashPassword(string password)
    {
        using var sha256 = System.Security.Cryptography.SHA256.Create();
        var bytes = System.Text.Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}