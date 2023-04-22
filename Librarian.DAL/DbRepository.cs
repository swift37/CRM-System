﻿using Librarian.DAL.Context;
using Librarian.DAL.Entities.Base;
using Librarian.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Librarian.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
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

        public void Remove(int id)
        {
            //var entity = Get(id);
            //if(entity is null) return;
            //_dbContext.Entry(entity);
            _dbContext.Remove(new T { Id = id});
            if (AutoSaveChanges)
                _dbContext.SaveChanges();
        }

        public async Task RemoveAsync(int id, CancellationToken cancellation = default)
        {
            _dbContext.Remove(new T { Id = id });
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
    }
}