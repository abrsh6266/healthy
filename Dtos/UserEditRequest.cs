using System.ComponentModel.DataAnnotations;

namespace Auth.Dtos
{
    public class UserEditRequest
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        public string? UserName { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }
    }
}
