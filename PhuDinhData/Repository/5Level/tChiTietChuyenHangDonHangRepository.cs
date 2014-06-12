using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static int GetDataCount(PhuDinhEntities context
            , Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).Count();
        }

        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context,
            Expression<Func<tChiTietChuyenHangDonHang, bool>> filter,
            int pageSize, int currentPageIndex, int itemCount)
        {
            return Repository<tChiTietChuyenHangDonHang>.PagingData(GetDataQuery(context, filter)
                , pageSize, currentPageIndex, itemCount);
        }

        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context
            , Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return GetDataQuery(context, filter).ToList();
        }

        public static List<Repository<tChiTietChuyenHangDonHang>.ChangedItemData> Save(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> data, List<tChiTietChuyenHangDonHang> origData)
        {
            var changed = Repository<tChiTietChuyenHangDonHang>.Save(context, data, origData, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));

            return changed;
        }

        private static IQueryable<tChiTietChuyenHangDonHang> GetDataQuery(PhuDinhEntities context
            , Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio);
        }
    }
}
