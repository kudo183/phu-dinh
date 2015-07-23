using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Linq;
using System.Linq.Expressions;
using Common;
using PhuDinhCommon;

namespace PhuDinhODataClientContext
{
    public class ODataContext : IClientContext
    {
        #region Implementation of IClientContext

        public IQueryable<T> GetData<T>(Expression<Func<T, bool>> filter) where T : BindableObject
        {
            var context = ContextFactory.CreateContext();

            var query = (filter == null) ? context.Set<T>() : context.Set<T>().Where(filter);

            return query;
        }

        public IQueryable<T> GetDataWithRelated<T>(Expression<Func<T, bool>> filter, List<string> related) where T : BindableObject
        {
            var query = GetData(filter) as DataServiceQuery<T>;

            query = related.Aggregate(query, (current, path) => current.Expand(path.Replace(".", "/")));

            return query;
        }

        public int Count<T>(IQueryable<T> query) where T : BindableObject
        {
            var q = (query as DataServiceQuery<T>).IncludeTotalCount().Take(1) as DataServiceQuery;

            var response = q.Execute() as QueryOperationResponse<T>;

            return (int)response.TotalCount;
        }

        public void AddOrUpdateEntity<T>(T entity) where T : BindableObject
        {
            var context = ContextFactory.CreateContext();
            if (entity.IsNewItem())
            {
                context.Add(entity);
            }
            else
            {
                context.Update(entity);
            }

            context.SaveChanges();
        }

        public void AddOrUpdateEntities<T>(List<T> entities) where T : BindableObject
        {
            var context = ContextFactory.CreateContext();
            foreach (var entity in entities)
            {

                if (entity.IsNewItem())
                {
                    context.Add(entity);
                }
                else
                {
                    context.Update(entity);
                }
            }

            context.SaveChanges();
        }

        public void DeleteEntity<T>(T entity) where T : BindableObject
        {
            throw new NotImplementedException();
        }

        public void DeleteEntities<T>(List<T> entities) where T : BindableObject
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
