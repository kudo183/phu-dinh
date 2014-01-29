using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChuyenHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChuyenHang> GetData(PhuDinhEntities context,
            Expression<Func<tChuyenHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChuyenHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChuyenHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChuyenHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChuyenHang> data, List<tChuyenHang> origData)
        {
            return Repository<tChuyenHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tChuyenHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChuyenHang, bool>> filter)
        {
            return Repository<tChuyenHang>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
