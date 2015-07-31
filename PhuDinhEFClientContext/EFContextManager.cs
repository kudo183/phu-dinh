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
        private PhuDinhEntities _context;

        public void CreateContext()
        {
            Dispose();

            _context = ContextFactory.CreateContext();
        }

        public List<T1> GetData<T1>(Expression<Func<T1, bool>> filter) where T1 : Common.BindableObject
        {
            if (_context == null)
            {
                _context = ContextFactory.CreateContext();
            }

            var relatedTable = new List<string>();
            var dbQuery = Repository.Repository<T1>.GetDataQueryAndRelatedTables(_context, filter, ref relatedTable);

            if (filter != null)
                return dbQuery.ToList();

            var lastUpdate = EntityFrameworkUtils.GetTablesLastUpdate(_context, relatedTable);
            var key = relatedTable.OrderBy(p => p).Aggregate((p, p1) => p + "." + p1);

            return _cache.GetData(dbQuery, key, lastUpdate);
        }

        public List<T> GetData(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount)
        {
            if (_context == null)
            {
                _context = ContextFactory.CreateContext();
            }

            return Repository.Repository<T>.GetData(_context, filter, pageSize, currentPageIndex, itemCount);
        }

        public void UndoChange()
        {
            PhuDinhCommon.EntityFrameworkUtils.UndoContextChange(_context, EntityState.Modified);
        }

        public void Dispose()
        {
            if (_context == null)
                return;

            _context.Dispose();
            _context = null;
        }

        public void Save(List<T> data, List<T> originalData)
        {
            Repository.Repository<T>.Save(_context, data, originalData);
        }

        public int GetDataCount(Expression<Func<T, bool>> filter)
        {
            if (_context == null)
            {
                _context = ContextFactory.CreateContext();
            }

            return Repository.Repository<T>.GetDataCount(_context, filter);
        }

        public void ReloadEntity(T entity)
        {
            var entityEntry = _context.Entry(entity);
            if (entityEntry.State == EntityState.Detached)
                entityEntry.State = EntityState.Unchanged;

            entityEntry.Reload();

            PhuDinhCommon.EntityFrameworkUtils.DetachAllUnchangedEntity(_context);
        }
    }
}
