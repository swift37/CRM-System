using Librarian.DAL.Entities.Base;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.DAL.Entities
{
    public class Shipper : NamedEntity
    {
        public string? ContactNumber { get; set; }

        public bool IsActual { get; set; }
    }
}
