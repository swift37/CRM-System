using CRM.DAL.Entities;
using CRM.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CRM.Services.Interfaces
{
    public interface IAuthorizationService
    {
        Task<Employee?> LoginAsync(LoginRequest? loginRequest);

        Task<Employee?> RegisterAsync(RegisterRequest? registerRequest);
    }
}
