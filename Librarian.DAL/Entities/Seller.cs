using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Seller : Person
    {
        public override string ToString() => $"Seller {Name} {Surname} {Patronymic}";
    }
}
