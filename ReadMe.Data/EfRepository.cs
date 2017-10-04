using ReadMe.Data.Contracts;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace ReadMe.Data
{
    public class EfRepository<T> : IEfRepository<T>
        where T : class
    {
        private readonly ReadMeDbContext dbContext;

        public EfRepository(ReadMeDbContext dbContext)
        {
            if(dbContext == null)
            {
                throw new ArgumentNullException("DbContext cannot be null");
            }

            this.dbContext = dbContext;
        }

        public IQueryable<T> All
        {
            get
            {
                // TODO where clause
                return this.dbContext.DbSet<T>();
            }
        }

        public IQueryable<T> AllAndDeleted
        {
            get
            {
                return this.dbContext.DbSet<T>();
            }
        }

        public T GetById(object id)
        {
            return this.dbContext.DbSet<T>().Find(id);
        }

        public void Add(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            DbEntityEntry entry = this.dbContext.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
