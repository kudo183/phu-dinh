using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
            var context = ContextFactory.CreateContext();
            context.Set<T>().Attach(entity);
            context.Entry(entity).State = entity.GetKey() == 0 ? EntityState.Added : EntityState.Modified;

            EntityFrameworkUtils.DetachAllUnchangedEntity(context);

            context.SaveChanges();
        }

        public void AddOrUpdateEntities<T>(List<T> entities) where T : BindableObject
        {
            var context = ContextFactory.CreateContext();
            foreach (var entity in entities)
            {

                context.Set<T>().Attach(entity);
                context.Entry(entity).State = entity.GetKey() == 0 ? EntityState.Added : EntityState.Modified;

                EntityFrameworkUtils.DetachAllUnchangedEntity(context);
            }

            context.SaveChanges();
        }

        #endregion
    }
}
