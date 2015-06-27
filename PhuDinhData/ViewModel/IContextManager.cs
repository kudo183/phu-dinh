using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhData
{
    public interface IContextManager
    {
        void CreateContext();
        List<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject;
        List<T> GetData<T>(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount) where T : Common.BindableObject;
        void UndoChange();
        void Dispose();
        List<Repository.Repository<T>.ChangedItemData> Save<T>(List<T> data, List<T> originalData) where T : Common.BindableObject;
        int GetDataCount<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject;
    }
}
