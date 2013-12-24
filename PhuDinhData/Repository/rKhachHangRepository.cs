using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class rKhachHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<rKhachHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<rKhachHang> GetData(PhuDinhEntities context,
            Expression<Func<rKhachHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<rKhachHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<rKhachHang> GetData(PhuDinhEntities context, Expression<Func<rKhachHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<rKhachHang>.ChangedItemData> Save(PhuDinhEntities context, List<rKhachHang> data, List<rKhachHang> origData)
        {
            return Repository<rKhachHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<rKhachHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<rKhachHang, bool>> filter)
        {
            return Repository<rKhachHang>.GetData(context, filter).OrderBy(p => p.TenKhachHang);
        }
    }
}
