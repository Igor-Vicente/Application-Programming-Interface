using Business.Layer.Interfaces;
using Business.Layer.Models;
using Data.Layer.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Layer.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;
        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = Db.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(Guid id) => await DbSet.FindAsync(id);

        public async Task<IEnumerable<TEntity>> SearchAsync(Expression<Func<TEntity, bool>> predicate) =>
            await DbSet.AsNoTracking().Where(predicate).ToListAsync();

        public async Task AddAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await SaveChangesAsync();
        }
        public async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChangesAsync();
        }

        public async Task<int> SaveChangesAsync() => await Db.SaveChangesAsync();

        public void Dispose() => Db.Dispose();
    }
}
