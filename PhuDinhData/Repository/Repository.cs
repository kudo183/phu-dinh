using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class Repository<T> where T : class
    {
        private static void RemoveItem(PhuDinhEntities context, List<T> gridDataSource, Expression<Func<T, bool>> filter, Func<T, T, bool> CompareFunc)
        {
            foreach (var item in GetData(context, filter))
            {
                var entity = gridDataSource.FirstOrDefault(p => CompareFunc(item, p));
                if (entity == null)
                {
                    context.Set<T>().Remove(item);
                }
            }
        }

        private static void AddNewItem(PhuDinhEntities context, List<T> gridDataSource, Func<T, bool> CheckNewItemFunc)
        {
            foreach (var item in gridDataSource)
            {
                if (CheckNewItemFunc(item))
                {
                    context.Set<T>().Add(item);
                }
            }
        }

        public static List<T> GetData(PhuDinhEntities context, Expression<Func<T, bool>> filter)
        {
            return context.Set<T>().Where(filter).ToList();
        }

        public static List<DbEntityEntry<T>> Save(PhuDinhEntities context, List<T> data, Expression<Func<T, bool>> filter, Func<T, bool> CheckNewItemFunc, Func<T, T, bool> CompareFunc)
        {
            RemoveItem(context, data, filter, CompareFunc);
            AddNewItem(context, data, CheckNewItemFunc);
            var changed = context.ChangeTracker.Entries<T>().ToList();
            context.SaveChanges();
            return changed;
        }
    }
}
