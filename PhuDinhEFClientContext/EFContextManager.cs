using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Common;
using PhuDinhCommon;
using PhuDinhDataEntity;

namespace PhuDinhEFClientContext
{
    public sealed class EFContextManager<T> : IClientContextManager<T> where T : Common.BindableObject
    {
        private readonly EFCache _cache = new EFCache();
        private readonly EFContext _context = new EFContext();

        public void CreateContext()
        {
        }

        public List<T1> GetData<T1>(Expression<Func<T1, bool>> filter, List<string> relatedTables) where T1 : Common.BindableObject
        {
            var dbQuery = Repository.RepositoryLocator<T1>.OrderBy(_context.GetDataWithRelated(filter, relatedTables));

            if (filter != null)
                return dbQuery.ToList();

            relatedTables.Add(typeof(T1).Name);
            var lastUpdate = _context.GetTablesLastUpdate(relatedTables);
            var key = relatedTables.OrderBy(p => p).Aggregate((p, p1) => p + "." + p1);

            return _cache.GetData(dbQuery, key, lastUpdate);
        }

        public List<T> GetData(Expression<Func<T, bool>> filter, List<string> relatedTables, int pageSize, int currentPageIndex, int itemCount)
        {
            var dbQuery = Repository.RepositoryLocator<T>.OrderBy(_context.GetDataWithRelated(filter, relatedTables));

            if (itemCount == 0)
            {
                return new List<T>();
            }

            if (pageSize > itemCount || pageSize == 0)
            {
                return dbQuery.ToList();
            }

            var skippedItem = pageSize * (currentPageIndex - 1);

            var takeItem = itemCount - skippedItem;
            if (takeItem > pageSize)
            {
                takeItem = pageSize;
            }

            if (takeItem <= 0)
            {
                return new List<T>();
            }

            return dbQuery.Skip(skippedItem).Take(takeItem).ToList();
        }

        public void UndoChange()
        {

        }

        public void Dispose()
        {
        }

        public void Save(List<T> data, List<T> originalData)
        {
            var context = ContextFactory.CreateContext();
            Repository.Repository<T>.Save(context, data, originalData);
        }

        public int GetDataCount(Expression<Func<T, bool>> filter)
        {
            return _context.Count(_context.GetData(filter));
        }
    }
}
