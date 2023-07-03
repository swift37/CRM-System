namespace Librarian.Interfaces
{
    public interface IRepository<T> where  T : class, IEntity, IArchivable, new()
    {
        bool AutoSaveChanges { get; set; }

        IQueryable<T>? Entities { get; }

        T? Get(int id);

        Task<T?> GetAsync(int id, CancellationToken cancellation = default);

        T? Add(T entity);

        Task<T?> AddAsync(T entity, CancellationToken cancellation = default);

        void Update(T entity);

        Task UpdateAsync(T entity, CancellationToken cancellation = default);

        void Archive(T entity);

        Task ArchiveAsync(T entity);

        void UnArchive(T entity);

        Task UnArchiveAsync(T entity);

        void Remove(int id);

        Task RemoveAsync(int id, CancellationToken cancellation = default);

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
