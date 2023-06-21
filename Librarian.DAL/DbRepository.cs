using Librarian.DAL.Context;
using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, IArchivable, new()
    {
        private readonly LibrarianDb _dbContext;
        private readonly DbSet<T> _dbSet;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(LibrarianDb context)
        {
            _dbContext = context;
            _dbSet = _dbContext.Set<T>();
        }

        public virtual IQueryable<T>? Entities => _dbSet;

        public T? Get(int id) => Entities?.SingleOrDefault(item => item.Id == id); 

        public async Task<T?>? GetAsync(int id, CancellationToken cancellation = default)
        {
            if (Entities is null) throw new ArgumentNullException(nameof(Entities));

            return await Entities.SingleOrDefaultAsync(item => item.Id == id, cancellation).ConfigureAwait(false);
        }
            

        public T? Add(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));
            
            _dbContext.Entry(entity).State = EntityState.Added;
            if (AutoSaveChanges) 
                _dbContext.SaveChanges();
            return entity;
        }

        public async Task<T?>? AddAsync(T entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Added;
            if (AutoSaveChanges)
                await _dbContext.SaveChangesAsync(cancellation).ConfigureAwait(false);
            return entity;
        }

        public void Archive(T entity)
        {
            entity.IsActual = false;

            Update(entity);
            if (AutoSaveChanges)
                _dbContext.SaveChanges();
        }

        public async Task ArchiveAsync(T entity)
        {
            entity.IsActual = false;

            await UpdateAsync(entity);
        }

        public void UnArchive(T entity)
        {
            entity.IsActual = true;

            Update(entity);
            if (AutoSaveChanges)
                _dbContext.SaveChanges();
        }

        public async Task UnArchiveAsync(T entity)
        {
            entity.IsActual = true;

            await UpdateAsync(entity);
        }

        public void Remove(int id)
        {
            var entity = _dbSet.Local.FirstOrDefault(entity => entity.Id == id) ?? new T { Id = id };

            _dbContext.Remove(entity);
            if (AutoSaveChanges)
                _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            var entity = _dbSet.Local.FirstOrDefault(entity => entity.Id == id) ?? new T { Id = id };

            _dbContext.Remove(entity);
            if (AutoSaveChanges)
                await _dbContext.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public void Update(T entity)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Modified;
            if (AutoSaveChanges)
                _dbContext.SaveChanges();
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellation = default)
        {
            if (entity is null) throw new ArgumentNullException(nameof(entity));

            _dbContext.Entry(entity).State = EntityState.Modified;
            if (AutoSaveChanges)
                await _dbContext.SaveChangesAsync(cancellation).ConfigureAwait(false);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}
