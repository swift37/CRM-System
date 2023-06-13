using Librarian.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.Models
{
    public class TopSellerInfo
    {
        public Employee? Seller { get; set; }

        public int DealsCount { get; set; }

        public decimal DealsAmount { get; set; }
    }
}
