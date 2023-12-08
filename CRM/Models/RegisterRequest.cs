using CRM.DAL.Entities;
using System;

namespace CRM.Models
{
    public class RegisterRequest
    {
        public string? Name { get; set; }

        public string? Surname { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public int PermissionLevel { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime HireDate { get; set; }

        public string? Title { get; set; }

        public string? ContactNumber { get; set; }

        public string? ContactMail { get; set; }

        public string? Address { get; set; }

        public string? IdentityDocumentNumber { get; set; }

        public DateTime? Extension { get; set; }

        public WorkingRate? WorkingRate { get; set; }
    }
}
