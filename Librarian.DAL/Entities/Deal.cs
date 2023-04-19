﻿using Librarian.DAL.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Librarian.DAL.Entities
{
    public class Deal : Entity
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public virtual Book? Book { get; set; }

        public virtual Seller? Seller { get; set; }

        public virtual Buyer? Buyer { get; set; }
    }
}
