using com.mastama.Models;

namespace com.mastama.Repositories.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Users>> GetAllAsync(); //getall data
    Task<Users?> GetByIdAsync(Guid id); //get data by id
    Task<Users?> AddAsync(Users user); //create user
    Task<Users?> UpdateAsync(Users user); //update user
    Task<Users?> DeleteAsync(Guid id);
    Task<Users?> AuthenticateAsync(string phoneNumber, string password);

    Task<Users?> FindByPhoneNumberOrEmailOrNik(string phoneNumber, string email, string nik);
    

}