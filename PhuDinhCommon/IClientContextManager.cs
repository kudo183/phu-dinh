﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PhuDinhCommon
{
    public interface IClientContextManager<T> where T : Common.BindableObject
    {
        void CreateContext();
        List<T1> GetData<T1>(Expression<Func<T1, bool>> filter) where T1 : Common.BindableObject;
        List<T> GetData(Expression<Func<T, bool>> filter, int pageSize, int currentPageIndex, int itemCount);
        void UndoChange();
        void Dispose();
        void Save(List<T> data, List<T> originalData);
        int GetDataCount(Expression<Func<T, bool>> filter);
        void ReloadEntity(T entity);
    }
}
