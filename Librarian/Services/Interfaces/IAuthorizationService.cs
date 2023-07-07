using Librarian.DAL.Entities;
using Librarian.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Librarian.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<Employee?> LoginAsync(LoginRequest? loginRequest);

        Task<Employee?> RegisterAsync(RegisterRequest? registerRequest);
    }
}
