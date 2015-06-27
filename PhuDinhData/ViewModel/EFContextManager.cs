using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhDataEntity;

namespace PhuDinhData
{
    public sealed class EFContextManager : IContextManager
    {
        private PhuDinhEntities _context;

        public void CreateContext()
        {
            Dispose();

            _context = ContextFactory.CreateContext();
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            if (_context == null)
            {
                _context = ContextFactory.CreateContext();
            }

            return Repository.Repository<T>.GetDataQuery(_context, filter).ToList();
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount) where T : Common.BindableObject
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

        public List<Repository.Repository<T>.ChangedItemData> Save<T>(List<T> data, List<T> originalData) where T : Common.BindableObject
        {
            return Repository.Repository<T>.Save(_context, data, originalData);
        }

        public int GetDataCount<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            return Repository.Repository<T>.GetDataCount(_context, filter);
        }
    }
}
