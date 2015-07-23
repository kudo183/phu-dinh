using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Common;
using PhuDinhCommon;

namespace PhuDinhEFClientContext
{
    public class EFContext : IClientContext
    {
        #region Implementation of IClientContext

        public IQueryable<T> GetData<T>(Expression<Func<T, bool>> filter) where T : BindableObject
        {
            var context = ContextFactory.CreateContext();

            var query = (filter == null) ? context.Set<T>().AsNoTracking() : context.Set<T>().AsNoTracking().Where(filter);

            return query;
        }

        public IQueryable<T> GetDataWithRelated<T>(Expression<Func<T, bool>> filter, List<string> related) where T : BindableObject
        {
            var query = GetData(filter);

            query = related.Aggregate(query, (current, path) => current.Include(path));

            return query;
        }

        public int Count<T>(IQueryable<T> query) where T : BindableObject
        {
            return query.Count();
        }

        public void AddOrUpdateEntity<T>(T entity) where T : BindableObject
        {
            AddOrUpdateEntities(new List<T> { entity });
        }

        public void AddOrUpdateEntities<T>(List<T> entities) where T : BindableObject
        {
            var cache = ClearNavigationProperties(entities);

            try
            {
                var context = ContextFactory.CreateContext();

                foreach (var entity in entities)
                {
                    context.Set<T>().Attach(entity);
                    context.Entry(entity).State = entity.IsNewItem() ? EntityState.Added : EntityState.Modified;
                }

                context.SaveChanges();
            }
            finally
            {
                SetNavigationProperties(cache);
            }
        }

        public void DeleteEntity<T>(T entity) where T : BindableObject
        {
            DeleteEntities(new List<T> { entity });
        }

        public void DeleteEntities<T>(List<T> entities) where T : BindableObject
        {
            var cache = ClearNavigationProperties(entities);

            try
            {
                var context = ContextFactory.CreateContext();

                foreach (var entity in entities)
                {
                    context.Set<T>().Attach(entity);
                    context.Entry(entity).State = EntityState.Deleted;
                }

                context.SaveChanges();
            }
            finally
            {
                SetNavigationProperties(cache);
            }
        }

        #endregion

        private Dictionary<PropertyInfo, Dictionary<T, object>> ClearNavigationProperties<T>(List<T> entities) where T : BindableObject
        {
            var virtualProperties = typeof(T).GetProperties().Where(p => p.IsVirtual()).ToList();

            var cache = new Dictionary<PropertyInfo, Dictionary<T, object>>();
            foreach (var property in virtualProperties)
            {
                if (property.CanRead == false || property.CanWrite == false)
                    continue;

                cache.Add(property, new Dictionary<T, object>());
                var cacheProperty = cache[property];
                foreach (var item in entities)
                {
                    cacheProperty.Add(item, property.GetValue(item));
                    property.SetValue(item, null);
                }
            }

            return cache;
        }

        private void SetNavigationProperties<T>(Dictionary<PropertyInfo, Dictionary<T, object>> cache) where T : BindableObject
        {
            foreach (var entry in cache)
            {
                foreach (var v in entry.Value)
                {
                    entry.Key.SetValue(v.Key, v.Value);
                }
            }
        }
    }
}
