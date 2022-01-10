using CafeAutomationCodeFirst.Data;
using CafeAutomationCodeFirst.Models.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Repository.Abstracts
{
    public abstract class RepositoryBase<T, TId> : IRepository<T, TId> where T : BaseEntity, new()
    {
        protected CafeContext cafeContext;
        public DbSet<T> Table { get; protected set; }

        protected RepositoryBase()
        {
            cafeContext = new CafeContext();
            Table = cafeContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            Table.Add(entity);
            this.Save();
        }

        public virtual IQueryable<T> Get(Func<T, bool> predicate = null)
        {
            return predicate == null ? Table : Table.Where(predicate).AsQueryable();
        }

        public virtual IQueryable<T> Get(string[] includes, Func<T, bool> predicate = null)
        {
            IQueryable<T> query = Table;
            foreach (var include in includes)
            {
                query = Table.Include(include);
            }
            return predicate == null ? query : query.Where(predicate).AsQueryable();
        }

        public virtual T GetById(TId id)
        {
            return Table.Find(id);
        }

        public virtual void Remove(T entity)
        {
            Table.Remove(entity);
            this.Save();
        }

        public virtual int Save()
        {
            return cafeContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            Table.Update(entity);
            this.Save();
        }
    }
}
