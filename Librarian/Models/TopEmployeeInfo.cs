using Librarian.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Models
{
    public class TopEmployeeInfo
    {
        public Employee? Employee { get; set; }

        public int OrdersCount { get; set; }

        public decimal OrdersAmount { get; set; }
    }
}
