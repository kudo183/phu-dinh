using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenKhoRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChuyenKho, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChuyenKho> GetData(PhuDinhEntities context,
            Expression<Func<tChuyenKho, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChuyenKho>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChuyenKho> GetData(PhuDinhEntities context, Expression<Func<tChuyenKho, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChuyenKho>.ChangedItemData> Save(PhuDinhEntities context, List<tChuyenKho> data, List<tChuyenKho> origData)
        {
            return Repository<tChuyenKho>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tChuyenKho> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChuyenKho, bool>> filter)
        {
            return Repository<tChuyenKho>.GetData(context, filter).OrderByDescending(p => p.Ngay);
        }
    }
}
