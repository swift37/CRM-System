using FluentValidation;
using CRM.DAL.Entities;
using CRM.Interfaces;
using CRM.Models;
using CRM.Services.Interfaces;
using CRM.Tools;
using Microsoft.VisualBasic.Logging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Security.Policy;
using System.Text.RegularExpressions;

namespace CRM.Services.Validators
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
