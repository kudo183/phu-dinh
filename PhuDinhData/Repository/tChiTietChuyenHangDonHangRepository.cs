using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PhuDinhData.Repository
{
    public static class tChiTietChuyenHangDonHangRepository
    {
        public static List<tChiTietChuyenHangDonHang> GetData(PhuDinhEntities context, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            return Repository<tChiTietChuyenHangDonHang>.GetData(context, filter).
                OrderByDescending(p => p.tChuyenHangDonHang.tChuyenHang.Ngay).
                ThenBy(p => p.tChuyenHangDonHang.tChuyenHang.Gio).ToList();
        }

        public static void Save(PhuDinhEntities context, List<tChiTietChuyenHangDonHang> data, Expression<Func<tChiTietChuyenHangDonHang, bool>> filter)
        {
            Repository<tChiTietChuyenHangDonHang>.Save(context, data, filter, (p => p.Ma == 0), ((p1, p2) => p1.Ma == p2.Ma));
        }
    }
}
