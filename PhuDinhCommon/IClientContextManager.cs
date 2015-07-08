using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhCommon
{
    public interface IClientContextManager
    {
        void CreateContext();
        List<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject;
        List<T> GetData<T>(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount) where T : Common.BindableObject;
        void UndoChange();
        void Dispose();
        void Save<T>(List<T> data, List<T> originalData) where T : Common.BindableObject;
        int GetDataCount<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject;
        void ReloadEntity<T>(T entity) where T : Common.BindableObject;
    }
}
