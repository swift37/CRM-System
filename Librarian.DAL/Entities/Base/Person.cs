﻿namespace Librarian.DAL.Entities.Base
{
    public class Person : NamedEntity
    {
        public string? Surname { get; set; }

        public string? Patronymic { get; set; }

    }
}