using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using PhuDinhDataEntity;

namespace PhuDinhData
{
    public sealed class ODataContextManager : IContextManager
    {
        public void CreateContext()
        {
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            return null;
        }

        public List<T> GetData<T>(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount) where T : Common.BindableObject
        {
            return null;
        }

        public void UndoChange()
        {
        }

        public void Dispose()
        {
        }

        public List<Repository.Repository<T>.ChangedItemData> Save<T>(List<T> data, List<T> originalData) where T : Common.BindableObject
        {
            return null;
        }

        public int GetDataCount<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject
        {
            return 0;
        }
    }
}
