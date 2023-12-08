using CRM.Interfaces;

namespace CRM.DAL.Entities.Base
{
    public abstract class Entity : IEntity
    {
        public int Id { get; set; }
    }
}
