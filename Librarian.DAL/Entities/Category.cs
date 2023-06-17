using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;

namespace Librarian.DAL.Entities
{
    public class Category : NamedEntity, IArchivable
    {
        public virtual ICollection<Product?>? Products { get; set; } = new HashSet<Product?>();

        public bool IsActual { get; set; } = true;

        public override string ToString() => $"{Name}";
    }
}
