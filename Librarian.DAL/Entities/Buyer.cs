using Librarian.DAL.Entities.Base;

namespace Librarian.DAL.Entities
{
    public class Buyer : Person
    {
        public override string ToString() => $"Buyer {Name} {Surname} {Patronymic}";
    }
}
