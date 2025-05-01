namespace ToolAppStudentsServer.DTO
{
    using System.ComponentModel.DataAnnotations;
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="The {0} must be at least {2} characters long",MinimumLength = 6)]
        public string Password { get; set; }
    }
}
