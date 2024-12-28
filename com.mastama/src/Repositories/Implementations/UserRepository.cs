using com.mastama.Models;
using com.mastama.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace com.mastama.Repositories.Implementations;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    // constructor
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Users>> GetAllAsync() => await _context.Users.ToListAsync();

    public async Task<Users?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<Users?> AddAsync(Users user)
    {
        await _context.Users.AddAsync(user); //Menambahkan user kedalam konteks
        await _context.SaveChangesAsync(); //simpan penambahan ke db
        return user; //balikan data user yang ditambahkan
    }

    public async Task<Users?> UpdateAsync(Users user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<Users?> DeleteAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
        return null;
    }

    public async Task<Users?> AuthenticateAsync(string phoneNumber, string password)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber && u.Password == password);
    }

    public async Task<Users?> FindByPhoneNumberOrEmailOrNik(string phoneNumber, string email, string nik)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email || u.PhoneNumber == phoneNumber || u.Nik == nik);
    }
}