using CustomerOrderTracking.Core.DataAccess;
using CustomerOrderTracking.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

                                                                                                      
namespace CustomerOrderTracking.DataAccess
{
    public class Repository<T> : RepositoryBase, IDataAccess<T> where T : class
    {
        private DbSet<T> _objectSet;

        public Repository()
        {
            _objectSet = context.Set<T>();
        }

        public List<T> List()
        {
            return _objectSet.ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return _objectSet.AsQueryable<T>();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return _objectSet.Where(where).ToList();
        }

        public int Insert(T obj)
        {
            _objectSet.Add(obj);

            if (obj is BaseEntity)
            {
                BaseEntity o = obj as BaseEntity;
                DateTime now = DateTime.Now;

                o.CreatedOn = now;
                o.ModifiedOn = now;
            }

            return Save();
        }

        public int Update(T obj)
        {
            if (obj is BaseEntity)
            {
                BaseEntity o = obj as BaseEntity;

                o.ModifiedOn = DateTime.Now;
            }

            return Save();
        }

        public int Delete(T obj)
        {
            _objectSet.Remove(obj);
            return Save();
        }

        public int Save()
        {
            return context.SaveChanges();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return _objectSet.FirstOrDefault(where);
        }
    }
}
