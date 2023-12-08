using CRM.DAL.Entities.Base;
using CRM.Interfaces;

namespace CRM.DAL.Entities
{
    public class Shipper : NamedEntity, IArchivable
    {
        public string? ContactNumber { get; set; }

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name}";
    }
}
