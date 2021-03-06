﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhCommon
{
    public interface IClientContext
    {
        IQueryable<T> GetData<T>(Expression<Func<T, bool>> filter) where T : Common.BindableObject;
        IQueryable<T> GetDataWithRelated<T>(Expression<Func<T, bool>> filter, List<string> related) where T : Common.BindableObject;
        int Count<T>(IQueryable<T> query) where T : Common.BindableObject;
        void AddOrUpdateEntity<T>(T entity) where T : Common.BindableObject;
        void AddOrUpdateEntities<T>(List<T> entities) where T : Common.BindableObject;
        void DeleteEntity<T>(T entity) where T : Common.BindableObject;
        void DeleteEntities<T>(List<T> entities) where T : Common.BindableObject;
        //void AddUpdateDeleteEntities<T>(List<T> addOrUpdateEntities, List<T> deleteEntities) where T : Common.BindableObject;
    }
}
