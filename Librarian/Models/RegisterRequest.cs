namespace Librarian.Models
{
    public class RegisterRequest
    {
        public string? Login { get; set; }

        public string? Password { get; set; }

        public string? PermissionLevel { get; set; }
    }
}
