using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.mastama.Models;

[Table("tbl_users")]
public class Users
{
    public Guid Id { get; set; }
    
    [StringLength(255, ErrorMessage = "Username must be between 3 and 255 characters.", MinimumLength = 3)]
    public string Username { get; set; } = string.Empty;
    
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    [StringLength(255, ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; } = string.Empty;

    [StringLength(16, ErrorMessage = "NIK must be between 10 and 16 characters.", MinimumLength = 10)]
    public string Nik { get; set; } = string.Empty;

    [StringLength(50, ErrorMessage = "Password must be between 8 and 50 characters.", MinimumLength = 8)]
    public string Password { get; set; } = string.Empty;
    
    [StringLength(15, ErrorMessage = "Phone number must be between 10 and max 15 characters.", MinimumLength = 10)]
    public string PhoneNumber { get; set; } = string.Empty;

    [StringLength(10, ErrorMessage = "Role must be max exactly 10 characters.", MinimumLength = 3)]
    public string Role { get; set; } = "User"; // Default Role
    public int Status { get; set; } = 1; // Default Status
}