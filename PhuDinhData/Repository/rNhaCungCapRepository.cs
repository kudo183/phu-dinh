﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rNhaCungCapRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rNhaCungCap, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rNhaCungCap> GetData(PhuDinhEntities context,
            Expression<Func<rNhaCungCap, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rNhaCungCap>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rNhaCungCap> GetData(PhuDinhEntities context, Expression<Func<rNhaCungCap, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rNhaCungCap>.ChangedItemData> Save(PhuDinhEntities context, List<rNhaCungCap> data, List<rNhaCungCap> origData)
        {
            return Repository<rNhaCungCap>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rNhaCungCap> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rNhaCungCap, bool>> filter)
        {
            return Repository<rNhaCungCap>.GetData(context, filter).OrderBy(p => p.TenNhaCungCap);
        }
    }
}
