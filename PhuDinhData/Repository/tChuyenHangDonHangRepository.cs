using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChuyenHangDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChuyenHangDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tChuyenHangDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChuyenHangDonHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChuyenHangDonHang>.ChangedItemData> Save(PhuDinhEntities context,
            List<tChuyenHangDonHang> data, List<tChuyenHangDonHang> origData)
        {
            return Repository<tChuyenHangDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }

        private static IQueryable<tChuyenHangDonHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChuyenHangDonHang>.GetData(context, filter).
                 OrderByDescending(p => p.tChuyenHang.Ngay).
                 ThenByDescending(p => p.tChuyenHang.Gio);
        }
    }
}
