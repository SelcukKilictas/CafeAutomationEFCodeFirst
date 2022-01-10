using CafeAutomationCodeFirst.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeAutomationCodeFirst.Repository.Abstracts
{
    public interface IRepository<T, in TId> where T : BaseEntity
    {
        T GetById(TId id);
        IQueryable<T> Get(Func<T, bool> predicate = null);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
        int Save();
    }
}
