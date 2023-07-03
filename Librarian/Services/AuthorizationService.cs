using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Librarian.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPasswordHashingService _hashingService;
        private readonly IRepository<Employee> _employeesRepository;
        private readonly IUserDialogService _userDialogService;

        public AuthorizationService(
            IPasswordHashingService hashingService, 
            IRepository<Employee> employeesRepository,
            IUserDialogService userDialogService)
        {
            _hashingService = hashingService;
            _employeesRepository = employeesRepository;
            _userDialogService = userDialogService;
        }

        public async Task<Employee?> LoginAsync(LoginRequest? loginRequest)
        {
            if (_employeesRepository.Entities is null) throw new ArgumentNullException(nameof(_employeesRepository.Entities));
            if (loginRequest?.Login is null || loginRequest?.Password is null) return null;

            var employee = await _employeesRepository.Entities.SingleOrDefaultAsync(e => e.Login == loginRequest.Login);

            if (employee is null)
            {
                _userDialogService.Error("Username isn`t corrent", "Incorrect data");
                return null;
            }
            
            if (!_hashingService.Verify(employee?.Password, loginRequest.Password))
                _userDialogService.Error("Password isn`t corrent", "Incorrect data");

            return employee;
        }

        public async Task<Employee?> RegisterAsync(RegisterRequest? registerRequest)
        {
            if (_employeesRepository.Entities is null) throw new ArgumentNullException(nameof(_employeesRepository.Entities));
            if (registerRequest?.Login is null || registerRequest?.Password is null) 
                throw new ArgumentNullException("Login or password contains a null reference"); ;

            var passwordHash = _hashingService.Hash(registerRequest.Password);

            var employee = new Employee { Login = registerRequest.Login, Password = passwordHash };

            return await _employeesRepository.AddAsync(employee);
        }
    }
}
