using FluentValidation;
using Librarian.DAL.Entities;
using Librarian.Interfaces;
using Librarian.Models;
using Librarian.Services.Interfaces;
using Librarian.Tools;
using Microsoft.VisualBasic.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace Librarian.Services.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Login).Login();
            RuleFor(x => x.Password).Password();
        }
    }
}
