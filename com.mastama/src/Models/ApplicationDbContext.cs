using Microsoft.EntityFrameworkCore;

namespace com.mastama.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<Users> Users { get; set; }
}