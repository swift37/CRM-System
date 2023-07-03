namespace Librarian.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        string? Hash(string? password);

        bool Verify(string? passwordHash, string? InputPassword);
    }
}
