﻿using System.ComponentModel.DataAnnotations;

namespace Librarian.DAL.Entities.Base
{
    public class NamedEntity : Entity
    {
        [Required]
        public string? Name { get; set; }
    }
}
