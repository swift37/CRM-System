﻿using Librarian.DAL.Entities;

namespace Librarian.Models
{
    public class TopCategoryInfo
    {
        public Category? Category { get; set; }

        public int OrdersCount { get; set; }

        public decimal OrdersAmount { get; set; }
    }
}
