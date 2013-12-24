﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tNhapNguyenLieuRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tNhapNguyenLieu> GetData(PhuDinhEntities context,
            Expression<Func<tNhapNguyenLieu, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tNhapNguyenLieu>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tNhapNguyenLieu> GetData(PhuDinhEntities context, Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tNhapNguyenLieu>.ChangedItemData> Save(PhuDinhEntities context, List<tNhapNguyenLieu> data, List<tNhapNguyenLieu> origData)
        {
            return Repository<tNhapNguyenLieu>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tNhapNguyenLieu> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tNhapNguyenLieu, bool>> filter)
        {
            return Repository<tNhapNguyenLieu>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
