using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos{
    public class LoginRequest
    {
        [Required]
        public string Role { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
                        
        [Required, DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}