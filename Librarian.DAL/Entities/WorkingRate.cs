using Librarian.DAL.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Librarian.DAL.Entities
{
    public class WorkingRate : NamedEntity
    {
        public int HoursPerMonth { get; set; }

        public string? Description { get; set; }
    }
}
