using CRM.DAL.Entities;
using CRM.Infrastructure.DebugServices;
using CRM.Interfaces;
using CRM.Models;
using CRM.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CRM.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IPasswordHashingService _hashingService;
        private readonly IRepository<Employee> _employeesRepository;

        public AuthorizationService() : this(
            new PasswordHashingService(), 
            new DebugEmployeesRepository())
        {
            if (!App.IsDesignMode)
                throw new InvalidOperationException(nameof(App.IsDesignMode));
        }

        public AuthorizationService(
            IPasswordHashingService hashingService, 
            IRepository<Employee> employeesRepository)
        {
            _hashingService = hashingService;
            _employeesRepository = employeesRepository;
        }

        public async Task<Employee?> LoginAsync(LoginRequest? loginRequest)
        {
            if (_employeesRepository.Entities is null) throw new ArgumentNullException(nameof(_employeesRepository.Entities));

            if (loginRequest?.Login is null || loginRequest?.Password is null)
                throw new Exception("Invalid login or password.");

            var employee = await _employeesRepository.Entities.SingleOrDefaultAsync(e => e.Login == loginRequest.Login);

            if (employee is null)
                throw new Exception("Username isn`t corrent.");
            
            if (!_hashingService.Verify(employee?.Password, loginRequest.Password))
                throw new Exception("Password isn`t corrent.");

            return employee;
        }

        public async Task<Employee?> RegisterAsync(RegisterRequest? registerRequest)
        {
            if (_employeesRepository.Entities is null) throw new ArgumentNullException(nameof(_employeesRepository.Entities));

            if (registerRequest?.Login is null || registerRequest?.Password is null) 
                throw new Exception("Invalid login or password.");

            var passwordHash = _hashingService.Hash(registerRequest.Password);

            var employee = new Employee { 
                Name = registerRequest.Name,
                Surname = registerRequest.Surname,
                Title = registerRequest.Title,
                Login = registerRequest.Login, 
                Password = passwordHash,
                PermissionLevel = registerRequest.PermissionLevel,
                DateOfBirth = registerRequest.DateOfBirth,
                HireDate = registerRequest.HireDate,
                Extension = registerRequest.Extension,
                ContactNumber = registerRequest.ContactNumber,
                ContactMail = registerRequest.ContactMail,
                Address = registerRequest.Address,
                IdentityDocumentNumber = registerRequest.IdentityDocumentNumber,
                WorkingRate = registerRequest.WorkingRate
            };

            return await _employeesRepository.AddAsync(employee);
        }
    }
}
