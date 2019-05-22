using Fthdgn.LibraryManager.Entities;
using Fthdgn.LibraryManager.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Fthdgn.LibraryManager.Repositories
{
    public class Repository<TDbContext, TEntity>
            where TDbContext : DbContext
            where TEntity : Entity
    {
        public TDbContext Context { get; protected set; }
        public Repository(TDbContext context)
        {
            Context = context;
        }

        public DbSet<TEntity> Set => Context?.Set<TEntity>();
        public virtual IEnumerable<TEntity> Filter(IEnumerable<TEntity> query, bool includeDeactivated = false, bool includeRemoved = false) => query?.Where(x => includeDeactivated || x.IsEnabled)?.Where(x => includeRemoved || x.RemovedAt == null);
        public virtual IQueryable<TEntity> Filter(IQueryable<TEntity> query, bool includeDeactivated = false, bool includeRemoved = false) => query?.Where(x => includeDeactivated || x.IsEnabled)?.Where(x => includeRemoved || x.RemovedAt == null);
        public virtual IQueryable<TEntity> Query(bool includeDeactivated = false, bool includeRemoved = false) => Filter(Set.AsQueryable(), includeDeactivated, includeRemoved);
        public virtual IEnumerable<TEntity> Get(bool includeDeactivated = false, bool includeRemoved = false) => Query(includeDeactivated, includeRemoved)?.AsEnumerable();
        public virtual IEnumerable<TEntity> Get() => Get(includeDeactivated: false, includeRemoved: false);
        public virtual TEntity Get(string key) => Set?.Find(key);
        public virtual IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> exp) => Query().Where(exp);
        public virtual TEntity Single(Expression<Func<TEntity, bool>> exp, bool includeDeactivated = false, bool includeRemoved = false) => Query(includeDeactivated, includeRemoved).FirstOrDefault(exp);

        public virtual TEntity Add(TEntity entity) => Set?.Add(entity);
        public virtual TEntity Update(TEntity entity) => entity.With(x => x.ChangedAt = DateTimeOffset.Now);
        public virtual TEntity Remove(TEntity entity) => entity.With(x => x.RemovedAt = DateTimeOffset.Now);
        public virtual TEntity Purge(TEntity entity) => Set?.Remove(entity);
        public virtual TEntity Resurrect(string key) => Get(key).With(x => x.RemovedAt = null);

        public virtual TEntity Activate(string key) => Activate(Get(key));
        public virtual TEntity Activate(TEntity entity) => entity.With(x => x.IsEnabled = true);
        public virtual TEntity Deactivate(string key) => Deactivate(Get(key));
        public virtual TEntity Deactivate(TEntity entity) => entity.With(x => x.IsEnabled = false);

        public virtual void Save() => Context?.SaveChanges();
    }
}
