using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhoHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rKhoHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rKhoHang> GetData(PhuDinhEntities context,
            Expression<Func<rKhoHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rKhoHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rKhoHang> GetData(PhuDinhEntities context, Expression<Func<rKhoHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rKhoHang>.ChangedItemData> Save(PhuDinhEntities context, List<rKhoHang> data, List<rKhoHang> origData)
        {
            return Repository<rKhoHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rKhoHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rKhoHang, bool>> filter)
        {
            return Repository<rKhoHang>.GetData(context, filter).OrderBy(p => p.TenKho);
        }
    }
}
